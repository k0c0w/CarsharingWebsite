import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class GetActiveTariffsUseCase extends UseCase<List<Tariff>> {

  static const String _activeTariffsQuery = """
  query {
    tariffs {
      id
      name
      priceInRubles
      maxMileage
      minBookMinutes
      maxBookMinutes
    }
  }
  """;


  Future<Result<List<Tariff>>> call() async {
    final queryOptions = QueryOptions(document: gql(_activeTariffsQuery));

    final getTariffResult = await withTimeOut(graphQlClient.query(queryOptions));

    if (getTariffResult.hasException || isUnexecuted(getTariffResult)) {
      return tryDispatchError(getTariffResult);
    }

    final tariffsMap = getTariffResult.data!["tariffs"] as List;

    return Ok<List<Tariff>>(
      tariffsMap
          .map((json) => Tariff.fromJson(json as Map<String, dynamic>))
          .toList()
    );
  }
}