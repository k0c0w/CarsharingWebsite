import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';

part 'car_model.freezed.dart';
part 'car_model.g.dart';

@freezed
class CarModel with _$CarModel {

  const factory CarModel({
    required int id,
    required String model,
    required String brand,
    required String description,
    required GeoPoint url,
  }) = _CarModel;

  factory CarModel.fromJson(Map<String, Object?> json)
  => _$CarModelFromJson(json);
}
