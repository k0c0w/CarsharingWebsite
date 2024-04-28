// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'tariff.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$TariffImpl _$$TariffImplFromJson(Map<String, dynamic> json) => _$TariffImpl(
      json['id'] as int,
      json['name'] as String,
      (json['maxRentMinutes'] as num).toDouble(),
    );

Map<String, dynamic> _$$TariffImplToJson(_$TariffImpl instance) =>
    <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'maxRentMinutes': instance.maxRentMinutes,
    };
