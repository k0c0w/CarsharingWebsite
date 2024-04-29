// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'booked_car.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$BookedCarImpl _$$BookedCarImplFromJson(Map<String, dynamic> json) =>
    _$BookedCarImpl(
      id: json['id'] as int,
      model: json['model'] as String,
      brand: json['brand'] as String,
      licensePlate: json['licensePlate'] as String,
      isOpen: json['isOpen'] as bool,
    );

Map<String, dynamic> _$$BookedCarImplToJson(_$BookedCarImpl instance) =>
    <String, dynamic>{
      'id': instance.id,
      'model': instance.model,
      'brand': instance.brand,
      'licensePlate': instance.licensePlate,
      'isOpen': instance.isOpen,
    };
