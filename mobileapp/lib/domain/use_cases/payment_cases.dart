import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class PaymentUseCase extends UseCase<bool> {
  static const String _balanceMutation = """
  mutation(\$value: Decimal!) {
    increaseBalance(val: \$value)
  }
  """;

  Future<Result<bool>> call(double money) async {
    final increaseBalanceMutationOptions = MutationOptions(
        document: gql(_balanceMutation),
        variables: {
          "value": money
        }
    );

    final increaseBalanceResult = await withTimeOut(graphQlClient.mutate(increaseBalanceMutationOptions));

    if (increaseBalanceResult.hasException || isUnexecuted(increaseBalanceResult)) {
      return tryDispatchError(increaseBalanceResult);
    }

    return  Ok(true);
  }
}