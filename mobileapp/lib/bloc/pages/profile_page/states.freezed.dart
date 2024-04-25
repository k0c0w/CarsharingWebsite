// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'states.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

/// @nodoc
mixin _$ProfilePageBlocState {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(ProfilePageBlocStateLoadedMapModel model) loaded,
    required TResult Function(String error) loadError,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult? Function(String error)? loadError,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult Function(String error)? loadError,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(ProfilePageBlocStateLoading value) loading,
    required TResult Function(ProfilePageBlocStateLoaded value) loaded,
    required TResult Function(ProfilePageBlocStateLoadError value) loadError,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ProfilePageBlocStateLoading value)? loading,
    TResult? Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult? Function(ProfilePageBlocStateLoadError value)? loadError,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ProfilePageBlocStateLoading value)? loading,
    TResult Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult Function(ProfilePageBlocStateLoadError value)? loadError,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $ProfilePageBlocStateCopyWith<$Res> {
  factory $ProfilePageBlocStateCopyWith(ProfilePageBlocState value,
          $Res Function(ProfilePageBlocState) then) =
      _$ProfilePageBlocStateCopyWithImpl<$Res, ProfilePageBlocState>;
}

/// @nodoc
class _$ProfilePageBlocStateCopyWithImpl<$Res,
        $Val extends ProfilePageBlocState>
    implements $ProfilePageBlocStateCopyWith<$Res> {
  _$ProfilePageBlocStateCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$ProfilePageBlocStateLoadingImplCopyWith<$Res> {
  factory _$$ProfilePageBlocStateLoadingImplCopyWith(
          _$ProfilePageBlocStateLoadingImpl value,
          $Res Function(_$ProfilePageBlocStateLoadingImpl) then) =
      __$$ProfilePageBlocStateLoadingImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$ProfilePageBlocStateLoadingImplCopyWithImpl<$Res>
    extends _$ProfilePageBlocStateCopyWithImpl<$Res,
        _$ProfilePageBlocStateLoadingImpl>
    implements _$$ProfilePageBlocStateLoadingImplCopyWith<$Res> {
  __$$ProfilePageBlocStateLoadingImplCopyWithImpl(
      _$ProfilePageBlocStateLoadingImpl _value,
      $Res Function(_$ProfilePageBlocStateLoadingImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$ProfilePageBlocStateLoadingImpl implements ProfilePageBlocStateLoading {
  const _$ProfilePageBlocStateLoadingImpl();

  @override
  String toString() {
    return 'ProfilePageBlocState.loading()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ProfilePageBlocStateLoadingImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(ProfilePageBlocStateLoadedMapModel model) loaded,
    required TResult Function(String error) loadError,
  }) {
    return loading();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult? Function(String error)? loadError,
  }) {
    return loading?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult Function(String error)? loadError,
    required TResult orElse(),
  }) {
    if (loading != null) {
      return loading();
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(ProfilePageBlocStateLoading value) loading,
    required TResult Function(ProfilePageBlocStateLoaded value) loaded,
    required TResult Function(ProfilePageBlocStateLoadError value) loadError,
  }) {
    return loading(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ProfilePageBlocStateLoading value)? loading,
    TResult? Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult? Function(ProfilePageBlocStateLoadError value)? loadError,
  }) {
    return loading?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ProfilePageBlocStateLoading value)? loading,
    TResult Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult Function(ProfilePageBlocStateLoadError value)? loadError,
    required TResult orElse(),
  }) {
    if (loading != null) {
      return loading(this);
    }
    return orElse();
  }
}

abstract class ProfilePageBlocStateLoading implements ProfilePageBlocState {
  const factory ProfilePageBlocStateLoading() =
      _$ProfilePageBlocStateLoadingImpl;
}

/// @nodoc
abstract class _$$ProfilePageBlocStateLoadedImplCopyWith<$Res> {
  factory _$$ProfilePageBlocStateLoadedImplCopyWith(
          _$ProfilePageBlocStateLoadedImpl value,
          $Res Function(_$ProfilePageBlocStateLoadedImpl) then) =
      __$$ProfilePageBlocStateLoadedImplCopyWithImpl<$Res>;
  @useResult
  $Res call({ProfilePageBlocStateLoadedMapModel model});
}

/// @nodoc
class __$$ProfilePageBlocStateLoadedImplCopyWithImpl<$Res>
    extends _$ProfilePageBlocStateCopyWithImpl<$Res,
        _$ProfilePageBlocStateLoadedImpl>
    implements _$$ProfilePageBlocStateLoadedImplCopyWith<$Res> {
  __$$ProfilePageBlocStateLoadedImplCopyWithImpl(
      _$ProfilePageBlocStateLoadedImpl _value,
      $Res Function(_$ProfilePageBlocStateLoadedImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? model = null,
  }) {
    return _then(_$ProfilePageBlocStateLoadedImpl(
      null == model
          ? _value.model
          : model // ignore: cast_nullable_to_non_nullable
              as ProfilePageBlocStateLoadedMapModel,
    ));
  }
}

/// @nodoc

class _$ProfilePageBlocStateLoadedImpl implements ProfilePageBlocStateLoaded {
  const _$ProfilePageBlocStateLoadedImpl(this.model);

  @override
  final ProfilePageBlocStateLoadedMapModel model;

  @override
  String toString() {
    return 'ProfilePageBlocState.loaded(model: $model)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ProfilePageBlocStateLoadedImpl &&
            (identical(other.model, model) || other.model == model));
  }

  @override
  int get hashCode => Object.hash(runtimeType, model);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ProfilePageBlocStateLoadedImplCopyWith<_$ProfilePageBlocStateLoadedImpl>
      get copyWith => __$$ProfilePageBlocStateLoadedImplCopyWithImpl<
          _$ProfilePageBlocStateLoadedImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(ProfilePageBlocStateLoadedMapModel model) loaded,
    required TResult Function(String error) loadError,
  }) {
    return loaded(model);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult? Function(String error)? loadError,
  }) {
    return loaded?.call(model);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult Function(String error)? loadError,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(model);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(ProfilePageBlocStateLoading value) loading,
    required TResult Function(ProfilePageBlocStateLoaded value) loaded,
    required TResult Function(ProfilePageBlocStateLoadError value) loadError,
  }) {
    return loaded(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ProfilePageBlocStateLoading value)? loading,
    TResult? Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult? Function(ProfilePageBlocStateLoadError value)? loadError,
  }) {
    return loaded?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ProfilePageBlocStateLoading value)? loading,
    TResult Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult Function(ProfilePageBlocStateLoadError value)? loadError,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(this);
    }
    return orElse();
  }
}

abstract class ProfilePageBlocStateLoaded implements ProfilePageBlocState {
  const factory ProfilePageBlocStateLoaded(
          final ProfilePageBlocStateLoadedMapModel model) =
      _$ProfilePageBlocStateLoadedImpl;

  ProfilePageBlocStateLoadedMapModel get model;
  @JsonKey(ignore: true)
  _$$ProfilePageBlocStateLoadedImplCopyWith<_$ProfilePageBlocStateLoadedImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$ProfilePageBlocStateLoadErrorImplCopyWith<$Res> {
  factory _$$ProfilePageBlocStateLoadErrorImplCopyWith(
          _$ProfilePageBlocStateLoadErrorImpl value,
          $Res Function(_$ProfilePageBlocStateLoadErrorImpl) then) =
      __$$ProfilePageBlocStateLoadErrorImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String error});
}

/// @nodoc
class __$$ProfilePageBlocStateLoadErrorImplCopyWithImpl<$Res>
    extends _$ProfilePageBlocStateCopyWithImpl<$Res,
        _$ProfilePageBlocStateLoadErrorImpl>
    implements _$$ProfilePageBlocStateLoadErrorImplCopyWith<$Res> {
  __$$ProfilePageBlocStateLoadErrorImplCopyWithImpl(
      _$ProfilePageBlocStateLoadErrorImpl _value,
      $Res Function(_$ProfilePageBlocStateLoadErrorImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? error = null,
  }) {
    return _then(_$ProfilePageBlocStateLoadErrorImpl(
      null == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String,
    ));
  }
}

/// @nodoc

class _$ProfilePageBlocStateLoadErrorImpl
    implements ProfilePageBlocStateLoadError {
  const _$ProfilePageBlocStateLoadErrorImpl(this.error);

  @override
  final String error;

  @override
  String toString() {
    return 'ProfilePageBlocState.loadError(error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ProfilePageBlocStateLoadErrorImpl &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ProfilePageBlocStateLoadErrorImplCopyWith<
          _$ProfilePageBlocStateLoadErrorImpl>
      get copyWith => __$$ProfilePageBlocStateLoadErrorImplCopyWithImpl<
          _$ProfilePageBlocStateLoadErrorImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(ProfilePageBlocStateLoadedMapModel model) loaded,
    required TResult Function(String error) loadError,
  }) {
    return loadError(error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult? Function(String error)? loadError,
  }) {
    return loadError?.call(error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(ProfilePageBlocStateLoadedMapModel model)? loaded,
    TResult Function(String error)? loadError,
    required TResult orElse(),
  }) {
    if (loadError != null) {
      return loadError(error);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(ProfilePageBlocStateLoading value) loading,
    required TResult Function(ProfilePageBlocStateLoaded value) loaded,
    required TResult Function(ProfilePageBlocStateLoadError value) loadError,
  }) {
    return loadError(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ProfilePageBlocStateLoading value)? loading,
    TResult? Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult? Function(ProfilePageBlocStateLoadError value)? loadError,
  }) {
    return loadError?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ProfilePageBlocStateLoading value)? loading,
    TResult Function(ProfilePageBlocStateLoaded value)? loaded,
    TResult Function(ProfilePageBlocStateLoadError value)? loadError,
    required TResult orElse(),
  }) {
    if (loadError != null) {
      return loadError(this);
    }
    return orElse();
  }
}

abstract class ProfilePageBlocStateLoadError implements ProfilePageBlocState {
  const factory ProfilePageBlocStateLoadError(final String error) =
      _$ProfilePageBlocStateLoadErrorImpl;

  String get error;
  @JsonKey(ignore: true)
  _$$ProfilePageBlocStateLoadErrorImplCopyWith<
          _$ProfilePageBlocStateLoadErrorImpl>
      get copyWith => throw _privateConstructorUsedError;
}
