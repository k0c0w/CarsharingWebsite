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
mixin _$HomePageBlocState {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $HomePageBlocStateCopyWith<$Res> {
  factory $HomePageBlocStateCopyWith(
          HomePageBlocState value, $Res Function(HomePageBlocState) then) =
      _$HomePageBlocStateCopyWithImpl<$Res, HomePageBlocState>;
}

/// @nodoc
class _$HomePageBlocStateCopyWithImpl<$Res, $Val extends HomePageBlocState>
    implements $HomePageBlocStateCopyWith<$Res> {
  _$HomePageBlocStateCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$HomePageBlocLoadedStateImplCopyWith<$Res> {
  factory _$$HomePageBlocLoadedStateImplCopyWith(
          _$HomePageBlocLoadedStateImpl value,
          $Res Function(_$HomePageBlocLoadedStateImpl) then) =
      __$$HomePageBlocLoadedStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call(
      {List<Car> cars,
      List<Tariff> tariffs,
      MapSearchArea mapAnchor,
      int? selectedTariffIndex,
      int? selectedCarId});

  $MapSearchAreaCopyWith<$Res> get mapAnchor;
}

/// @nodoc
class __$$HomePageBlocLoadedStateImplCopyWithImpl<$Res>
    extends _$HomePageBlocStateCopyWithImpl<$Res, _$HomePageBlocLoadedStateImpl>
    implements _$$HomePageBlocLoadedStateImplCopyWith<$Res> {
  __$$HomePageBlocLoadedStateImplCopyWithImpl(
      _$HomePageBlocLoadedStateImpl _value,
      $Res Function(_$HomePageBlocLoadedStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? cars = null,
    Object? tariffs = null,
    Object? mapAnchor = null,
    Object? selectedTariffIndex = freezed,
    Object? selectedCarId = freezed,
  }) {
    return _then(_$HomePageBlocLoadedStateImpl(
      cars: null == cars
          ? _value._cars
          : cars // ignore: cast_nullable_to_non_nullable
              as List<Car>,
      tariffs: null == tariffs
          ? _value._tariffs
          : tariffs // ignore: cast_nullable_to_non_nullable
              as List<Tariff>,
      mapAnchor: null == mapAnchor
          ? _value.mapAnchor
          : mapAnchor // ignore: cast_nullable_to_non_nullable
              as MapSearchArea,
      selectedTariffIndex: freezed == selectedTariffIndex
          ? _value.selectedTariffIndex
          : selectedTariffIndex // ignore: cast_nullable_to_non_nullable
              as int?,
      selectedCarId: freezed == selectedCarId
          ? _value.selectedCarId
          : selectedCarId // ignore: cast_nullable_to_non_nullable
              as int?,
    ));
  }

  @override
  @pragma('vm:prefer-inline')
  $MapSearchAreaCopyWith<$Res> get mapAnchor {
    return $MapSearchAreaCopyWith<$Res>(_value.mapAnchor, (value) {
      return _then(_value.copyWith(mapAnchor: value));
    });
  }
}

/// @nodoc

class _$HomePageBlocLoadedStateImpl implements HomePageBlocLoadedState {
  const _$HomePageBlocLoadedStateImpl(
      {required final List<Car> cars,
      required final List<Tariff> tariffs,
      required this.mapAnchor,
      required this.selectedTariffIndex,
      this.selectedCarId})
      : _cars = cars,
        _tariffs = tariffs;

  final List<Car> _cars;
  @override
  List<Car> get cars {
    if (_cars is EqualUnmodifiableListView) return _cars;
    // ignore: implicit_dynamic_type
    return EqualUnmodifiableListView(_cars);
  }

  final List<Tariff> _tariffs;
  @override
  List<Tariff> get tariffs {
    if (_tariffs is EqualUnmodifiableListView) return _tariffs;
    // ignore: implicit_dynamic_type
    return EqualUnmodifiableListView(_tariffs);
  }

  @override
  final MapSearchArea mapAnchor;
  @override
  final int? selectedTariffIndex;
  @override
  final int? selectedCarId;

  @override
  String toString() {
    return 'HomePageBlocState.loaded(cars: $cars, tariffs: $tariffs, mapAnchor: $mapAnchor, selectedTariffIndex: $selectedTariffIndex, selectedCarId: $selectedCarId)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$HomePageBlocLoadedStateImpl &&
            const DeepCollectionEquality().equals(other._cars, _cars) &&
            const DeepCollectionEquality().equals(other._tariffs, _tariffs) &&
            (identical(other.mapAnchor, mapAnchor) ||
                other.mapAnchor == mapAnchor) &&
            (identical(other.selectedTariffIndex, selectedTariffIndex) ||
                other.selectedTariffIndex == selectedTariffIndex) &&
            (identical(other.selectedCarId, selectedCarId) ||
                other.selectedCarId == selectedCarId));
  }

  @override
  int get hashCode => Object.hash(
      runtimeType,
      const DeepCollectionEquality().hash(_cars),
      const DeepCollectionEquality().hash(_tariffs),
      mapAnchor,
      selectedTariffIndex,
      selectedCarId);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$HomePageBlocLoadedStateImplCopyWith<_$HomePageBlocLoadedStateImpl>
      get copyWith => __$$HomePageBlocLoadedStateImplCopyWithImpl<
          _$HomePageBlocLoadedStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return loaded(cars, tariffs, mapAnchor, selectedTariffIndex, selectedCarId);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return loaded?.call(
        cars, tariffs, mapAnchor, selectedTariffIndex, selectedCarId);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(
          cars, tariffs, mapAnchor, selectedTariffIndex, selectedCarId);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) {
    return loaded(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) {
    return loaded?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(this);
    }
    return orElse();
  }
}

abstract class HomePageBlocLoadedState implements HomePageBlocState {
  const factory HomePageBlocLoadedState(
      {required final List<Car> cars,
      required final List<Tariff> tariffs,
      required final MapSearchArea mapAnchor,
      required final int? selectedTariffIndex,
      final int? selectedCarId}) = _$HomePageBlocLoadedStateImpl;

  List<Car> get cars;
  List<Tariff> get tariffs;
  MapSearchArea get mapAnchor;
  int? get selectedTariffIndex;
  int? get selectedCarId;
  @JsonKey(ignore: true)
  _$$HomePageBlocLoadedStateImplCopyWith<_$HomePageBlocLoadedStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$HomePageBlocRentingStateImplCopyWith<$Res> {
  factory _$$HomePageBlocRentingStateImplCopyWith(
          _$HomePageBlocRentingStateImpl value,
          $Res Function(_$HomePageBlocRentingStateImpl) then) =
      __$$HomePageBlocRentingStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$HomePageBlocRentingStateImplCopyWithImpl<$Res>
    extends _$HomePageBlocStateCopyWithImpl<$Res,
        _$HomePageBlocRentingStateImpl>
    implements _$$HomePageBlocRentingStateImplCopyWith<$Res> {
  __$$HomePageBlocRentingStateImplCopyWithImpl(
      _$HomePageBlocRentingStateImpl _value,
      $Res Function(_$HomePageBlocRentingStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$HomePageBlocRentingStateImpl implements HomePageBlocRentingState {
  const _$HomePageBlocRentingStateImpl();

  @override
  String toString() {
    return 'HomePageBlocState.renting()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$HomePageBlocRentingStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return renting();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return renting?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) {
    if (renting != null) {
      return renting();
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) {
    return renting(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) {
    return renting?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (renting != null) {
      return renting(this);
    }
    return orElse();
  }
}

abstract class HomePageBlocRentingState implements HomePageBlocState {
  const factory HomePageBlocRentingState() = _$HomePageBlocRentingStateImpl;
}

/// @nodoc
abstract class _$$HomePageBlocSuccessfulRentStateImplCopyWith<$Res> {
  factory _$$HomePageBlocSuccessfulRentStateImplCopyWith(
          _$HomePageBlocSuccessfulRentStateImpl value,
          $Res Function(_$HomePageBlocSuccessfulRentStateImpl) then) =
      __$$HomePageBlocSuccessfulRentStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$HomePageBlocSuccessfulRentStateImplCopyWithImpl<$Res>
    extends _$HomePageBlocStateCopyWithImpl<$Res,
        _$HomePageBlocSuccessfulRentStateImpl>
    implements _$$HomePageBlocSuccessfulRentStateImplCopyWith<$Res> {
  __$$HomePageBlocSuccessfulRentStateImplCopyWithImpl(
      _$HomePageBlocSuccessfulRentStateImpl _value,
      $Res Function(_$HomePageBlocSuccessfulRentStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$HomePageBlocSuccessfulRentStateImpl
    implements HomePageBlocSuccessfulRentState {
  const _$HomePageBlocSuccessfulRentStateImpl();

  @override
  String toString() {
    return 'HomePageBlocState.successfulRent()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$HomePageBlocSuccessfulRentStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return successfulRent();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return successfulRent?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) {
    if (successfulRent != null) {
      return successfulRent();
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) {
    return successfulRent(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) {
    return successfulRent?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (successfulRent != null) {
      return successfulRent(this);
    }
    return orElse();
  }
}

abstract class HomePageBlocSuccessfulRentState implements HomePageBlocState {
  const factory HomePageBlocSuccessfulRentState() =
      _$HomePageBlocSuccessfulRentStateImpl;
}

/// @nodoc
abstract class _$$HomePageBlocUnsuccessfulRentStateImplCopyWith<$Res> {
  factory _$$HomePageBlocUnsuccessfulRentStateImplCopyWith(
          _$HomePageBlocUnsuccessfulRentStateImpl value,
          $Res Function(_$HomePageBlocUnsuccessfulRentStateImpl) then) =
      __$$HomePageBlocUnsuccessfulRentStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String? error});
}

/// @nodoc
class __$$HomePageBlocUnsuccessfulRentStateImplCopyWithImpl<$Res>
    extends _$HomePageBlocStateCopyWithImpl<$Res,
        _$HomePageBlocUnsuccessfulRentStateImpl>
    implements _$$HomePageBlocUnsuccessfulRentStateImplCopyWith<$Res> {
  __$$HomePageBlocUnsuccessfulRentStateImplCopyWithImpl(
      _$HomePageBlocUnsuccessfulRentStateImpl _value,
      $Res Function(_$HomePageBlocUnsuccessfulRentStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? error = freezed,
  }) {
    return _then(_$HomePageBlocUnsuccessfulRentStateImpl(
      error: freezed == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$HomePageBlocUnsuccessfulRentStateImpl
    implements HomePageBlocUnsuccessfulRentState {
  const _$HomePageBlocUnsuccessfulRentStateImpl({this.error});

  @override
  final String? error;

  @override
  String toString() {
    return 'HomePageBlocState.unsuccessfulRent(error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$HomePageBlocUnsuccessfulRentStateImpl &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$HomePageBlocUnsuccessfulRentStateImplCopyWith<
          _$HomePageBlocUnsuccessfulRentStateImpl>
      get copyWith => __$$HomePageBlocUnsuccessfulRentStateImplCopyWithImpl<
          _$HomePageBlocUnsuccessfulRentStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return unsuccessfulRent(error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return unsuccessfulRent?.call(error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
    TResult Function(String? error)? loadError,
    TResult Function()? loading,
    required TResult orElse(),
  }) {
    if (unsuccessfulRent != null) {
      return unsuccessfulRent(error);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) {
    return unsuccessfulRent(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) {
    return unsuccessfulRent?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (unsuccessfulRent != null) {
      return unsuccessfulRent(this);
    }
    return orElse();
  }
}

abstract class HomePageBlocUnsuccessfulRentState implements HomePageBlocState {
  const factory HomePageBlocUnsuccessfulRentState({final String? error}) =
      _$HomePageBlocUnsuccessfulRentStateImpl;

  String? get error;
  @JsonKey(ignore: true)
  _$$HomePageBlocUnsuccessfulRentStateImplCopyWith<
          _$HomePageBlocUnsuccessfulRentStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$HomePageBlocLoadErrorStateImplCopyWith<$Res> {
  factory _$$HomePageBlocLoadErrorStateImplCopyWith(
          _$HomePageBlocLoadErrorStateImpl value,
          $Res Function(_$HomePageBlocLoadErrorStateImpl) then) =
      __$$HomePageBlocLoadErrorStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String? error});
}

/// @nodoc
class __$$HomePageBlocLoadErrorStateImplCopyWithImpl<$Res>
    extends _$HomePageBlocStateCopyWithImpl<$Res,
        _$HomePageBlocLoadErrorStateImpl>
    implements _$$HomePageBlocLoadErrorStateImplCopyWith<$Res> {
  __$$HomePageBlocLoadErrorStateImplCopyWithImpl(
      _$HomePageBlocLoadErrorStateImpl _value,
      $Res Function(_$HomePageBlocLoadErrorStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? error = freezed,
  }) {
    return _then(_$HomePageBlocLoadErrorStateImpl(
      error: freezed == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$HomePageBlocLoadErrorStateImpl implements HomePageBlocLoadErrorState {
  const _$HomePageBlocLoadErrorStateImpl({this.error});

  @override
  final String? error;

  @override
  String toString() {
    return 'HomePageBlocState.loadError(error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$HomePageBlocLoadErrorStateImpl &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$HomePageBlocLoadErrorStateImplCopyWith<_$HomePageBlocLoadErrorStateImpl>
      get copyWith => __$$HomePageBlocLoadErrorStateImplCopyWithImpl<
          _$HomePageBlocLoadErrorStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return loadError(error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return loadError?.call(error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
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
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) {
    return loadError(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) {
    return loadError?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (loadError != null) {
      return loadError(this);
    }
    return orElse();
  }
}

abstract class HomePageBlocLoadErrorState implements HomePageBlocState {
  const factory HomePageBlocLoadErrorState({final String? error}) =
      _$HomePageBlocLoadErrorStateImpl;

  String? get error;
  @JsonKey(ignore: true)
  _$$HomePageBlocLoadErrorStateImplCopyWith<_$HomePageBlocLoadErrorStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$HomePageBlocLoadingStateImplCopyWith<$Res> {
  factory _$$HomePageBlocLoadingStateImplCopyWith(
          _$HomePageBlocLoadingStateImpl value,
          $Res Function(_$HomePageBlocLoadingStateImpl) then) =
      __$$HomePageBlocLoadingStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$HomePageBlocLoadingStateImplCopyWithImpl<$Res>
    extends _$HomePageBlocStateCopyWithImpl<$Res,
        _$HomePageBlocLoadingStateImpl>
    implements _$$HomePageBlocLoadingStateImplCopyWith<$Res> {
  __$$HomePageBlocLoadingStateImplCopyWithImpl(
      _$HomePageBlocLoadingStateImpl _value,
      $Res Function(_$HomePageBlocLoadingStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$HomePageBlocLoadingStateImpl implements HomePageBlocLoadingState {
  const _$HomePageBlocLoadingStateImpl();

  @override
  String toString() {
    return 'HomePageBlocState.loading()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$HomePageBlocLoadingStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)
        loaded,
    required TResult Function() renting,
    required TResult Function() successfulRent,
    required TResult Function(String? error) unsuccessfulRent,
    required TResult Function(String? error) loadError,
    required TResult Function() loading,
  }) {
    return loading();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult? Function()? renting,
    TResult? Function()? successfulRent,
    TResult? Function(String? error)? unsuccessfulRent,
    TResult? Function(String? error)? loadError,
    TResult? Function()? loading,
  }) {
    return loading?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function(
            List<Car> cars,
            List<Tariff> tariffs,
            MapSearchArea mapAnchor,
            int? selectedTariffIndex,
            int? selectedCarId)?
        loaded,
    TResult Function()? renting,
    TResult Function()? successfulRent,
    TResult Function(String? error)? unsuccessfulRent,
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
    required TResult Function(HomePageBlocLoadedState value) loaded,
    required TResult Function(HomePageBlocRentingState value) renting,
    required TResult Function(HomePageBlocSuccessfulRentState value)
        successfulRent,
    required TResult Function(HomePageBlocUnsuccessfulRentState value)
        unsuccessfulRent,
    required TResult Function(HomePageBlocLoadErrorState value) loadError,
    required TResult Function(HomePageBlocLoadingState value) loading,
  }) {
    return loading(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(HomePageBlocLoadedState value)? loaded,
    TResult? Function(HomePageBlocRentingState value)? renting,
    TResult? Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult? Function(HomePageBlocUnsuccessfulRentState value)?
        unsuccessfulRent,
    TResult? Function(HomePageBlocLoadErrorState value)? loadError,
    TResult? Function(HomePageBlocLoadingState value)? loading,
  }) {
    return loading?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(HomePageBlocLoadedState value)? loaded,
    TResult Function(HomePageBlocRentingState value)? renting,
    TResult Function(HomePageBlocSuccessfulRentState value)? successfulRent,
    TResult Function(HomePageBlocUnsuccessfulRentState value)? unsuccessfulRent,
    TResult Function(HomePageBlocLoadErrorState value)? loadError,
    TResult Function(HomePageBlocLoadingState value)? loading,
    required TResult orElse(),
  }) {
    if (loading != null) {
      return loading(this);
    }
    return orElse();
  }
}

abstract class HomePageBlocLoadingState implements HomePageBlocState {
  const factory HomePageBlocLoadingState() = _$HomePageBlocLoadingStateImpl;
}
