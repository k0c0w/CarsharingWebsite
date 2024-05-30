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
mixin _$DrawerBlocState {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function() error,
    required TResult Function(
            String nameAndSecondName, String accountConfirmedTitle)
        loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function()? error,
    TResult? Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function()? error,
    TResult Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadingState value) loading,
    required TResult Function(DrawerBlocLoadErrorState value) error,
    required TResult Function(DrawerBlocLoadedState value) loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadingState value)? loading,
    TResult? Function(DrawerBlocLoadErrorState value)? error,
    TResult? Function(DrawerBlocLoadedState value)? loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadingState value)? loading,
    TResult Function(DrawerBlocLoadErrorState value)? error,
    TResult Function(DrawerBlocLoadedState value)? loaded,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $DrawerBlocStateCopyWith<$Res> {
  factory $DrawerBlocStateCopyWith(
          DrawerBlocState value, $Res Function(DrawerBlocState) then) =
      _$DrawerBlocStateCopyWithImpl<$Res, DrawerBlocState>;
}

/// @nodoc
class _$DrawerBlocStateCopyWithImpl<$Res, $Val extends DrawerBlocState>
    implements $DrawerBlocStateCopyWith<$Res> {
  _$DrawerBlocStateCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$DrawerBlocLoadingStateImplCopyWith<$Res> {
  factory _$$DrawerBlocLoadingStateImplCopyWith(
          _$DrawerBlocLoadingStateImpl value,
          $Res Function(_$DrawerBlocLoadingStateImpl) then) =
      __$$DrawerBlocLoadingStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$DrawerBlocLoadingStateImplCopyWithImpl<$Res>
    extends _$DrawerBlocStateCopyWithImpl<$Res, _$DrawerBlocLoadingStateImpl>
    implements _$$DrawerBlocLoadingStateImplCopyWith<$Res> {
  __$$DrawerBlocLoadingStateImplCopyWithImpl(
      _$DrawerBlocLoadingStateImpl _value,
      $Res Function(_$DrawerBlocLoadingStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$DrawerBlocLoadingStateImpl implements DrawerBlocLoadingState {
  const _$DrawerBlocLoadingStateImpl();

  @override
  String toString() {
    return 'DrawerBlocState.loading()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerBlocLoadingStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function() error,
    required TResult Function(
            String nameAndSecondName, String accountConfirmedTitle)
        loaded,
  }) {
    return loading();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function()? error,
    TResult? Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
  }) {
    return loading?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function()? error,
    TResult Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
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
    required TResult Function(DrawerBlocLoadingState value) loading,
    required TResult Function(DrawerBlocLoadErrorState value) error,
    required TResult Function(DrawerBlocLoadedState value) loaded,
  }) {
    return loading(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadingState value)? loading,
    TResult? Function(DrawerBlocLoadErrorState value)? error,
    TResult? Function(DrawerBlocLoadedState value)? loaded,
  }) {
    return loading?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadingState value)? loading,
    TResult Function(DrawerBlocLoadErrorState value)? error,
    TResult Function(DrawerBlocLoadedState value)? loaded,
    required TResult orElse(),
  }) {
    if (loading != null) {
      return loading(this);
    }
    return orElse();
  }
}

abstract class DrawerBlocLoadingState implements DrawerBlocState {
  const factory DrawerBlocLoadingState() = _$DrawerBlocLoadingStateImpl;
}

/// @nodoc
abstract class _$$DrawerBlocLoadErrorStateImplCopyWith<$Res> {
  factory _$$DrawerBlocLoadErrorStateImplCopyWith(
          _$DrawerBlocLoadErrorStateImpl value,
          $Res Function(_$DrawerBlocLoadErrorStateImpl) then) =
      __$$DrawerBlocLoadErrorStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$DrawerBlocLoadErrorStateImplCopyWithImpl<$Res>
    extends _$DrawerBlocStateCopyWithImpl<$Res, _$DrawerBlocLoadErrorStateImpl>
    implements _$$DrawerBlocLoadErrorStateImplCopyWith<$Res> {
  __$$DrawerBlocLoadErrorStateImplCopyWithImpl(
      _$DrawerBlocLoadErrorStateImpl _value,
      $Res Function(_$DrawerBlocLoadErrorStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$DrawerBlocLoadErrorStateImpl implements DrawerBlocLoadErrorState {
  const _$DrawerBlocLoadErrorStateImpl();

  @override
  String toString() {
    return 'DrawerBlocState.error()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerBlocLoadErrorStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function() error,
    required TResult Function(
            String nameAndSecondName, String accountConfirmedTitle)
        loaded,
  }) {
    return error();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function()? error,
    TResult? Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
  }) {
    return error?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function()? error,
    TResult Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
    required TResult orElse(),
  }) {
    if (error != null) {
      return error();
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadingState value) loading,
    required TResult Function(DrawerBlocLoadErrorState value) error,
    required TResult Function(DrawerBlocLoadedState value) loaded,
  }) {
    return error(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadingState value)? loading,
    TResult? Function(DrawerBlocLoadErrorState value)? error,
    TResult? Function(DrawerBlocLoadedState value)? loaded,
  }) {
    return error?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadingState value)? loading,
    TResult Function(DrawerBlocLoadErrorState value)? error,
    TResult Function(DrawerBlocLoadedState value)? loaded,
    required TResult orElse(),
  }) {
    if (error != null) {
      return error(this);
    }
    return orElse();
  }
}

abstract class DrawerBlocLoadErrorState implements DrawerBlocState {
  const factory DrawerBlocLoadErrorState() = _$DrawerBlocLoadErrorStateImpl;
}

/// @nodoc
abstract class _$$DrawerBlocLoadedStateImplCopyWith<$Res> {
  factory _$$DrawerBlocLoadedStateImplCopyWith(
          _$DrawerBlocLoadedStateImpl value,
          $Res Function(_$DrawerBlocLoadedStateImpl) then) =
      __$$DrawerBlocLoadedStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String nameAndSecondName, String accountConfirmedTitle});
}

/// @nodoc
class __$$DrawerBlocLoadedStateImplCopyWithImpl<$Res>
    extends _$DrawerBlocStateCopyWithImpl<$Res, _$DrawerBlocLoadedStateImpl>
    implements _$$DrawerBlocLoadedStateImplCopyWith<$Res> {
  __$$DrawerBlocLoadedStateImplCopyWithImpl(_$DrawerBlocLoadedStateImpl _value,
      $Res Function(_$DrawerBlocLoadedStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? nameAndSecondName = null,
    Object? accountConfirmedTitle = null,
  }) {
    return _then(_$DrawerBlocLoadedStateImpl(
      null == nameAndSecondName
          ? _value.nameAndSecondName
          : nameAndSecondName // ignore: cast_nullable_to_non_nullable
              as String,
      null == accountConfirmedTitle
          ? _value.accountConfirmedTitle
          : accountConfirmedTitle // ignore: cast_nullable_to_non_nullable
              as String,
    ));
  }
}

/// @nodoc

class _$DrawerBlocLoadedStateImpl implements DrawerBlocLoadedState {
  const _$DrawerBlocLoadedStateImpl(
      this.nameAndSecondName, this.accountConfirmedTitle);

  @override
  final String nameAndSecondName;
  @override
  final String accountConfirmedTitle;

  @override
  String toString() {
    return 'DrawerBlocState.loaded(nameAndSecondName: $nameAndSecondName, accountConfirmedTitle: $accountConfirmedTitle)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerBlocLoadedStateImpl &&
            (identical(other.nameAndSecondName, nameAndSecondName) ||
                other.nameAndSecondName == nameAndSecondName) &&
            (identical(other.accountConfirmedTitle, accountConfirmedTitle) ||
                other.accountConfirmedTitle == accountConfirmedTitle));
  }

  @override
  int get hashCode =>
      Object.hash(runtimeType, nameAndSecondName, accountConfirmedTitle);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$DrawerBlocLoadedStateImplCopyWith<_$DrawerBlocLoadedStateImpl>
      get copyWith => __$$DrawerBlocLoadedStateImplCopyWithImpl<
          _$DrawerBlocLoadedStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function() error,
    required TResult Function(
            String nameAndSecondName, String accountConfirmedTitle)
        loaded,
  }) {
    return loaded(nameAndSecondName, accountConfirmedTitle);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function()? error,
    TResult? Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
  }) {
    return loaded?.call(nameAndSecondName, accountConfirmedTitle);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function()? error,
    TResult Function(String nameAndSecondName, String accountConfirmedTitle)?
        loaded,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(nameAndSecondName, accountConfirmedTitle);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadingState value) loading,
    required TResult Function(DrawerBlocLoadErrorState value) error,
    required TResult Function(DrawerBlocLoadedState value) loaded,
  }) {
    return loaded(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadingState value)? loading,
    TResult? Function(DrawerBlocLoadErrorState value)? error,
    TResult? Function(DrawerBlocLoadedState value)? loaded,
  }) {
    return loaded?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadingState value)? loading,
    TResult Function(DrawerBlocLoadErrorState value)? error,
    TResult Function(DrawerBlocLoadedState value)? loaded,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(this);
    }
    return orElse();
  }
}

abstract class DrawerBlocLoadedState implements DrawerBlocState {
  const factory DrawerBlocLoadedState(
          final String nameAndSecondName, final String accountConfirmedTitle) =
      _$DrawerBlocLoadedStateImpl;

  String get nameAndSecondName;
  String get accountConfirmedTitle;
  @JsonKey(ignore: true)
  _$$DrawerBlocLoadedStateImplCopyWith<_$DrawerBlocLoadedStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}
