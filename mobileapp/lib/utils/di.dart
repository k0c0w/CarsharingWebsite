import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:get_it/get_it.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/domain/providers/location_provider.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/domain/providers/user_info_provider.dart';
import 'package:mobileapp/main.dart';
import 'package:shared_preferences/shared_preferences.dart';

Future<void> registerServicesAtGetIt(GetIt getIt) async {
  getIt.registerSingleton(await SharedPreferences.getInstance());
  getIt.registerSingleton(const FlutterSecureStorage());

  getIt.registerSingleton(SessionDataProvider(getIt<FlutterSecureStorage>()));
  getIt.registerSingleton(DrawerUserInfoDataProvider(getIt<SharedPreferences>()));
  getIt.registerSingleton(LocationProvider());

  getIt.registerSingleton(AuthBloc(AuthCheckStatusInProgressState()));

  getIt.registerSingleton<GraphQLClient>(getGraphQLClient(getIt<SessionDataProvider>()));


  await getIt.allReady();
}

final graphQLClientFactory = getGraphQLClient(getIt<SessionDataProvider>());

GraphQLClient getGraphQLClient(SessionDataProvider sessionDataProvider) {
  final HttpLink httpLink = HttpLink(
    'http://192.168.0.16:5082/graphql/',
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