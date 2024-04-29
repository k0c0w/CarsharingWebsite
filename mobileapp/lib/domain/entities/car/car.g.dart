// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'car.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$CarImpl _$$CarImplFromJson(Map<String, dynamic> json) => _$CarImpl(
      id: json['id'] as int,
      model: json['model'] as String,
      brand: json['brand'] as String,
      carModelDescription: json['carModelDescription'] as String,
      location: GeoPoint.fromJson(json['location'] as Map<String, dynamic>),
      licensePlate: json['licensePlate'] as String,
    );

Map<String, dynamic> _$$CarImplToJson(_$CarImpl instance) => <String, dynamic>{
      'id': instance.id,
      'model': instance.model,
      'brand': instance.brand,
      'carModelDescription': instance.carModelDescription,
      'location': instance.location,
      'licensePlate': instance.licensePlate,
    };
