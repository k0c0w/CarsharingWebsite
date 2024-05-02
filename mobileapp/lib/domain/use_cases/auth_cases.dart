import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

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
    }
  """;

  Future<Result<bool>> call() async {

    final queryOptions = QueryOptions(document: gql(profileInfoQuery));

    final queryResult = await withTimeOut(graphQlClient.query(queryOptions));

    return Ok(!isUnexecuted(queryResult) && !queryResult.hasException);
  }
}

class SignUpUseCase extends UseCase<bool> {

  Future<Result<bool>> call(
      String email,
      String firstName,
      String secondName,
      DateTime birthDate,
      String password) async {
    return Ok(false);
  }
}