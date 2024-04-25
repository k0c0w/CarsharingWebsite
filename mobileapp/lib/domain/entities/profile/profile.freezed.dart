// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'profile.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

Profile _$ProfileFromJson(Map<String, dynamic> json) {
  return _$ProfilePageLoadEvent.fromJson(json);
}

/// @nodoc
mixin _$Profile {
  String get name => throw _privateConstructorUsedError;
  String get secondName => throw _privateConstructorUsedError;
  String get email => throw _privateConstructorUsedError;
  DateTime get birthDate => throw _privateConstructorUsedError;
  double get balance => throw _privateConstructorUsedError;
  bool get isConfirmed => throw _privateConstructorUsedError;
  String? get passport => throw _privateConstructorUsedError;
  String? get driverLicense => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $ProfileCopyWith<Profile> get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $ProfileCopyWith<$Res> {
  factory $ProfileCopyWith(Profile value, $Res Function(Profile) then) =
      _$ProfileCopyWithImpl<$Res, Profile>;
  @useResult
  $Res call(
      {String name,
      String secondName,
      String email,
      DateTime birthDate,
      double balance,
      bool isConfirmed,
      String? passport,
      String? driverLicense});
}

/// @nodoc
class _$ProfileCopyWithImpl<$Res, $Val extends Profile>
    implements $ProfileCopyWith<$Res> {
  _$ProfileCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? name = null,
    Object? secondName = null,
    Object? email = null,
    Object? birthDate = null,
    Object? balance = null,
    Object? isConfirmed = null,
    Object? passport = freezed,
    Object? driverLicense = freezed,
  }) {
    return _then(_value.copyWith(
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      secondName: null == secondName
          ? _value.secondName
          : secondName // ignore: cast_nullable_to_non_nullable
              as String,
      email: null == email
          ? _value.email
          : email // ignore: cast_nullable_to_non_nullable
              as String,
      birthDate: null == birthDate
          ? _value.birthDate
          : birthDate // ignore: cast_nullable_to_non_nullable
              as DateTime,
      balance: null == balance
          ? _value.balance
          : balance // ignore: cast_nullable_to_non_nullable
              as double,
      isConfirmed: null == isConfirmed
          ? _value.isConfirmed
          : isConfirmed // ignore: cast_nullable_to_non_nullable
              as bool,
      passport: freezed == passport
          ? _value.passport
          : passport // ignore: cast_nullable_to_non_nullable
              as String?,
      driverLicense: freezed == driverLicense
          ? _value.driverLicense
          : driverLicense // ignore: cast_nullable_to_non_nullable
              as String?,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$$ProfilePageLoadEventImplCopyWith<$Res>
    implements $ProfileCopyWith<$Res> {
  factory _$$$ProfilePageLoadEventImplCopyWith(
          _$$ProfilePageLoadEventImpl value,
          $Res Function(_$$ProfilePageLoadEventImpl) then) =
      __$$$ProfilePageLoadEventImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call(
      {String name,
      String secondName,
      String email,
      DateTime birthDate,
      double balance,
      bool isConfirmed,
      String? passport,
      String? driverLicense});
}

/// @nodoc
class __$$$ProfilePageLoadEventImplCopyWithImpl<$Res>
    extends _$ProfileCopyWithImpl<$Res, _$$ProfilePageLoadEventImpl>
    implements _$$$ProfilePageLoadEventImplCopyWith<$Res> {
  __$$$ProfilePageLoadEventImplCopyWithImpl(_$$ProfilePageLoadEventImpl _value,
      $Res Function(_$$ProfilePageLoadEventImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? name = null,
    Object? secondName = null,
    Object? email = null,
    Object? birthDate = null,
    Object? balance = null,
    Object? isConfirmed = null,
    Object? passport = freezed,
    Object? driverLicense = freezed,
  }) {
    return _then(_$$ProfilePageLoadEventImpl(
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      secondName: null == secondName
          ? _value.secondName
          : secondName // ignore: cast_nullable_to_non_nullable
              as String,
      email: null == email
          ? _value.email
          : email // ignore: cast_nullable_to_non_nullable
              as String,
      birthDate: null == birthDate
          ? _value.birthDate
          : birthDate // ignore: cast_nullable_to_non_nullable
              as DateTime,
      balance: null == balance
          ? _value.balance
          : balance // ignore: cast_nullable_to_non_nullable
              as double,
      isConfirmed: null == isConfirmed
          ? _value.isConfirmed
          : isConfirmed // ignore: cast_nullable_to_non_nullable
              as bool,
      passport: freezed == passport
          ? _value.passport
          : passport // ignore: cast_nullable_to_non_nullable
              as String?,
      driverLicense: freezed == driverLicense
          ? _value.driverLicense
          : driverLicense // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$$ProfilePageLoadEventImpl implements _$ProfilePageLoadEvent {
  const _$$ProfilePageLoadEventImpl(
      {required this.name,
      required this.secondName,
      required this.email,
      required this.birthDate,
      required this.balance,
      this.isConfirmed = false,
      this.passport,
      this.driverLicense});

  factory _$$ProfilePageLoadEventImpl.fromJson(Map<String, dynamic> json) =>
      _$$$ProfilePageLoadEventImplFromJson(json);

  @override
  final String name;
  @override
  final String secondName;
  @override
  final String email;
  @override
  final DateTime birthDate;
  @override
  final double balance;
  @override
  @JsonKey()
  final bool isConfirmed;
  @override
  final String? passport;
  @override
  final String? driverLicense;

  @override
  String toString() {
    return 'Profile(name: $name, secondName: $secondName, email: $email, birthDate: $birthDate, balance: $balance, isConfirmed: $isConfirmed, passport: $passport, driverLicense: $driverLicense)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$$ProfilePageLoadEventImpl &&
            (identical(other.name, name) || other.name == name) &&
            (identical(other.secondName, secondName) ||
                other.secondName == secondName) &&
            (identical(other.email, email) || other.email == email) &&
            (identical(other.birthDate, birthDate) ||
                other.birthDate == birthDate) &&
            (identical(other.balance, balance) || other.balance == balance) &&
            (identical(other.isConfirmed, isConfirmed) ||
                other.isConfirmed == isConfirmed) &&
            (identical(other.passport, passport) ||
                other.passport == passport) &&
            (identical(other.driverLicense, driverLicense) ||
                other.driverLicense == driverLicense));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, name, secondName, email,
      birthDate, balance, isConfirmed, passport, driverLicense);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$$ProfilePageLoadEventImplCopyWith<_$$ProfilePageLoadEventImpl>
      get copyWith => __$$$ProfilePageLoadEventImplCopyWithImpl<
          _$$ProfilePageLoadEventImpl>(this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$$ProfilePageLoadEventImplToJson(
      this,
    );
  }
}

abstract class _$ProfilePageLoadEvent implements Profile {
  const factory _$ProfilePageLoadEvent(
      {required final String name,
      required final String secondName,
      required final String email,
      required final DateTime birthDate,
      required final double balance,
      final bool isConfirmed,
      final String? passport,
      final String? driverLicense}) = _$$ProfilePageLoadEventImpl;

  factory _$ProfilePageLoadEvent.fromJson(Map<String, dynamic> json) =
      _$$ProfilePageLoadEventImpl.fromJson;

  @override
  String get name;
  @override
  String get secondName;
  @override
  String get email;
  @override
  DateTime get birthDate;
  @override
  double get balance;
  @override
  bool get isConfirmed;
  @override
  String? get passport;
  @override
  String? get driverLicense;
  @override
  @JsonKey(ignore: true)
  _$$$ProfilePageLoadEventImplCopyWith<_$$ProfilePageLoadEventImpl>
      get copyWith => throw _privateConstructorUsedError;
}
