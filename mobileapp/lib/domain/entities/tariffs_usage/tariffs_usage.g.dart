// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'tariffs_usage.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$TariffUsageStatsImpl _$$TariffUsageStatsImplFromJson(
        Map<String, dynamic> json) =>
    _$TariffUsageStatsImpl(
      tariffName: json['TariffName'] as String,
      usageCount: json['UsageCount'] as int,
    );

Map<String, dynamic> _$$TariffUsageStatsImplToJson(
        _$TariffUsageStatsImpl instance) =>
    <String, dynamic>{
      'TariffName': instance.tariffName,
      'UsageCount': instance.usageCount,
    };
