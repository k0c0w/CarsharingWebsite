// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'map_search_area.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

/// @nodoc
mixin _$MapSearchArea {
  GeoPoint get anchorPoint => throw _privateConstructorUsedError;
  double get radius => throw _privateConstructorUsedError;

  @JsonKey(ignore: true)
  $MapSearchAreaCopyWith<MapSearchArea> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $MapSearchAreaCopyWith<$Res> {
  factory $MapSearchAreaCopyWith(
          MapSearchArea value, $Res Function(MapSearchArea) then) =
      _$MapSearchAreaCopyWithImpl<$Res, MapSearchArea>;
  @useResult
  $Res call({GeoPoint anchorPoint, double radius});

  $GeoPointCopyWith<$Res> get anchorPoint;
}

/// @nodoc
class _$MapSearchAreaCopyWithImpl<$Res, $Val extends MapSearchArea>
    implements $MapSearchAreaCopyWith<$Res> {
  _$MapSearchAreaCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? anchorPoint = null,
    Object? radius = null,
  }) {
    return _then(_value.copyWith(
      anchorPoint: null == anchorPoint
          ? _value.anchorPoint
          : anchorPoint // ignore: cast_nullable_to_non_nullable
              as GeoPoint,
      radius: null == radius
          ? _value.radius
          : radius // ignore: cast_nullable_to_non_nullable
              as double,
    ) as $Val);
  }

  @override
  @pragma('vm:prefer-inline')
  $GeoPointCopyWith<$Res> get anchorPoint {
    return $GeoPointCopyWith<$Res>(_value.anchorPoint, (value) {
      return _then(_value.copyWith(anchorPoint: value) as $Val);
    });
  }
}

/// @nodoc
abstract class _$$MapSearchAreaImplCopyWith<$Res>
    implements $MapSearchAreaCopyWith<$Res> {
  factory _$$MapSearchAreaImplCopyWith(
          _$MapSearchAreaImpl value, $Res Function(_$MapSearchAreaImpl) then) =
      __$$MapSearchAreaImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call({GeoPoint anchorPoint, double radius});

  @override
  $GeoPointCopyWith<$Res> get anchorPoint;
}

/// @nodoc
class __$$MapSearchAreaImplCopyWithImpl<$Res>
    extends _$MapSearchAreaCopyWithImpl<$Res, _$MapSearchAreaImpl>
    implements _$$MapSearchAreaImplCopyWith<$Res> {
  __$$MapSearchAreaImplCopyWithImpl(
      _$MapSearchAreaImpl _value, $Res Function(_$MapSearchAreaImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? anchorPoint = null,
    Object? radius = null,
  }) {
    return _then(_$MapSearchAreaImpl(
      null == anchorPoint
          ? _value.anchorPoint
          : anchorPoint // ignore: cast_nullable_to_non_nullable
              as GeoPoint,
      radius: null == radius
          ? _value.radius
          : radius // ignore: cast_nullable_to_non_nullable
              as double,
    ));
  }
}

/// @nodoc

class _$MapSearchAreaImpl implements _MapSearchArea {
  const _$MapSearchAreaImpl(this.anchorPoint, {this.radius = 500});

  @override
  final GeoPoint anchorPoint;
  @override
  @JsonKey()
  final double radius;

  @override
  String toString() {
    return 'MapSearchArea(anchorPoint: $anchorPoint, radius: $radius)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$MapSearchAreaImpl &&
            (identical(other.anchorPoint, anchorPoint) ||
                other.anchorPoint == anchorPoint) &&
            (identical(other.radius, radius) || other.radius == radius));
  }

  @override
  int get hashCode => Object.hash(runtimeType, anchorPoint, radius);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$MapSearchAreaImplCopyWith<_$MapSearchAreaImpl> get copyWith =>
      __$$MapSearchAreaImplCopyWithImpl<_$MapSearchAreaImpl>(this, _$identity);
}

abstract class _MapSearchArea implements MapSearchArea {
  const factory _MapSearchArea(final GeoPoint anchorPoint,
      {final double radius}) = _$MapSearchAreaImpl;

  @override
  GeoPoint get anchorPoint;
  @override
  double get radius;
  @override
  @JsonKey(ignore: true)
  _$$MapSearchAreaImplCopyWith<_$MapSearchAreaImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
