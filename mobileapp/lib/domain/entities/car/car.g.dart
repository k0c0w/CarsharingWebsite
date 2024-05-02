// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'car.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$CarImpl _$$CarImplFromJson(Map<String, dynamic> json) => _$CarImpl(
      id: json['id'] as int,
      model: CarModel.fromJson(json['model'] as Map<String, dynamic>),
      location: GeoPoint.fromJson(json['location'] as Map<String, dynamic>),
      licensePlate: json['licensePlate'] as String,
    );

Map<String, dynamic> _$$CarImplToJson(_$CarImpl instance) => <String, dynamic>{
      'id': instance.id,
      'model': instance.model,
      'location': instance.location,
      'licensePlate': instance.licensePlate,
    };
