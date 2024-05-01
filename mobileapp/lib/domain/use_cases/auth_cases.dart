
import 'dart:isolate';

import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';
import 'package:mobileapp/utils/extensions.dart';

class SignInUserUseCase extends UseCase<String> {

  static String loginMutation = """
  mutation (\$email: String!, \$password: String!) {
    login(vm: {email: \$email, password: \$password})
  }
  """;


  Future<Result<String>> call(String login, String password) async {

    final mutationOptions = MutationOptions(
        document: gql(loginMutation),
        variables: {
          'email': login,
          'password': password
        },
    );

    final mutationResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if (isUnexecuted(mutationResult) || mutationResult.hasException) {
      return tryDispatchError(mutationResult);
    }

    return Ok(mutationResult.data!["login"]);
  }
}

class ValidateSessionUseCase extends UseCase<bool> {
  static String profileInfoQuery = """
    query {
      profile {
        userInfo {
	        fullName
        }
    }
  """;

  Future<Result<bool>> call() async {

    final queryOptions = QueryOptions(document: gql(profileInfoQuery));

    final queryResult = await graphQlClient.query(queryOptions)
        .timeout(const Duration(seconds: 30), onTimeout: () {
      return QueryResult.unexecuted;
    });

    return Ok(!isUnexecuted(queryResult) && queryResult.statusCode == 200);
  }
}

class SignUpUseCase extends UseCase<bool> {

  Future<Result<bool>> call() async {
    return Ok(false);
  }
}