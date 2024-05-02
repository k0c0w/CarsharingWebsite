import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class BookCarUseCase extends UseCase<bool> {

  static const String _bookCarMutation = """
  mutation (\$carId: Int!, \$endDate: DateTime!, \$startDate: DateTime!){
    bookCar(bookingInfo: {
      carId: \$carId
      endDate: \$endDate
      startDate: \$startDate
    })
  }
  """;

  Future<Result<bool>> call(int carId, DateTime startDate, DateTime endDate) async {
    final mutationOptions = MutationOptions(
        document: gql(_bookCarMutation),
        variables: {
          "carId": carId,
          "endDate": endDate.toIso8601String(),
          "startDate": startDate.toIso8601String()
        }
    );

    final mutationResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if (mutationResult.hasException | isUnexecuted(mutationResult)) {
      return tryDispatchError(mutationResult);
    }

    return Ok<bool>(true);
  }
}