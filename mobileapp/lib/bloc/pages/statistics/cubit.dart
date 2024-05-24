import 'dart:async';
import 'package:bloc/bloc.dart';
import 'package:mobileapp/bloc/pages/statistics/state.dart';
import 'package:mobileapp/domain/api_clients/rabbitmq_client.dart';
import 'package:mobileapp/domain/entities/tariffs_usage/tariffs_usage.dart';

class StatisticsCubit extends Cubit<StatisticsState> {
  final RabbitMqClient _client;
  StreamSubscription<List<TariffUsageStats>>? _streamSubscription;

  StatisticsCubit(this._client, super.initialState);

  void _onStatistics(List<TariffUsageStats> stats) {
    emit(StatisticsState.received(stats: stats));
  }

  Future<void> trySubscribeUpdates() async {
    emit(const StatisticsState.subscribing());
    try{
      _streamSubscription?.cancel();
      await _client.onDispose();

      _streamSubscription = _client.serverEvents.listen(_onStatistics);
      await _client.init();
    } catch(e) {
      _streamSubscription?.cancel();
      await _client.onDispose();

      emit(const StatisticsState.subscriptionError());
    }
  }

  @override
  Future<void> close() async {
    _streamSubscription?.cancel();
    await _client.onDispose();
    await super.close();
  }
}