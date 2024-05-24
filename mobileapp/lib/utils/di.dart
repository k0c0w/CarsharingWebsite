import 'dart:io';
import 'package:dart_amqp/dart_amqp.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:get_it/get_it.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/domain/api_clients/rabbitmq_client.dart';
import 'package:mobileapp/domain/providers/location_provider.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/domain/providers/user_info_provider.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/utils/grpc/analytics.pbgrpc.dart';
import 'package:mobileapp/utils/grpc/chat.pbgrpc.dart';
import 'package:mobileapp/utils/grpc/chat_grpc_client.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:grpc/grpc.dart' as $grpc;

const host = "10.0.2.2";

Future<void> registerServicesAtGetIt(GetIt getIt) async {
  getIt.registerSingleton(await SharedPreferences.getInstance());
  getIt.registerSingleton(const FlutterSecureStorage());

  getIt.registerSingleton(SessionDataProvider(getIt<FlutterSecureStorage>()));
  getIt.registerSingleton(DrawerUserInfoDataProvider(getIt<SharedPreferences>()));
  getIt.registerSingleton(LocationProvider());

  getIt.registerSingleton(AuthBloc(AuthCheckStatusInProgressState()));

  getIt.registerSingleton<GraphQLClient>(_getGraphQLClient(getIt<SessionDataProvider>()));

  getIt.registerSingleton<ChatGrpcClient>(_getChatGrpcClient());

  getIt.registerSingleton<RabbitMqClient>(_getRabbitMqClient());

  await getIt.allReady();
}

GraphQLClient _getGraphQLClient(SessionDataProvider sessionDataProvider) {
  final HttpLink httpLink = HttpLink(
    'http://$host:5082/graphql/',
  );

  final AuthLink authLink = AuthLink(
      getToken: () async {
        final jwtToken = await sessionDataProvider.getJwtToken();
        return 'Bearer $jwtToken';
      }
  );

  final Link link = authLink.concat(httpLink);

  return GraphQLClient(link: link, cache: GraphQLCache());
}

ChatGrpcClient _getChatGrpcClient() {
  final channel = $grpc.ClientChannel(
      InternetAddress(host, type: InternetAddressType.IPv4),
      port: 7080,
      options: const $grpc.ChannelOptions(
          credentials: $grpc.ChannelCredentials.insecure(),
          connectionTimeout: Duration(seconds: 30),
          connectTimeout: Duration(seconds: 5),
      ),
  );

  final authorizationCallOptions = $grpc.CallOptions(
    providers: [(Map<String, String> metadata, String _) async {
      final sessionDataProvider = getIt<SessionDataProvider>();
      final token = await sessionDataProvider.getJwtToken();
      metadata["Authorization"] = "Bearer $token";
    }],
  );

  final msgSrvCl = MessagingServiceClient(channel, options: authorizationCallOptions);
  final mngmntService = ManagementServiceClient(channel, options: authorizationCallOptions);

  return ChatGrpcClient(msgSrvCl, mngmntService);
}

RabbitMqClient _getRabbitMqClient() {

  final rabbitClient = Client(settings: ConnectionSettings(
    maxConnectionAttempts: 3,
    host: host,
    port: 5673,
    authProvider: const PlainAuthenticator("k0c0w", "passw0rd"),
    tuningSettings: TuningSettings(heartbeatPeriod: const Duration(seconds: 60), maxChannels: 1),
  ));


  final channel = $grpc.ClientChannel(
    InternetAddress(host, type: InternetAddressType.IPv4),
    port: 11080,
    options: const $grpc.ChannelOptions(
      credentials: $grpc.ChannelCredentials.insecure(),
      connectionTimeout: Duration(seconds: 30),
      connectTimeout: Duration(seconds: 5),
    ),
  );

  final authorizationCallOptions = $grpc.CallOptions(
    providers: [(Map<String, String> metadata, String _) async {
      final sessionDataProvider = getIt<SessionDataProvider>();
      final token = await sessionDataProvider.getJwtToken();
      metadata["Authorization"] = "Bearer $token";
    }],
  );

  final grpcClient = AnalyticsServiceClient(channel, options: authorizationCallOptions);

  return RabbitMqClient(rabbitClient, grpcClient);
}