// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'booked_car.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$BookedCarImpl _$$BookedCarImplFromJson(Map<String, dynamic> json) =>
    _$BookedCarImpl(
      name: json['name'] as String,
      licensePlate: json['licensePlate'] as String,
      isOpened: json['isOpened'] as bool,
      id: json['id'] as int,
    );

Map<String, dynamic> _$$BookedCarImplToJson(_$BookedCarImpl instance) =>
    <String, dynamic>{
      'name': instance.name,
      'licensePlate': instance.licensePlate,
      'isOpened': instance.isOpened,
      'id': instance.id,
    };
