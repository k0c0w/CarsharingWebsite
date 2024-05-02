// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'tariff.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$TariffImpl _$$TariffImplFromJson(Map<String, dynamic> json) => _$TariffImpl(
      json['id'] as int,
      json['name'] as String,
      (json['priceInRubles'] as num).toDouble(),
      (json['maxBookMinutes'] as num).toDouble(),
      (json['minBookMinutes'] as num).toDouble(),
    );

Map<String, dynamic> _$$TariffImplToJson(_$TariffImpl instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'priceInRubles': instance.priceInRubles,
      'maxBookMinutes': instance.maxBookMinutes,
      'minBookMinutes': instance.minBookMinutes,
    };
