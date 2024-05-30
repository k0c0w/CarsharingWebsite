import 'package:freezed_annotation/freezed_annotation.dart';

part 'booked_car.freezed.dart';
part 'booked_car.g.dart';

@freezed
class BookedCar with _$BookedCar {

  const factory BookedCar({
    required String name,
    required String licensePlate,
    required bool isOpened,
    required int id,
  }) = _BookedCar;

  factory BookedCar.fromJson(Map<String, Object?> json)
  => _$BookedCarFromJson(json);
}
