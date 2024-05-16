// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'profile.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

_$$ProfilePageLoadEventImpl _$$$ProfilePageLoadEventImplFromJson(
        Map<String, dynamic> json) =>
    _$$ProfilePageLoadEventImpl(
      name: json['name'] as String,
      secondName: json['secondName'] as String,
      email: json['email'] as String,
      birthDate: DateTime.parse(json['birthDate'] as String),
      balance: (json['balance'] as num).toDouble(),
      isConfirmed: json['isConfirmed'] as bool? ?? false,
      passport: json['passport'] as String?,
      driverLicense: json['driverLicense'] as String?,
    );

Map<String, dynamic> _$$$ProfilePageLoadEventImplToJson(
        _$$ProfilePageLoadEventImpl instance) =>
    <String, dynamic>{
      'name': instance.name,
      'secondName': instance.secondName,
      'email': instance.email,
      'birthDate': instance.birthDate.toIso8601String(),
      'balance': instance.balance,
      'isConfirmed': instance.isConfirmed,
      'passport': instance.passport,
      'driverLicense': instance.driverLicense,
    };
