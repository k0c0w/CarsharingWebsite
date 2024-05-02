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
  String get name => throw _privateConstructorUsedError;
  String get licensePlate => throw _privateConstructorUsedError;
  bool get isOpened => throw _privateConstructorUsedError;
  int get id => throw _privateConstructorUsedError;

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
  $Res call({String name, String licensePlate, bool isOpened, int id});
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
    Object? name = null,
    Object? licensePlate = null,
    Object? isOpened = null,
    Object? id = null,
  }) {
    return _then(_value.copyWith(
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      licensePlate: null == licensePlate
          ? _value.licensePlate
          : licensePlate // ignore: cast_nullable_to_non_nullable
              as String,
      isOpened: null == isOpened
          ? _value.isOpened
          : isOpened // ignore: cast_nullable_to_non_nullable
              as bool,
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
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
  $Res call({String name, String licensePlate, bool isOpened, int id});
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
    Object? name = null,
    Object? licensePlate = null,
    Object? isOpened = null,
    Object? id = null,
  }) {
    return _then(_$BookedCarImpl(
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      licensePlate: null == licensePlate
          ? _value.licensePlate
          : licensePlate // ignore: cast_nullable_to_non_nullable
              as String,
      isOpened: null == isOpened
          ? _value.isOpened
          : isOpened // ignore: cast_nullable_to_non_nullable
              as bool,
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$BookedCarImpl implements _BookedCar {
  const _$BookedCarImpl(
      {required this.name,
      required this.licensePlate,
      required this.isOpened,
      required this.id});

  factory _$BookedCarImpl.fromJson(Map<String, dynamic> json) =>
      _$$BookedCarImplFromJson(json);

  @override
  final String name;
  @override
  final String licensePlate;
  @override
  final bool isOpened;
  @override
  final int id;

  @override
  String toString() {
    return 'BookedCar(name: $name, licensePlate: $licensePlate, isOpened: $isOpened, id: $id)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$BookedCarImpl &&
            (identical(other.name, name) || other.name == name) &&
            (identical(other.licensePlate, licensePlate) ||
                other.licensePlate == licensePlate) &&
            (identical(other.isOpened, isOpened) ||
                other.isOpened == isOpened) &&
            (identical(other.id, id) || other.id == id));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode =>
      Object.hash(runtimeType, name, licensePlate, isOpened, id);

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
      {required final String name,
      required final String licensePlate,
      required final bool isOpened,
      required final int id}) = _$BookedCarImpl;

  factory _BookedCar.fromJson(Map<String, dynamic> json) =
      _$BookedCarImpl.fromJson;

  @override
  String get name;
  @override
  String get licensePlate;
  @override
  bool get isOpened;
  @override
  int get id;
  @override
  @JsonKey(ignore: true)
  _$$BookedCarImplCopyWith<_$BookedCarImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
