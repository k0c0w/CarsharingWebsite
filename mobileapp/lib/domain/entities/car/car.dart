import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';

part 'car.freezed.dart';
part 'car.g.dart';

@freezed
class Car with _$Car {

  const factory Car({
    required int id,
    required String model,
    required String brand,
    required String carModelDescription,
    required GeoPoint location,
    required String licensePlate
  }) = _Car;

  factory Car.fromJson(Map<String, Object?> json)
  => _$CarFromJson(json);
}
