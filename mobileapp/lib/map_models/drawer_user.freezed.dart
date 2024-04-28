// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'drawer_user.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

DrawerUserInfo _$DrawerUserInfoFromJson(Map<String, dynamic> json) {
  return _DrawerUserInfo.fromJson(json);
}

/// @nodoc
mixin _$DrawerUserInfo {
  String get name => throw _privateConstructorUsedError;
  String get secondName => throw _privateConstructorUsedError;
  bool get isConfirmed => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $DrawerUserInfoCopyWith<DrawerUserInfo> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $DrawerUserInfoCopyWith<$Res> {
  factory $DrawerUserInfoCopyWith(
          DrawerUserInfo value, $Res Function(DrawerUserInfo) then) =
      _$DrawerUserInfoCopyWithImpl<$Res, DrawerUserInfo>;
  @useResult
  $Res call({String name, String secondName, bool isConfirmed});
}

/// @nodoc
class _$DrawerUserInfoCopyWithImpl<$Res, $Val extends DrawerUserInfo>
    implements $DrawerUserInfoCopyWith<$Res> {
  _$DrawerUserInfoCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? name = null,
    Object? secondName = null,
    Object? isConfirmed = null,
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
      isConfirmed: null == isConfirmed
          ? _value.isConfirmed
          : isConfirmed // ignore: cast_nullable_to_non_nullable
              as bool,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$DrawerUserInfoImplCopyWith<$Res>
    implements $DrawerUserInfoCopyWith<$Res> {
  factory _$$DrawerUserInfoImplCopyWith(_$DrawerUserInfoImpl value,
          $Res Function(_$DrawerUserInfoImpl) then) =
      __$$DrawerUserInfoImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call({String name, String secondName, bool isConfirmed});
}

/// @nodoc
class __$$DrawerUserInfoImplCopyWithImpl<$Res>
    extends _$DrawerUserInfoCopyWithImpl<$Res, _$DrawerUserInfoImpl>
    implements _$$DrawerUserInfoImplCopyWith<$Res> {
  __$$DrawerUserInfoImplCopyWithImpl(
      _$DrawerUserInfoImpl _value, $Res Function(_$DrawerUserInfoImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? name = null,
    Object? secondName = null,
    Object? isConfirmed = null,
  }) {
    return _then(_$DrawerUserInfoImpl(
      name: null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      secondName: null == secondName
          ? _value.secondName
          : secondName // ignore: cast_nullable_to_non_nullable
              as String,
      isConfirmed: null == isConfirmed
          ? _value.isConfirmed
          : isConfirmed // ignore: cast_nullable_to_non_nullable
              as bool,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$DrawerUserInfoImpl implements _DrawerUserInfo {
  const _$DrawerUserInfoImpl(
      {required this.name,
      required this.secondName,
      required this.isConfirmed});

  factory _$DrawerUserInfoImpl.fromJson(Map<String, dynamic> json) =>
      _$$DrawerUserInfoImplFromJson(json);

  @override
  final String name;
  @override
  final String secondName;
  @override
  final bool isConfirmed;

  @override
  String toString() {
    return 'DrawerUserInfo(name: $name, secondName: $secondName, isConfirmed: $isConfirmed)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerUserInfoImpl &&
            (identical(other.name, name) || other.name == name) &&
            (identical(other.secondName, secondName) ||
                other.secondName == secondName) &&
            (identical(other.isConfirmed, isConfirmed) ||
                other.isConfirmed == isConfirmed));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, name, secondName, isConfirmed);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$DrawerUserInfoImplCopyWith<_$DrawerUserInfoImpl> get copyWith =>
      __$$DrawerUserInfoImplCopyWithImpl<_$DrawerUserInfoImpl>(
          this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$DrawerUserInfoImplToJson(
      this,
    );
  }
}

abstract class _DrawerUserInfo implements DrawerUserInfo {
  const factory _DrawerUserInfo(
      {required final String name,
      required final String secondName,
      required final bool isConfirmed}) = _$DrawerUserInfoImpl;

  factory _DrawerUserInfo.fromJson(Map<String, dynamic> json) =
      _$DrawerUserInfoImpl.fromJson;

  @override
  String get name;
  @override
  String get secondName;
  @override
  bool get isConfirmed;
  @override
  @JsonKey(ignore: true)
  _$$DrawerUserInfoImplCopyWith<_$DrawerUserInfoImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
