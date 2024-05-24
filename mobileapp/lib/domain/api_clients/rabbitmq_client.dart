import 'dart:async';
import 'dart:convert';
import 'package:dart_amqp/dart_amqp.dart';
import 'package:get_it/get_it.dart';
import 'package:mobileapp/domain/entities/tariffs_usage/tariffs_usage.dart';
import 'package:mobileapp/utils/grpc/analytics.pbgrpc.dart';
import 'package:mobileapp/utils/grpc/google/empty.pb.dart';


class RabbitMqClient extends Disposable {
  late final StreamController<List<TariffUsageStats>> _streamController = StreamController<List<TariffUsageStats>>.broadcast();
  final Client _client;
  final AnalyticsServiceClient _analyticsClient;

  Channel? _channel;

  Stream<List<TariffUsageStats>> get serverEvents => _streamController.stream;

  bool get isCreated => _channel != null;

  RabbitMqClient(this._client, this._analyticsClient) {
  }

  Future<void> init() async {
    await _client.connect();
    _channel = await _client.channel();

    final queueResponse = await _analyticsClient.subscribeTariffsUsageUpdates(Empty());
    final queue = await _channel!.queue(queueResponse.queueName, autoDelete: true);

    final consumer = await queue.consume();
    consumer.listen((event) {
      final json = event.payloadAsJson as Map<String, dynamic>;
      final stats = json["Stats"] as List;

      final mappedList = stats
          .map((stat) => TariffUsageStats.fromJson(stat))
          .toList();

      mappedList.sort((a, b) => a.tariffName.compareTo(b.tariffName));
      _streamController.add(mappedList);
    });
  }

  @override
  FutureOr onDispose() async {
    await _channel?.close();
    await _client.close();
  }
}