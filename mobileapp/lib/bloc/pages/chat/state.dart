import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/message/message.dart';

part 'state.freezed.dart';

@Freezed(makeCollectionsUnmodifiable: false)
class ChatState with _$ChatState{

  const factory ChatState.loading() = _ChatLoadingState;
  const factory ChatState.loadError({String? error}) = _ChatLoadErrorState;
  const factory ChatState.loaded({
    required List<Message> messages,
    @Default(false)
    bool messageSent,
    String? errorMessage
  }) = ChatLoadedState;
}