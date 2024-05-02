import 'package:freezed_annotation/freezed_annotation.dart';

part 'tariff.freezed.dart';
part 'tariff.g.dart';

@freezed
class Tariff with _$Tariff {
  const factory Tariff(
      int id,
      String name,
      double priceInRubles,
      double maxBookMinutes,
      double minBookMinutes,
      ) = _Tariff;

  factory Tariff.fromJson(Map<String, Object?> json)
  => _$TariffFromJson(json);
}
