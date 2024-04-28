import 'package:freezed_annotation/freezed_annotation.dart';

part 'booked_car.freezed.dart';
part 'booked_car.g.dart';

@freezed
class BookedCar with _$BookedCar {

  const factory BookedCar({
    required int id,
    required String model,
    required String brand,
    required String licensePlate,
    required bool isOpen,
  }) = _BookedCar;

  factory BookedCar.fromJson(Map<String, Object?> json)
  => _$BookedCarFromJson(json);
}
