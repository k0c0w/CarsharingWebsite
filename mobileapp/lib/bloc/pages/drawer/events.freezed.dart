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
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(DrawerBlocLoadEvent value) load,
    required TResult Function(DrawerBlocErrorEvent value) error,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
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
  }) {
    return load();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
  }) {
    return load?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
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
  }) {
    return load(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
  }) {
    return load?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
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
  }) {
    return error();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? load,
    TResult? Function()? error,
  }) {
    return error?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? load,
    TResult Function()? error,
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
  }) {
    return error(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(DrawerBlocLoadEvent value)? load,
    TResult? Function(DrawerBlocErrorEvent value)? error,
  }) {
    return error?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(DrawerBlocLoadEvent value)? load,
    TResult Function(DrawerBlocErrorEvent value)? error,
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
