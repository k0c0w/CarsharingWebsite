import 'package:mobileapp/domain/entities/message/message.dart';
import 'package:freezed_annotation/freezed_annotation.dart';

part 'state.freezed.dart';

@freezed
class ChatState with _$ChatState{

  const factory ChatState({
    @Default([])
    List<Message> messages,
    @Default(false)
    bool messageSent,
    @Default("")
    String errorMessage
  }) = _ChatState;
}