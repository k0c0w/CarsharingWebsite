import 'package:freezed_annotation/freezed_annotation.dart';

part 'message.freezed.dart';

@freezed
class Message with _$Message {
  const factory Message(
  {
    required String id,
    required String text,
    required String author,
    required DateTime time,
    @Default(false)
    bool isFromManager,
  }) = _Message;
}