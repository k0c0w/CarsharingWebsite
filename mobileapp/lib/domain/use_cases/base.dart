import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/main.dart';

abstract class UseCase<TModel> {
  final graphQLClient = getIt<GraphQLClient>();
  final _authBloc = getIt<AuthBloc>();

  Error<TModel> tryDispatchError(QueryResult queryResult) {
    assert(queryResult.hasException);

    var error = "Произошла неизвестная ошибка. Повторите попытку.";
    final exception = queryResult.exception!.linkException;

    if (exception is NetworkException) {
      error = "Проблемы с интернетом.";
    }

    return Error<TModel>(error);
  }

  void logout() {
    _authBloc.add(AuthLogoutEvent());
  }
}
