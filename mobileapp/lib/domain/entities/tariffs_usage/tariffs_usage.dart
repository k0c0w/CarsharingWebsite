import 'package:freezed_annotation/freezed_annotation.dart';

part 'tariffs_usage.freezed.dart';
part 'tariffs_usage.g.dart';

@freezed
class TariffUsageStats with _$TariffUsageStats {
  const factory TariffUsageStats(
      {
        @JsonKey(name: "TariffName")
        required String tariffName,
        @JsonKey(name: "UsageCount")
        required int usageCount
      }) = _TariffUsageStats;

  factory TariffUsageStats.fromJson(Map<String, Object?> json)
  => _$TariffUsageStatsFromJson(json);
}