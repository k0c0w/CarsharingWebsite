// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'events.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

/// @nodoc
mixin _$DrawerBlocEvent {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() load,
    required TResult Function() error,
    required TResult Function(
            String name, String secondName, bool profileConfirmed)
        loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
    TResult? Function(String name, String secondName, bool profileConfirmed)?
        loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
    TResult Function(String name, String secondName, bool profileConfirmed)?
        loaded,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadEvent value) load,
    required TResult Function(DrawerBlocErrorEvent value) error,
    required TResult Function(DrawerBlocLoadedEvent value) loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
    TResult? Function(DrawerBlocLoadedEvent value)? loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
    TResult Function(DrawerBlocLoadedEvent value)? loaded,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $DrawerBlocEventCopyWith<$Res> {
  factory $DrawerBlocEventCopyWith(
          DrawerBlocEvent value, $Res Function(DrawerBlocEvent) then) =
      _$DrawerBlocEventCopyWithImpl<$Res, DrawerBlocEvent>;
}

/// @nodoc
class _$DrawerBlocEventCopyWithImpl<$Res, $Val extends DrawerBlocEvent>
    implements $DrawerBlocEventCopyWith<$Res> {
  _$DrawerBlocEventCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$DrawerBlocLoadEventImplCopyWith<$Res> {
  factory _$$DrawerBlocLoadEventImplCopyWith(_$DrawerBlocLoadEventImpl value,
          $Res Function(_$DrawerBlocLoadEventImpl) then) =
      __$$DrawerBlocLoadEventImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$DrawerBlocLoadEventImplCopyWithImpl<$Res>
    extends _$DrawerBlocEventCopyWithImpl<$Res, _$DrawerBlocLoadEventImpl>
    implements _$$DrawerBlocLoadEventImplCopyWith<$Res> {
  __$$DrawerBlocLoadEventImplCopyWithImpl(_$DrawerBlocLoadEventImpl _value,
      $Res Function(_$DrawerBlocLoadEventImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$DrawerBlocLoadEventImpl implements DrawerBlocLoadEvent {
  const _$DrawerBlocLoadEventImpl();

  @override
  String toString() {
    return 'DrawerBlocEvent.load()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerBlocLoadEventImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() load,
    required TResult Function() error,
    required TResult Function(
            String name, String secondName, bool profileConfirmed)
        loaded,
  }) {
    return load();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
    TResult? Function(String name, String secondName, bool profileConfirmed)?
        loaded,
  }) {
    return load?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
    TResult Function(String name, String secondName, bool profileConfirmed)?
        loaded,
    required TResult orElse(),
  }) {
    if (load != null) {
      return load();
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadEvent value) load,
    required TResult Function(DrawerBlocErrorEvent value) error,
    required TResult Function(DrawerBlocLoadedEvent value) loaded,
  }) {
    return load(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
    TResult? Function(DrawerBlocLoadedEvent value)? loaded,
  }) {
    return load?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
    TResult Function(DrawerBlocLoadedEvent value)? loaded,
    required TResult orElse(),
  }) {
    if (load != null) {
      return load(this);
    }
    return orElse();
  }
}

abstract class DrawerBlocLoadEvent implements DrawerBlocEvent {
  const factory DrawerBlocLoadEvent() = _$DrawerBlocLoadEventImpl;
}

/// @nodoc
abstract class _$$DrawerBlocErrorEventImplCopyWith<$Res> {
  factory _$$DrawerBlocErrorEventImplCopyWith(_$DrawerBlocErrorEventImpl value,
          $Res Function(_$DrawerBlocErrorEventImpl) then) =
      __$$DrawerBlocErrorEventImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$DrawerBlocErrorEventImplCopyWithImpl<$Res>
    extends _$DrawerBlocEventCopyWithImpl<$Res, _$DrawerBlocErrorEventImpl>
    implements _$$DrawerBlocErrorEventImplCopyWith<$Res> {
  __$$DrawerBlocErrorEventImplCopyWithImpl(_$DrawerBlocErrorEventImpl _value,
      $Res Function(_$DrawerBlocErrorEventImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$DrawerBlocErrorEventImpl implements DrawerBlocErrorEvent {
  const _$DrawerBlocErrorEventImpl();

  @override
  String toString() {
    return 'DrawerBlocEvent.error()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerBlocErrorEventImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() load,
    required TResult Function() error,
    required TResult Function(
            String name, String secondName, bool profileConfirmed)
        loaded,
  }) {
    return error();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
    TResult? Function(String name, String secondName, bool profileConfirmed)?
        loaded,
  }) {
    return error?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
    TResult Function(String name, String secondName, bool profileConfirmed)?
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
    required TResult Function(DrawerBlocLoadEvent value) load,
    required TResult Function(DrawerBlocErrorEvent value) error,
    required TResult Function(DrawerBlocLoadedEvent value) loaded,
  }) {
    return error(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
    TResult? Function(DrawerBlocLoadedEvent value)? loaded,
  }) {
    return error?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
    TResult Function(DrawerBlocLoadedEvent value)? loaded,
    required TResult orElse(),
  }) {
    if (error != null) {
      return error(this);
    }
    return orElse();
  }
}

abstract class DrawerBlocErrorEvent implements DrawerBlocEvent {
  const factory DrawerBlocErrorEvent() = _$DrawerBlocErrorEventImpl;
}

/// @nodoc
abstract class _$$DrawerBlocLoadedEventImplCopyWith<$Res> {
  factory _$$DrawerBlocLoadedEventImplCopyWith(
          _$DrawerBlocLoadedEventImpl value,
          $Res Function(_$DrawerBlocLoadedEventImpl) then) =
      __$$DrawerBlocLoadedEventImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String name, String secondName, bool profileConfirmed});
}

/// @nodoc
class __$$DrawerBlocLoadedEventImplCopyWithImpl<$Res>
    extends _$DrawerBlocEventCopyWithImpl<$Res, _$DrawerBlocLoadedEventImpl>
    implements _$$DrawerBlocLoadedEventImplCopyWith<$Res> {
  __$$DrawerBlocLoadedEventImplCopyWithImpl(_$DrawerBlocLoadedEventImpl _value,
      $Res Function(_$DrawerBlocLoadedEventImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? name = null,
    Object? secondName = null,
    Object? profileConfirmed = null,
  }) {
    return _then(_$DrawerBlocLoadedEventImpl(
      null == name
          ? _value.name
          : name // ignore: cast_nullable_to_non_nullable
              as String,
      null == secondName
          ? _value.secondName
          : secondName // ignore: cast_nullable_to_non_nullable
              as String,
      null == profileConfirmed
          ? _value.profileConfirmed
          : profileConfirmed // ignore: cast_nullable_to_non_nullable
              as bool,
    ));
  }
}

/// @nodoc

class _$DrawerBlocLoadedEventImpl implements DrawerBlocLoadedEvent {
  const _$DrawerBlocLoadedEventImpl(
      this.name, this.secondName, this.profileConfirmed);

  @override
  final String name;
  @override
  final String secondName;
  @override
  final bool profileConfirmed;

  @override
  String toString() {
    return 'DrawerBlocEvent.loaded(name: $name, secondName: $secondName, profileConfirmed: $profileConfirmed)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$DrawerBlocLoadedEventImpl &&
            (identical(other.name, name) || other.name == name) &&
            (identical(other.secondName, secondName) ||
                other.secondName == secondName) &&
            (identical(other.profileConfirmed, profileConfirmed) ||
                other.profileConfirmed == profileConfirmed));
  }

  @override
  int get hashCode =>
      Object.hash(runtimeType, name, secondName, profileConfirmed);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$DrawerBlocLoadedEventImplCopyWith<_$DrawerBlocLoadedEventImpl>
      get copyWith => __$$DrawerBlocLoadedEventImplCopyWithImpl<
          _$DrawerBlocLoadedEventImpl>(this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() load,
    required TResult Function() error,
    required TResult Function(
            String name, String secondName, bool profileConfirmed)
        loaded,
  }) {
    return loaded(name, secondName, profileConfirmed);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
    TResult? Function(String name, String secondName, bool profileConfirmed)?
        loaded,
  }) {
    return loaded?.call(name, secondName, profileConfirmed);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
    TResult Function(String name, String secondName, bool profileConfirmed)?
        loaded,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(name, secondName, profileConfirmed);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadEvent value) load,
    required TResult Function(DrawerBlocErrorEvent value) error,
    required TResult Function(DrawerBlocLoadedEvent value) loaded,
  }) {
    return loaded(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
    TResult? Function(DrawerBlocLoadedEvent value)? loaded,
  }) {
    return loaded?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
    TResult Function(DrawerBlocLoadedEvent value)? loaded,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(this);
    }
    return orElse();
  }
}

abstract class DrawerBlocLoadedEvent implements DrawerBlocEvent {
  const factory DrawerBlocLoadedEvent(
      final String name,
      final String secondName,
      final bool profileConfirmed) = _$DrawerBlocLoadedEventImpl;

  String get name;
  String get secondName;
  bool get profileConfirmed;
  @JsonKey(ignore: true)
  _$$DrawerBlocLoadedEventImplCopyWith<_$DrawerBlocLoadedEventImpl>
      get copyWith => throw _privateConstructorUsedError;
}
