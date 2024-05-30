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
mixin _$ChatState {
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(String? error) loadError,
    required TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)
        loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(String? error)? loadError,
    TResult? Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(String? error)? loadError,
    TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(_ChatLoadingState value) loading,
    required TResult Function(_ChatLoadErrorState value) loadError,
    required TResult Function(ChatLoadedState value) loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_ChatLoadingState value)? loading,
    TResult? Function(_ChatLoadErrorState value)? loadError,
    TResult? Function(ChatLoadedState value)? loaded,
  }) =>
      throw _privateConstructorUsedError;
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_ChatLoadingState value)? loading,
    TResult Function(_ChatLoadErrorState value)? loadError,
    TResult Function(ChatLoadedState value)? loaded,
    required TResult orElse(),
  }) =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $ChatStateCopyWith<$Res> {
  factory $ChatStateCopyWith(ChatState value, $Res Function(ChatState) then) =
      _$ChatStateCopyWithImpl<$Res, ChatState>;
}

/// @nodoc
class _$ChatStateCopyWithImpl<$Res, $Val extends ChatState>
    implements $ChatStateCopyWith<$Res> {
  _$ChatStateCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;
}

/// @nodoc
abstract class _$$ChatLoadingStateImplCopyWith<$Res> {
  factory _$$ChatLoadingStateImplCopyWith(_$ChatLoadingStateImpl value,
          $Res Function(_$ChatLoadingStateImpl) then) =
      __$$ChatLoadingStateImplCopyWithImpl<$Res>;
}

/// @nodoc
class __$$ChatLoadingStateImplCopyWithImpl<$Res>
    extends _$ChatStateCopyWithImpl<$Res, _$ChatLoadingStateImpl>
    implements _$$ChatLoadingStateImplCopyWith<$Res> {
  __$$ChatLoadingStateImplCopyWithImpl(_$ChatLoadingStateImpl _value,
      $Res Function(_$ChatLoadingStateImpl) _then)
      : super(_value, _then);
}

/// @nodoc

class _$ChatLoadingStateImpl implements _ChatLoadingState {
  const _$ChatLoadingStateImpl();

  @override
  String toString() {
    return 'ChatState.loading()';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType && other is _$ChatLoadingStateImpl);
  }

  @override
  int get hashCode => runtimeType.hashCode;

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(String? error) loadError,
    required TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)
        loaded,
  }) {
    return loading();
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(String? error)? loadError,
    TResult? Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
  }) {
    return loading?.call();
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(String? error)? loadError,
    TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
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
    required TResult Function(_ChatLoadingState value) loading,
    required TResult Function(_ChatLoadErrorState value) loadError,
    required TResult Function(ChatLoadedState value) loaded,
  }) {
    return loading(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_ChatLoadingState value)? loading,
    TResult? Function(_ChatLoadErrorState value)? loadError,
    TResult? Function(ChatLoadedState value)? loaded,
  }) {
    return loading?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_ChatLoadingState value)? loading,
    TResult Function(_ChatLoadErrorState value)? loadError,
    TResult Function(ChatLoadedState value)? loaded,
    required TResult orElse(),
  }) {
    if (loading != null) {
      return loading(this);
    }
    return orElse();
  }
}

abstract class _ChatLoadingState implements ChatState {
  const factory _ChatLoadingState() = _$ChatLoadingStateImpl;
}

/// @nodoc
abstract class _$$ChatLoadErrorStateImplCopyWith<$Res> {
  factory _$$ChatLoadErrorStateImplCopyWith(_$ChatLoadErrorStateImpl value,
          $Res Function(_$ChatLoadErrorStateImpl) then) =
      __$$ChatLoadErrorStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({String? error});
}

/// @nodoc
class __$$ChatLoadErrorStateImplCopyWithImpl<$Res>
    extends _$ChatStateCopyWithImpl<$Res, _$ChatLoadErrorStateImpl>
    implements _$$ChatLoadErrorStateImplCopyWith<$Res> {
  __$$ChatLoadErrorStateImplCopyWithImpl(_$ChatLoadErrorStateImpl _value,
      $Res Function(_$ChatLoadErrorStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? error = freezed,
  }) {
    return _then(_$ChatLoadErrorStateImpl(
      error: freezed == error
          ? _value.error
          : error // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$ChatLoadErrorStateImpl implements _ChatLoadErrorState {
  const _$ChatLoadErrorStateImpl({this.error});

  @override
  final String? error;

  @override
  String toString() {
    return 'ChatState.loadError(error: $error)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ChatLoadErrorStateImpl &&
            (identical(other.error, error) || other.error == error));
  }

  @override
  int get hashCode => Object.hash(runtimeType, error);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ChatLoadErrorStateImplCopyWith<_$ChatLoadErrorStateImpl> get copyWith =>
      __$$ChatLoadErrorStateImplCopyWithImpl<_$ChatLoadErrorStateImpl>(
          this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(String? error) loadError,
    required TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)
        loaded,
  }) {
    return loadError(error);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(String? error)? loadError,
    TResult? Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
  }) {
    return loadError?.call(error);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(String? error)? loadError,
    TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
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
    required TResult Function(_ChatLoadingState value) loading,
    required TResult Function(_ChatLoadErrorState value) loadError,
    required TResult Function(ChatLoadedState value) loaded,
  }) {
    return loadError(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_ChatLoadingState value)? loading,
    TResult? Function(_ChatLoadErrorState value)? loadError,
    TResult? Function(ChatLoadedState value)? loaded,
  }) {
    return loadError?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_ChatLoadingState value)? loading,
    TResult Function(_ChatLoadErrorState value)? loadError,
    TResult Function(ChatLoadedState value)? loaded,
    required TResult orElse(),
  }) {
    if (loadError != null) {
      return loadError(this);
    }
    return orElse();
  }
}

abstract class _ChatLoadErrorState implements ChatState {
  const factory _ChatLoadErrorState({final String? error}) =
      _$ChatLoadErrorStateImpl;

  String? get error;
  @JsonKey(ignore: true)
  _$$ChatLoadErrorStateImplCopyWith<_$ChatLoadErrorStateImpl> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class _$$ChatLoadedStateImplCopyWith<$Res> {
  factory _$$ChatLoadedStateImplCopyWith(_$ChatLoadedStateImpl value,
          $Res Function(_$ChatLoadedStateImpl) then) =
      __$$ChatLoadedStateImplCopyWithImpl<$Res>;
  @useResult
  $Res call({List<Message> messages, bool messageSent, String? errorMessage});
}

/// @nodoc
class __$$ChatLoadedStateImplCopyWithImpl<$Res>
    extends _$ChatStateCopyWithImpl<$Res, _$ChatLoadedStateImpl>
    implements _$$ChatLoadedStateImplCopyWith<$Res> {
  __$$ChatLoadedStateImplCopyWithImpl(
      _$ChatLoadedStateImpl _value, $Res Function(_$ChatLoadedStateImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? messages = null,
    Object? messageSent = null,
    Object? errorMessage = freezed,
  }) {
    return _then(_$ChatLoadedStateImpl(
      messages: null == messages
          ? _value.messages
          : messages // ignore: cast_nullable_to_non_nullable
              as List<Message>,
      messageSent: null == messageSent
          ? _value.messageSent
          : messageSent // ignore: cast_nullable_to_non_nullable
              as bool,
      errorMessage: freezed == errorMessage
          ? _value.errorMessage
          : errorMessage // ignore: cast_nullable_to_non_nullable
              as String?,
    ));
  }
}

/// @nodoc

class _$ChatLoadedStateImpl implements ChatLoadedState {
  const _$ChatLoadedStateImpl(
      {required this.messages, this.messageSent = false, this.errorMessage});

  @override
  final List<Message> messages;
  @override
  @JsonKey()
  final bool messageSent;
  @override
  final String? errorMessage;

  @override
  String toString() {
    return 'ChatState.loaded(messages: $messages, messageSent: $messageSent, errorMessage: $errorMessage)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$ChatLoadedStateImpl &&
            const DeepCollectionEquality().equals(other.messages, messages) &&
            (identical(other.messageSent, messageSent) ||
                other.messageSent == messageSent) &&
            (identical(other.errorMessage, errorMessage) ||
                other.errorMessage == errorMessage));
  }

  @override
  int get hashCode => Object.hash(runtimeType,
      const DeepCollectionEquality().hash(messages), messageSent, errorMessage);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$ChatLoadedStateImplCopyWith<_$ChatLoadedStateImpl> get copyWith =>
      __$$ChatLoadedStateImplCopyWithImpl<_$ChatLoadedStateImpl>(
          this, _$identity);

  @override
  @optionalTypeArgs
  TResult when<TResult extends Object?>({
    required TResult Function() loading,
    required TResult Function(String? error) loadError,
    required TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)
        loaded,
  }) {
    return loaded(messages, messageSent, errorMessage);
  }

  @override
  @optionalTypeArgs
  TResult? whenOrNull<TResult extends Object?>({
    TResult? Function()? loading,
    TResult? Function(String? error)? loadError,
    TResult? Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
  }) {
    return loaded?.call(messages, messageSent, errorMessage);
  }

  @override
  @optionalTypeArgs
  TResult maybeWhen<TResult extends Object?>({
    TResult Function()? loading,
    TResult Function(String? error)? loadError,
    TResult Function(
            List<Message> messages, bool messageSent, String? errorMessage)?
        loaded,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(messages, messageSent, errorMessage);
    }
    return orElse();
  }

  @override
  @optionalTypeArgs
  TResult map<TResult extends Object?>({
    required TResult Function(_ChatLoadingState value) loading,
    required TResult Function(_ChatLoadErrorState value) loadError,
    required TResult Function(ChatLoadedState value) loaded,
  }) {
    return loaded(this);
  }

  @override
  @optionalTypeArgs
  TResult? mapOrNull<TResult extends Object?>({
    TResult? Function(_ChatLoadingState value)? loading,
    TResult? Function(_ChatLoadErrorState value)? loadError,
    TResult? Function(ChatLoadedState value)? loaded,
  }) {
    return loaded?.call(this);
  }

  @override
  @optionalTypeArgs
  TResult maybeMap<TResult extends Object?>({
    TResult Function(_ChatLoadingState value)? loading,
    TResult Function(_ChatLoadErrorState value)? loadError,
    TResult Function(ChatLoadedState value)? loaded,
    required TResult orElse(),
  }) {
    if (loaded != null) {
      return loaded(this);
    }
    return orElse();
  }
}

abstract class ChatLoadedState implements ChatState {
  const factory ChatLoadedState(
      {required final List<Message> messages,
      final bool messageSent,
      final String? errorMessage}) = _$ChatLoadedStateImpl;

  List<Message> get messages;
  bool get messageSent;
  String? get errorMessage;
  @JsonKey(ignore: true)
  _$$ChatLoadedStateImplCopyWith<_$ChatLoadedStateImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
