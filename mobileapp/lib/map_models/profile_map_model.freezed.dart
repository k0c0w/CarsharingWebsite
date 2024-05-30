// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'profile_map_model.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

/// @nodoc
mixin _$ProfilePageBlocStateLoadedMapModelProperty {
  String get text => throw _privateConstructorUsedError;
  String get error => throw _privateConstructorUsedError;

  @JsonKey(ignore: true)
  $ProfilePageBlocStateLoadedMapModelPropertyCopyWith<
          ProfilePageBlocStateLoadedMapModelProperty>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $ProfilePageBlocStateLoadedMapModelPropertyCopyWith<$Res> {
  factory $ProfilePageBlocStateLoadedMapModelPropertyCopyWith(
          ProfilePageBlocStateLoadedMapModelProperty value,
          $Res Function(ProfilePageBlocStateLoadedMapModelProperty) then) =
      _$ProfilePageBlocStateLoadedMapModelPropertyCopyWithImpl<$Res,
          ProfilePageBlocStateLoadedMapModelProperty>;
  @useResult
  $Res call({String text, String error});
}

/// @nodoc
class _$ProfilePageBlocStateLoadedMapModelPropertyCopyWithImpl<$Res,
        $Val extends ProfilePageBlocStateLoadedMapModelProperty>
    implements $ProfilePageBlocStateLoadedMapModelPropertyCopyWith<$Res> {
  _$ProfilePageBlocStateLoadedMapModelPropertyCopyWithImpl(
      this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? text = null,
    Object? error = null,
  }) {
    return _then(_value.copyWith(
      text: null == text
          ? _value.text
          : text // ignore: cast_nullable_to_non_nullable
              as String,
      error: null == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWith<$Res>
    implements $ProfilePageBlocStateLoadedMapModelPropertyCopyWith<$Res> {
  factory _$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWith(
          _$ProfilePageBlocStateLoadedMapModelPropertyImpl value,
          $Res Function(_$ProfilePageBlocStateLoadedMapModelPropertyImpl)
              then) =
      __$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call({String text, String error});
}

/// @nodoc
class __$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWithImpl<$Res>
    extends _$ProfilePageBlocStateLoadedMapModelPropertyCopyWithImpl<$Res,
        _$ProfilePageBlocStateLoadedMapModelPropertyImpl>
    implements _$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWith<$Res> {
  __$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWithImpl(
      _$ProfilePageBlocStateLoadedMapModelPropertyImpl _value,
      $Res Function(_$ProfilePageBlocStateLoadedMapModelPropertyImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? text = null,
    Object? error = null,
  }) {
    return _then(_$ProfilePageBlocStateLoadedMapModelPropertyImpl(
      text: null == text
          ? _value.text
          : text // ignore: cast_nullable_to_non_nullable
              as String,
      error: null == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String,
    ));
  }
}

/// @nodoc

class _$ProfilePageBlocStateLoadedMapModelPropertyImpl
    implements _ProfilePageBlocStateLoadedMapModelProperty {
  const _$ProfilePageBlocStateLoadedMapModelPropertyImpl(
      {required this.text, this.error = ""});

  @override
  final String text;
  @override
  @JsonKey()
  final String error;

  @override
  String toString() {
    return 'ProfilePageBlocStateLoadedMapModelProperty(text: $text, error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ProfilePageBlocStateLoadedMapModelPropertyImpl &&
            (identical(other.text, text) || other.text == text) &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, text, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWith<
          _$ProfilePageBlocStateLoadedMapModelPropertyImpl>
      get copyWith =>
          __$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWithImpl<
                  _$ProfilePageBlocStateLoadedMapModelPropertyImpl>(
              this, _$identity);
}

abstract class _ProfilePageBlocStateLoadedMapModelProperty
    implements ProfilePageBlocStateLoadedMapModelProperty {
  const factory _ProfilePageBlocStateLoadedMapModelProperty(
      {required final String text,
      final String error}) = _$ProfilePageBlocStateLoadedMapModelPropertyImpl;

  @override
  String get text;
  @override
  String get error;
  @override
  @JsonKey(ignore: true)
  _$$ProfilePageBlocStateLoadedMapModelPropertyImplCopyWith<
          _$ProfilePageBlocStateLoadedMapModelPropertyImpl>
      get copyWith => throw _privateConstructorUsedError;
}
