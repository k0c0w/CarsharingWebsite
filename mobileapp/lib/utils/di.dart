import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:get_it/get_it.dart';
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/domain/providers/user_info_provider.dart';
import 'package:shared_preferences/shared_preferences.dart';

Future<void> registerServicesAtGetIt(GetIt getIt) async {
  getIt.registerSingleton(await SharedPreferences.getInstance());
  getIt.registerSingleton(const FlutterSecureStorage());

  getIt.registerSingleton(SessionDataProvider(getIt<FlutterSecureStorage>()));
  getIt.registerSingleton(DrawerUserInfoDataProvider(getIt<SharedPreferences>()));

  getIt.registerSingleton(AuthBloc(AuthCheckStatusInProgressState()));

  _registerGraphQLClient(getIt);

  await getIt.allReady();
}

void _registerGraphQLClient(GetIt getIt) {
  final HttpLink httpLink = HttpLink(
    'https://api.github.com/graphql',
  );

  final AuthLink authLink = AuthLink(
      getToken: () async {
        final jwtToken = await getIt<SessionDataProvider>().getJwtToken();
        return 'Bearer $jwtToken';
      }
  );

  final Link link = authLink.concat(httpLink);

  getIt.registerSingleton(GraphQLClient(link: link, cache: GraphQLCache()));
}