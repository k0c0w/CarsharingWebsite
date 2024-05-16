// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'state.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

/// @nodoc
mixin _$ActiveSubscriptionsState {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(List<BookedCar> cars, String? error) loaded,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(List<BookedCar> cars, String? error)? loaded,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(List<BookedCar> cars, String? error)? loaded,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(ActiveSubscriptionsLoadedState value) loaded,
    required TResult Function(ActiveSubscriptionsLoadErrorState value)
        loadError,
    required TResult Function(ActiveSubscriptionsLoadingState value) loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult? Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult? Function(ActiveSubscriptionsLoadingState value)? loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult Function(ActiveSubscriptionsLoadingState value)? loading,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $ActiveSubscriptionsStateCopyWith<$Res> {
  factory $ActiveSubscriptionsStateCopyWith(ActiveSubscriptionsState value,
          $Res Function(ActiveSubscriptionsState) then) =
      _$ActiveSubscriptionsStateCopyWithImpl<$Res, ActiveSubscriptionsState>;
}

/// @nodoc
class _$ActiveSubscriptionsStateCopyWithImpl<$Res,
        $Val extends ActiveSubscriptionsState>
    implements $ActiveSubscriptionsStateCopyWith<$Res> {
  _$ActiveSubscriptionsStateCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$ActiveSubscriptionsLoadedStateImplCopyWith<$Res> {
  factory _$$ActiveSubscriptionsLoadedStateImplCopyWith(
          _$ActiveSubscriptionsLoadedStateImpl value,
          $Res Function(_$ActiveSubscriptionsLoadedStateImpl) then) =
      __$$ActiveSubscriptionsLoadedStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({List<BookedCar> cars, String? error});
}

/// @nodoc
class __$$ActiveSubscriptionsLoadedStateImplCopyWithImpl<$Res>
    extends _$ActiveSubscriptionsStateCopyWithImpl<$Res,
        _$ActiveSubscriptionsLoadedStateImpl>
    implements _$$ActiveSubscriptionsLoadedStateImplCopyWith<$Res> {
  __$$ActiveSubscriptionsLoadedStateImplCopyWithImpl(
      _$ActiveSubscriptionsLoadedStateImpl _value,
      $Res Function(_$ActiveSubscriptionsLoadedStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? cars = null,
    Object? error = freezed,
  }) {
    return _then(_$ActiveSubscriptionsLoadedStateImpl(
      cars: null == cars
          ? _value._cars
          : cars // ignore: cast_nullable_to_non_nullable
              as List<BookedCar>,
      error: freezed == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$ActiveSubscriptionsLoadedStateImpl
    implements ActiveSubscriptionsLoadedState {
  const _$ActiveSubscriptionsLoadedStateImpl(
      {required final List<BookedCar> cars, this.error})
      : _cars = cars;

  final List<BookedCar> _cars;
  @override
  List<BookedCar> get cars {
    if (_cars is EqualUnmodifiableListView) return _cars;
    // ignore: implicit_dynamic_type
    return EqualUnmodifiableListView(_cars);
  }

  @override
  final String? error;

  @override
  String toString() {
    return 'ActiveSubscriptionsState.loaded(cars: $cars, error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ActiveSubscriptionsLoadedStateImpl &&
            const DeepCollectionEquality().equals(other._cars, _cars) &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(
      runtimeType, const DeepCollectionEquality().hash(_cars), error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ActiveSubscriptionsLoadedStateImplCopyWith<
          _$ActiveSubscriptionsLoadedStateImpl>
      get copyWith => __$$ActiveSubscriptionsLoadedStateImplCopyWithImpl<
          _$ActiveSubscriptionsLoadedStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(List<BookedCar> cars, String? error) loaded,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return loaded(cars, error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(List<BookedCar> cars, String? error)? loaded,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return loaded?.call(cars, error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(List<BookedCar> cars, String? error)? loaded,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(cars, error);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(ActiveSubscriptionsLoadedState value) loaded,
    required TResult Function(ActiveSubscriptionsLoadErrorState value)
        loadError,
    required TResult Function(ActiveSubscriptionsLoadingState value) loading,
  }) {
    return loaded(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult? Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult? Function(ActiveSubscriptionsLoadingState value)? loading,
  }) {
    return loaded?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult Function(ActiveSubscriptionsLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(this);
    }
    return orElse();
  }
}

abstract class ActiveSubscriptionsLoadedState
    implements ActiveSubscriptionsState {
  const factory ActiveSubscriptionsLoadedState(
      {required final List<BookedCar> cars,
      final String? error}) = _$ActiveSubscriptionsLoadedStateImpl;

  List<BookedCar> get cars;
  String? get error;
  @JsonKey(ignore: true)
  _$$ActiveSubscriptionsLoadedStateImplCopyWith<
          _$ActiveSubscriptionsLoadedStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$ActiveSubscriptionsLoadErrorStateImplCopyWith<$Res> {
  factory _$$ActiveSubscriptionsLoadErrorStateImplCopyWith(
          _$ActiveSubscriptionsLoadErrorStateImpl value,
          $Res Function(_$ActiveSubscriptionsLoadErrorStateImpl) then) =
      __$$ActiveSubscriptionsLoadErrorStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String? error});
}

/// @nodoc
class __$$ActiveSubscriptionsLoadErrorStateImplCopyWithImpl<$Res>
    extends _$ActiveSubscriptionsStateCopyWithImpl<$Res,
        _$ActiveSubscriptionsLoadErrorStateImpl>
    implements _$$ActiveSubscriptionsLoadErrorStateImplCopyWith<$Res> {
  __$$ActiveSubscriptionsLoadErrorStateImplCopyWithImpl(
      _$ActiveSubscriptionsLoadErrorStateImpl _value,
      $Res Function(_$ActiveSubscriptionsLoadErrorStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? error = freezed,
  }) {
    return _then(_$ActiveSubscriptionsLoadErrorStateImpl(
      error: freezed == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$ActiveSubscriptionsLoadErrorStateImpl
    implements ActiveSubscriptionsLoadErrorState {
  const _$ActiveSubscriptionsLoadErrorStateImpl({this.error});

  @override
  final String? error;

  @override
  String toString() {
    return 'ActiveSubscriptionsState.loadError(error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ActiveSubscriptionsLoadErrorStateImpl &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ActiveSubscriptionsLoadErrorStateImplCopyWith<
          _$ActiveSubscriptionsLoadErrorStateImpl>
      get copyWith => __$$ActiveSubscriptionsLoadErrorStateImplCopyWithImpl<
          _$ActiveSubscriptionsLoadErrorStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(List<BookedCar> cars, String? error) loaded,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return loadError(error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(List<BookedCar> cars, String? error)? loaded,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return loadError?.call(error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(List<BookedCar> cars, String? error)? loaded,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
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
    required TResult Function(ActiveSubscriptionsLoadedState value) loaded,
    required TResult Function(ActiveSubscriptionsLoadErrorState value)
        loadError,
    required TResult Function(ActiveSubscriptionsLoadingState value) loading,
  }) {
    return loadError(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult? Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult? Function(ActiveSubscriptionsLoadingState value)? loading,
  }) {
    return loadError?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult Function(ActiveSubscriptionsLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (loadError != null) {
      return loadError(this);
    }
    return orElse();
  }
}

abstract class ActiveSubscriptionsLoadErrorState
    implements ActiveSubscriptionsState {
  const factory ActiveSubscriptionsLoadErrorState({final String? error}) =
      _$ActiveSubscriptionsLoadErrorStateImpl;

  String? get error;
  @JsonKey(ignore: true)
  _$$ActiveSubscriptionsLoadErrorStateImplCopyWith<
          _$ActiveSubscriptionsLoadErrorStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$ActiveSubscriptionsLoadingStateImplCopyWith<$Res> {
  factory _$$ActiveSubscriptionsLoadingStateImplCopyWith(
          _$ActiveSubscriptionsLoadingStateImpl value,
          $Res Function(_$ActiveSubscriptionsLoadingStateImpl) then) =
      __$$ActiveSubscriptionsLoadingStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$ActiveSubscriptionsLoadingStateImplCopyWithImpl<$Res>
    extends _$ActiveSubscriptionsStateCopyWithImpl<$Res,
        _$ActiveSubscriptionsLoadingStateImpl>
    implements _$$ActiveSubscriptionsLoadingStateImplCopyWith<$Res> {
  __$$ActiveSubscriptionsLoadingStateImplCopyWithImpl(
      _$ActiveSubscriptionsLoadingStateImpl _value,
      $Res Function(_$ActiveSubscriptionsLoadingStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$ActiveSubscriptionsLoadingStateImpl
    implements ActiveSubscriptionsLoadingState {
  const _$ActiveSubscriptionsLoadingStateImpl();

  @override
  String toString() {
    return 'ActiveSubscriptionsState.loading()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ActiveSubscriptionsLoadingStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(List<BookedCar> cars, String? error) loaded,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return loading();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(List<BookedCar> cars, String? error)? loaded,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return loading?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(List<BookedCar> cars, String? error)? loaded,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
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
    required TResult Function(ActiveSubscriptionsLoadedState value) loaded,
    required TResult Function(ActiveSubscriptionsLoadErrorState value)
        loadError,
    required TResult Function(ActiveSubscriptionsLoadingState value) loading,
  }) {
    return loading(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult? Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult? Function(ActiveSubscriptionsLoadingState value)? loading,
  }) {
    return loading?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(ActiveSubscriptionsLoadedState value)? loaded,
    TResult Function(ActiveSubscriptionsLoadErrorState value)? loadError,
    TResult Function(ActiveSubscriptionsLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (loading != null) {
      return loading(this);
    }
    return orElse();
  }
}

abstract class ActiveSubscriptionsLoadingState
    implements ActiveSubscriptionsState {
  const factory ActiveSubscriptionsLoadingState() =
      _$ActiveSubscriptionsLoadingStateImpl;
}
