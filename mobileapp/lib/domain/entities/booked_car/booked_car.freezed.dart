// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'booked_car.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

BookedCar _$BookedCarFromJson(Map<String, dynamic> json) {
  return _BookedCar.fromJson(json);
}

/// @nodoc
mixin _$BookedCar {
  int get id => throw _privateConstructorUsedError;
  String get model => throw _privateConstructorUsedError;
  String get brand => throw _privateConstructorUsedError;
  String get licensePlate => throw _privateConstructorUsedError;
  bool get isOpen => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $BookedCarCopyWith<BookedCar> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $BookedCarCopyWith<$Res> {
  factory $BookedCarCopyWith(BookedCar value, $Res Function(BookedCar) then) =
      _$BookedCarCopyWithImpl<$Res, BookedCar>;
  @useResult
  $Res call(
      {int id, String model, String brand, String licensePlate, bool isOpen});
}

/// @nodoc
class _$BookedCarCopyWithImpl<$Res, $Val extends BookedCar>
    implements $BookedCarCopyWith<$Res> {
  _$BookedCarCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? model = null,
    Object? brand = null,
    Object? licensePlate = null,
    Object? isOpen = null,
  }) {
    return _then(_value.copyWith(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      model: null == model
          ? _value.model
          : model // ignore: cast_nullable_to_non_nullable
              as String,
      brand: null == brand
          ? _value.brand
          : brand // ignore: cast_nullable_to_non_nullable
              as String,
      licensePlate: null == licensePlate
          ? _value.licensePlate
          : licensePlate // ignore: cast_nullable_to_non_nullable
              as String,
      isOpen: null == isOpen
          ? _value.isOpen
          : isOpen // ignore: cast_nullable_to_non_nullable
              as bool,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$BookedCarImplCopyWith<$Res>
    implements $BookedCarCopyWith<$Res> {
  factory _$$BookedCarImplCopyWith(
          _$BookedCarImpl value, $Res Function(_$BookedCarImpl) then) =
      __$$BookedCarImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call(
      {int id, String model, String brand, String licensePlate, bool isOpen});
}

/// @nodoc
class __$$BookedCarImplCopyWithImpl<$Res>
    extends _$BookedCarCopyWithImpl<$Res, _$BookedCarImpl>
    implements _$$BookedCarImplCopyWith<$Res> {
  __$$BookedCarImplCopyWithImpl(
      _$BookedCarImpl _value, $Res Function(_$BookedCarImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? model = null,
    Object? brand = null,
    Object? licensePlate = null,
    Object? isOpen = null,
  }) {
    return _then(_$BookedCarImpl(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      model: null == model
          ? _value.model
          : model // ignore: cast_nullable_to_non_nullable
              as String,
      brand: null == brand
          ? _value.brand
          : brand // ignore: cast_nullable_to_non_nullable
              as String,
      licensePlate: null == licensePlate
          ? _value.licensePlate
          : licensePlate // ignore: cast_nullable_to_non_nullable
              as String,
      isOpen: null == isOpen
          ? _value.isOpen
          : isOpen // ignore: cast_nullable_to_non_nullable
              as bool,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$BookedCarImpl implements _BookedCar {
  const _$BookedCarImpl(
      {required this.id,
      required this.model,
      required this.brand,
      required this.licensePlate,
      required this.isOpen});

  factory _$BookedCarImpl.fromJson(Map<String, dynamic> json) =>
      _$$BookedCarImplFromJson(json);

  @override
  final int id;
  @override
  final String model;
  @override
  final String brand;
  @override
  final String licensePlate;
  @override
  final bool isOpen;

  @override
  String toString() {
    return 'BookedCar(id: $id, model: $model, brand: $brand, licensePlate: $licensePlate, isOpen: $isOpen)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$BookedCarImpl &&
            (identical(other.id, id) || other.id == id) &&
            (identical(other.model, model) || other.model == model) &&
            (identical(other.brand, brand) || other.brand == brand) &&
            (identical(other.licensePlate, licensePlate) ||
                other.licensePlate == licensePlate) &&
            (identical(other.isOpen, isOpen) || other.isOpen == isOpen));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode =>
      Object.hash(runtimeType, id, model, brand, licensePlate, isOpen);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$BookedCarImplCopyWith<_$BookedCarImpl> get copyWith =>
      __$$BookedCarImplCopyWithImpl<_$BookedCarImpl>(this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$BookedCarImplToJson(
      this,
    );
  }
}

abstract class _BookedCar implements BookedCar {
  const factory _BookedCar(
      {required final int id,
      required final String model,
      required final String brand,
      required final String licensePlate,
      required final bool isOpen}) = _$BookedCarImpl;

  factory _BookedCar.fromJson(Map<String, dynamic> json) =
      _$BookedCarImpl.fromJson;

  @override
  int get id;
  @override
  String get model;
  @override
  String get brand;
  @override
  String get licensePlate;
  @override
  bool get isOpen;
  @override
  @JsonKey(ignore: true)
  _$$BookedCarImplCopyWith<_$BookedCarImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
