import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/utils/extensions.dart';

abstract class UseCase<TModel> {
  final _authBloc = getIt<AuthBloc>();
  final graphQlClient = getIt<GraphQLClient>();

  Error<TModel> tryDispatchError(QueryResult queryResult) {
    assert(queryResult.hasException);

    var error = "Произошла неизвестная ошибка. Повторите попытку.";
    final exception = queryResult.exception!.linkException;

    if (exception is NetworkException) {
      error = "Проблемы с интернетом.";
    } else if (queryResult.statusCode == 401){
      _authBloc.add(AuthLogoutEvent());
    } else if (isUnexecuted(queryResult)) {
      error = "Превышено допустимое время ожидания.";
    }

    return Error<TModel>(error);
  }

  void logout() {
    _authBloc.add(AuthLogoutEvent());
  }

  Future<QueryResult<T>> withTimeOut<T>(Future<QueryResult<T>> future, [Duration? timeOut])
    => future.timeout(timeOut ?? const Duration(seconds: 30),
        onTimeout: () {return QueryResult.unexecuted;});

  bool isUnexecuted<T>(QueryResult<T> queryResult) => queryResult == QueryResult.unexecuted;
}
