// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'tariff.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

Tariff _$TariffFromJson(Map<String, dynamic> json) {
  return _Tariff.fromJson(json);
}

/// @nodoc
mixin _$Tariff {
  int get id => throw _privateConstructorUsedError;
  String get name => throw _privateConstructorUsedError;
  double get maxRentMinutes => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $TariffCopyWith<Tariff> get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $TariffCopyWith<$Res> {
  factory $TariffCopyWith(Tariff value, $Res Function(Tariff) then) =
      _$TariffCopyWithImpl<$Res, Tariff>;
  @useResult
  $Res call({int id, String name, double maxRentMinutes});
}

/// @nodoc
class _$TariffCopyWithImpl<$Res, $Val extends Tariff>
    implements $TariffCopyWith<$Res> {
  _$TariffCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? name = null,
    Object? maxRentMinutes = null,
  }) {
    return _then(_value.copyWith(
      id: null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      maxRentMinutes: null == maxRentMinutes
          ? _value.maxRentMinutes
          : maxRentMinutes // ignore: cast_nullable_to_non_nullable
              as double,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$TariffImplCopyWith<$Res> implements $TariffCopyWith<$Res> {
  factory _$$TariffImplCopyWith(
          _$TariffImpl value, $Res Function(_$TariffImpl) then) =
      __$$TariffImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call({int id, String name, double maxRentMinutes});
}

/// @nodoc
class __$$TariffImplCopyWithImpl<$Res>
    extends _$TariffCopyWithImpl<$Res, _$TariffImpl>
    implements _$$TariffImplCopyWith<$Res> {
  __$$TariffImplCopyWithImpl(
      _$TariffImpl _value, $Res Function(_$TariffImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? id = null,
    Object? name = null,
    Object? maxRentMinutes = null,
  }) {
    return _then(_$TariffImpl(
      null == id
          ? _value.id
          : id // ignore: cast_nullable_to_non_nullable
              as int,
      null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      null == maxRentMinutes
          ? _value.maxRentMinutes
          : maxRentMinutes // ignore: cast_nullable_to_non_nullable
              as double,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$TariffImpl implements _Tariff {
  const _$TariffImpl(this.id, this.name, this.maxRentMinutes);

  factory _$TariffImpl.fromJson(Map<String, dynamic> json) =>
      _$$TariffImplFromJson(json);

  @override
  final int id;
  @override
  final String name;
  @override
  final double maxRentMinutes;

  @override
  String toString() {
    return 'Tariff(id: $id, name: $name, maxRentMinutes: $maxRentMinutes)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$TariffImpl &&
            (identical(other.id, id) || other.id == id) &&
            (identical(other.name, name) || other.name == name) &&
            (identical(other.maxRentMinutes, maxRentMinutes) ||
                other.maxRentMinutes == maxRentMinutes));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, id, name, maxRentMinutes);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$TariffImplCopyWith<_$TariffImpl> get copyWith =>
      __$$TariffImplCopyWithImpl<_$TariffImpl>(this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$TariffImplToJson(
      this,
    );
  }
}

abstract class _Tariff implements Tariff {
  const factory _Tariff(
          final int id, final String name, final double maxRentMinutes) =
      _$TariffImpl;

  factory _Tariff.fromJson(Map<String, dynamic> json) = _$TariffImpl.fromJson;

  @override
  int get id;
  @override
  String get name;
  @override
  double get maxRentMinutes;
  @override
  @JsonKey(ignore: true)
  _$$TariffImplCopyWith<_$TariffImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
