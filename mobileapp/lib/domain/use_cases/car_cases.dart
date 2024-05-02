import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/car_model/car_model.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class GetCarsByTariffUseCase extends UseCase<List<Car>> {

  static const String _getCarModelsQuery = """
  query (\$tariffId: Int!) {
    carModelsByTariff(id: \$tariffId) {
      id
      description
      brand
      model
      url
    }
  }
  """;

  static const String _getCarsQuery = """
  query (\$carModelId: Int!, \$longitude: Decimal!, \$latitude: Decimal!, \$radius: Int!) {
    freeCars(carSearch: { 
    carModelId: \$carModelId, longitude: \$longitude, latitude: \$latitude, radius: \$radius}) {
      id
      licensePlate
      parkingLatitude
      parkingLongitude
    }
  }
  """;

  Future<Result<List<Car>>> call(int tariffId, GeoPoint location, {int radiusInMeters = 20}) async {
    final carModelsQueryOptions = QueryOptions(
        document: gql(_getCarModelsQuery),
        variables: {
          "tariffId": tariffId
        });
    final carModelsResult = await withTimeOut(graphQlClient.query(carModelsQueryOptions));
    if (carModelsResult.hasException || isUnexecuted(carModelsResult)) {
      return tryDispatchError(carModelsResult);
    }

    final carModels = (carModelsResult.data!["carModelsByTariff"] as List)
                      .map((json) => CarModel.fromJson(json as Map<String, dynamic>))
                      .toList();

    final freeCars = <Car>[];
    for (var i = 0; i < carModels.length; i++) {
      final carModel = carModels[i];
      final carModelQueryOptions = QueryOptions(
          document: gql(_getCarsQuery),
          variables: {
            "carModelId": carModel.id,
            "longitude": location.long,
            "latitude": location.lat,
            "radius": radiusInMeters,
          }
      );

      final queryResult = await withTimeOut(graphQlClient.query(carModelQueryOptions));

      if (queryResult.hasException || isUnexecuted(queryResult)){
        continue;
      }

      (queryResult.data!["freeCars"] as List)
          .forEach((map) {
              final json = map as Map<String, dynamic>;
              final car = Car(
                id: json["id"],
                model: carModel,
                location: GeoPoint(json["parkingLatitude"], json["parkingLongitude"]),
                licensePlate: json["licensePlate"],
              );

              freeCars.add(car);
          });
    }

    return Ok(freeCars);
  }
}