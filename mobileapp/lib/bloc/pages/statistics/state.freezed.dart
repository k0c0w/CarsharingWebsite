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
mixin _$StatisticsState {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() subscribing,
    required TResult Function(String? error) subscriptionError,
    required TResult Function(List<TariffUsageStats> stats) received,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? subscribing,
    TResult? Function(String? error)? subscriptionError,
    TResult? Function(List<TariffUsageStats> stats)? received,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? subscribing,
    TResult Function(String? error)? subscriptionError,
    TResult Function(List<TariffUsageStats> stats)? received,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(_StatisticsSubscribingState value) subscribing,
    required TResult Function(_StatisticsSubscriptionErrorState value)
        subscriptionError,
    required TResult Function(_StatisticsReceivedState value) received,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_StatisticsSubscribingState value)? subscribing,
    TResult? Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult? Function(_StatisticsReceivedState value)? received,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_StatisticsSubscribingState value)? subscribing,
    TResult Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult Function(_StatisticsReceivedState value)? received,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $StatisticsStateCopyWith<$Res> {
  factory $StatisticsStateCopyWith(
          StatisticsState value, $Res Function(StatisticsState) then) =
      _$StatisticsStateCopyWithImpl<$Res, StatisticsState>;
}

/// @nodoc
class _$StatisticsStateCopyWithImpl<$Res, $Val extends StatisticsState>
    implements $StatisticsStateCopyWith<$Res> {
  _$StatisticsStateCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$StatisticsSubscribingStateImplCopyWith<$Res> {
  factory _$$StatisticsSubscribingStateImplCopyWith(
          _$StatisticsSubscribingStateImpl value,
          $Res Function(_$StatisticsSubscribingStateImpl) then) =
      __$$StatisticsSubscribingStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$StatisticsSubscribingStateImplCopyWithImpl<$Res>
    extends _$StatisticsStateCopyWithImpl<$Res,
        _$StatisticsSubscribingStateImpl>
    implements _$$StatisticsSubscribingStateImplCopyWith<$Res> {
  __$$StatisticsSubscribingStateImplCopyWithImpl(
      _$StatisticsSubscribingStateImpl _value,
      $Res Function(_$StatisticsSubscribingStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$StatisticsSubscribingStateImpl implements _StatisticsSubscribingState {
  const _$StatisticsSubscribingStateImpl();

  @override
  String toString() {
    return 'StatisticsState.subscribing()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$StatisticsSubscribingStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() subscribing,
    required TResult Function(String? error) subscriptionError,
    required TResult Function(List<TariffUsageStats> stats) received,
  }) {
    return subscribing();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? subscribing,
    TResult? Function(String? error)? subscriptionError,
    TResult? Function(List<TariffUsageStats> stats)? received,
  }) {
    return subscribing?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? subscribing,
    TResult Function(String? error)? subscriptionError,
    TResult Function(List<TariffUsageStats> stats)? received,
    required TResult orElse(),
  }) {
    if (subscribing != null) {
      return subscribing();
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(_StatisticsSubscribingState value) subscribing,
    required TResult Function(_StatisticsSubscriptionErrorState value)
        subscriptionError,
    required TResult Function(_StatisticsReceivedState value) received,
  }) {
    return subscribing(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_StatisticsSubscribingState value)? subscribing,
    TResult? Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult? Function(_StatisticsReceivedState value)? received,
  }) {
    return subscribing?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_StatisticsSubscribingState value)? subscribing,
    TResult Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult Function(_StatisticsReceivedState value)? received,
    required TResult orElse(),
  }) {
    if (subscribing != null) {
      return subscribing(this);
    }
    return orElse();
  }
}

abstract class _StatisticsSubscribingState implements StatisticsState {
  const factory _StatisticsSubscribingState() =
      _$StatisticsSubscribingStateImpl;
}

/// @nodoc
abstract class _$$StatisticsSubscriptionErrorStateImplCopyWith<$Res> {
  factory _$$StatisticsSubscriptionErrorStateImplCopyWith(
          _$StatisticsSubscriptionErrorStateImpl value,
          $Res Function(_$StatisticsSubscriptionErrorStateImpl) then) =
      __$$StatisticsSubscriptionErrorStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String? error});
}

/// @nodoc
class __$$StatisticsSubscriptionErrorStateImplCopyWithImpl<$Res>
    extends _$StatisticsStateCopyWithImpl<$Res,
        _$StatisticsSubscriptionErrorStateImpl>
    implements _$$StatisticsSubscriptionErrorStateImplCopyWith<$Res> {
  __$$StatisticsSubscriptionErrorStateImplCopyWithImpl(
      _$StatisticsSubscriptionErrorStateImpl _value,
      $Res Function(_$StatisticsSubscriptionErrorStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? error = freezed,
  }) {
    return _then(_$StatisticsSubscriptionErrorStateImpl(
      error: freezed == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$StatisticsSubscriptionErrorStateImpl
    implements _StatisticsSubscriptionErrorState {
  const _$StatisticsSubscriptionErrorStateImpl({this.error});

  @override
  final String? error;

  @override
  String toString() {
    return 'StatisticsState.subscriptionError(error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$StatisticsSubscriptionErrorStateImpl &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$StatisticsSubscriptionErrorStateImplCopyWith<
          _$StatisticsSubscriptionErrorStateImpl>
      get copyWith => __$$StatisticsSubscriptionErrorStateImplCopyWithImpl<
          _$StatisticsSubscriptionErrorStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() subscribing,
    required TResult Function(String? error) subscriptionError,
    required TResult Function(List<TariffUsageStats> stats) received,
  }) {
    return subscriptionError(error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? subscribing,
    TResult? Function(String? error)? subscriptionError,
    TResult? Function(List<TariffUsageStats> stats)? received,
  }) {
    return subscriptionError?.call(error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? subscribing,
    TResult Function(String? error)? subscriptionError,
    TResult Function(List<TariffUsageStats> stats)? received,
    required TResult orElse(),
  }) {
    if (subscriptionError != null) {
      return subscriptionError(error);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(_StatisticsSubscribingState value) subscribing,
    required TResult Function(_StatisticsSubscriptionErrorState value)
        subscriptionError,
    required TResult Function(_StatisticsReceivedState value) received,
  }) {
    return subscriptionError(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_StatisticsSubscribingState value)? subscribing,
    TResult? Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult? Function(_StatisticsReceivedState value)? received,
  }) {
    return subscriptionError?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_StatisticsSubscribingState value)? subscribing,
    TResult Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult Function(_StatisticsReceivedState value)? received,
    required TResult orElse(),
  }) {
    if (subscriptionError != null) {
      return subscriptionError(this);
    }
    return orElse();
  }
}

abstract class _StatisticsSubscriptionErrorState implements StatisticsState {
  const factory _StatisticsSubscriptionErrorState({final String? error}) =
      _$StatisticsSubscriptionErrorStateImpl;

  String? get error;
  @JsonKey(ignore: true)
  _$$StatisticsSubscriptionErrorStateImplCopyWith<
          _$StatisticsSubscriptionErrorStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$StatisticsReceivedStateImplCopyWith<$Res> {
  factory _$$StatisticsReceivedStateImplCopyWith(
          _$StatisticsReceivedStateImpl value,
          $Res Function(_$StatisticsReceivedStateImpl) then) =
      __$$StatisticsReceivedStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({List<TariffUsageStats> stats});
}

/// @nodoc
class __$$StatisticsReceivedStateImplCopyWithImpl<$Res>
    extends _$StatisticsStateCopyWithImpl<$Res, _$StatisticsReceivedStateImpl>
    implements _$$StatisticsReceivedStateImplCopyWith<$Res> {
  __$$StatisticsReceivedStateImplCopyWithImpl(
      _$StatisticsReceivedStateImpl _value,
      $Res Function(_$StatisticsReceivedStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? stats = null,
  }) {
    return _then(_$StatisticsReceivedStateImpl(
      stats: null == stats
          ? _value._stats
          : stats // ignore: cast_nullable_to_non_nullable
              as List<TariffUsageStats>,
    ));
  }
}

/// @nodoc

class _$StatisticsReceivedStateImpl implements _StatisticsReceivedState {
  const _$StatisticsReceivedStateImpl(
      {required final List<TariffUsageStats> stats})
      : _stats = stats;

  final List<TariffUsageStats> _stats;
  @override
  List<TariffUsageStats> get stats {
    if (_stats is EqualUnmodifiableListView) return _stats;
    // ignore: implicit_dynamic_type
    return EqualUnmodifiableListView(_stats);
  }

  @override
  String toString() {
    return 'StatisticsState.received(stats: $stats)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$StatisticsReceivedStateImpl &&
            const DeepCollectionEquality().equals(other._stats, _stats));
  }

  @override
  int get hashCode =>
      Object.hash(runtimeType, const DeepCollectionEquality().hash(_stats));

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$StatisticsReceivedStateImplCopyWith<_$StatisticsReceivedStateImpl>
      get copyWith => __$$StatisticsReceivedStateImplCopyWithImpl<
          _$StatisticsReceivedStateImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() subscribing,
    required TResult Function(String? error) subscriptionError,
    required TResult Function(List<TariffUsageStats> stats) received,
  }) {
    return received(stats);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? subscribing,
    TResult? Function(String? error)? subscriptionError,
    TResult? Function(List<TariffUsageStats> stats)? received,
  }) {
    return received?.call(stats);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? subscribing,
    TResult Function(String? error)? subscriptionError,
    TResult Function(List<TariffUsageStats> stats)? received,
    required TResult orElse(),
  }) {
    if (received != null) {
      return received(stats);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(_StatisticsSubscribingState value) subscribing,
    required TResult Function(_StatisticsSubscriptionErrorState value)
        subscriptionError,
    required TResult Function(_StatisticsReceivedState value) received,
  }) {
    return received(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_StatisticsSubscribingState value)? subscribing,
    TResult? Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult? Function(_StatisticsReceivedState value)? received,
  }) {
    return received?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_StatisticsSubscribingState value)? subscribing,
    TResult Function(_StatisticsSubscriptionErrorState value)?
        subscriptionError,
    TResult Function(_StatisticsReceivedState value)? received,
    required TResult orElse(),
  }) {
    if (received != null) {
      return received(this);
    }
    return orElse();
  }
}

abstract class _StatisticsReceivedState implements StatisticsState {
  const factory _StatisticsReceivedState(
          {required final List<TariffUsageStats> stats}) =
      _$StatisticsReceivedStateImpl;

  List<TariffUsageStats> get stats;
  @JsonKey(ignore: true)
  _$$StatisticsReceivedStateImplCopyWith<_$StatisticsReceivedStateImpl>
      get copyWith => throw _privateConstructorUsedError;
}
