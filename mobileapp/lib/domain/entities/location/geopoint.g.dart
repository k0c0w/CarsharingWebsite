// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'geopoint.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$GeoPointImpl _$$GeoPointImplFromJson(Map<String, dynamic> json) =>
    _$GeoPointImpl(
      (json['lat'] as num).toDouble(),
      (json['long'] as num).toDouble(),
    );

Map<String, dynamic> _$$GeoPointImplToJson(_$GeoPointImpl instance) =>
    <String, dynamic>{
      'lat': instance.lat,
      'long': instance.long,
    };
