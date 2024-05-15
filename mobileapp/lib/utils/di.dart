import 'dart:io';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:get_it/get_it.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/domain/providers/location_provider.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/domain/providers/user_info_provider.dart';
import 'package:mobileapp/main.dart';
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

  getIt.registerSingleton<GraphQLClient>(getGraphQLClient(getIt<SessionDataProvider>()));

  getIt.registerSingleton<ChatGrpcClient>(getChatGrpcClient());

  await getIt.allReady();
}

final graphQLClientFactory = getGraphQLClient(getIt<SessionDataProvider>());

GraphQLClient getGraphQLClient(SessionDataProvider sessionDataProvider) {
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

ChatGrpcClient getChatGrpcClient() {
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