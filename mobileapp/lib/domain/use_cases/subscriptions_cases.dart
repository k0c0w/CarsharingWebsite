
import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/entities/booked_car/booked_car.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class RetrieveSubscriptionsUseCase extends UseCase<List<BookedCar>>{
  static const _activeSubs = """
  query {
    profile {
      bookedCars {
        id
        name
        licensePlate
        isOpened 
      }
    }
  }
  """;
  Future<Result<List<BookedCar>>> call() async {
    final queryOptions = QueryOptions(document: gql(_activeSubs), fetchPolicy: FetchPolicy.noCache);
    final activeSubsQueryResult = await withTimeOut(graphQlClient.query(queryOptions));

    if (activeSubsQueryResult.hasException || isUnexecuted(activeSubsQueryResult)) {
      return tryDispatchError(activeSubsQueryResult);
    }

    final bookedCars = activeSubsQueryResult.data!["profile"]["bookedCars"] as List;

    return Ok(bookedCars
        .map((json) => BookedCar.fromJson(json as Map<String, dynamic>))
        .toList()
    );
  }
}

class ChangeCarOpenStateUseCase extends UseCase<BookedCar> {
  Future<BookedCar> call() async {
    throw Error("dgdf");
  }
}

