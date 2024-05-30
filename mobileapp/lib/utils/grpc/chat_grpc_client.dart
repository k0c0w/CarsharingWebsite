import 'package:mobileapp/utils/grpc/chat.pbgrpc.dart';
import 'package:mobileapp/utils/grpc/google/empty.pb.dart';
import 'package:mobileapp/domain/entities/message/message.dart' as domain;

class ChatGrpcClient {
  late final MessagingServiceClient _messagingServiceClient;
  late final ManagementServiceClient _managementClient;
  String? _subscribedTopicName;

  ChatGrpcClient(
      this._messagingServiceClient,
      this._managementClient,
      );

  Future<bool> sendMessage(String text) async {
    if (_subscribedTopicName == null){
      return false;
    }

    final request = FromClientMessage(text: text, topicName: _subscribedTopicName);
    final response = await _messagingServiceClient.sendMessage(request);

    return response.accepted;
  }

  Future<List<domain.Message>> receiveHistory() async {
    final messages = await _managementClient
        .getMyChatHistory(MyChatHistorySelectorMessage());

    return messages.history
        .map((e) => _toDomainMessage(e))
        .toList();
  }

  Stream<domain.Message> receiveMessage() async* {
    await for (var msg in _messagingServiceClient
        .getChatStream(Empty())) {
      if (msg.hasTopicInfo()) {
        _subscribedTopicName = msg.topicInfo.topicName;
      } else if (msg.hasMessage()) {
        final chatMessage = msg.message;
        yield _toDomainMessage(chatMessage);
      }
    }
  }

  domain.Message _toDomainMessage(Message source) {
    final chatMessageAuthor = source.author;
    return domain.Message(
      id: source.id,
      text: source.text,
      author: chatMessageAuthor.name,
      isFromManager: chatMessageAuthor.isManager,
      time: source.time.toDateTime(toLocal: true),
    );
  }
}