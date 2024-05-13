import 'package:grpc/grpc.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/utils/grpc/chat.pbgrpc.dart';
import 'package:mobileapp/utils/grpc/google/empty.pb.dart';
import 'package:mobileapp/domain/entities/message/message.dart' as domain;

class ChatGrpcClient {
  late final MessagingServiceClient _messagingServiceClient;
  late final ManagementServiceClient _managementClient;
  late final SessionDataProvider _sessionDataProvider;
  String? _subscribedTopicName;

  ChatGrpcClient(
      this._messagingServiceClient,
      this._managementClient,
      this._sessionDataProvider
      );

  Future<bool> sendMessage(String text) async {
    if (_subscribedTopicName == null){
      return false;
    }

    final callOptions = CallOptions(
      metadata: await _getMetadata(),
    );
    final request = FromClientMessage(text: text, topicName: _subscribedTopicName);
    final response = await _messagingServiceClient.sendMessage(request, options: callOptions);

    return response.accepted;
  }

  Future<List<domain.Message>> receiveHistory() async {
    final callOptions = CallOptions(
      metadata: await _getMetadata(),
    );
    final messages = await _managementClient
        .getMyChatHistory(MyChatHistorySelectorMessage(), options: callOptions);

    return messages.history
        .map((e) => _toDomainMessage(e))
        .toList();
  }

  Stream<domain.Message> receiveMessage() async* {
    final callOptions = CallOptions(
      metadata: await _getMetadata(),
    );

    await for (var msg in _messagingServiceClient
        .getChatStream(Empty(), options: callOptions)) {
      if (msg.hasTopicInfo()) {
        _subscribedTopicName = msg.topicInfo.topicName;
      } else if (msg.hasMessage()) {
        final chatMessage = msg.message;
        yield _toDomainMessage(chatMessage);
      }
    }
  }

  Future<Map<String, String>> _getMetadata() async {
    final jwt = await _sessionDataProvider.getJwtToken();

    return {
      "Authorization": "Bearer $jwt"
    };
  }

  domain.Message _toDomainMessage(Message source) {
    final chatMessageAuthor = source.author;
    return domain.Message(
      text: source.text,
      author: chatMessageAuthor.name,
      isFromManager: chatMessageAuthor.isManager,
      id: source.id,
    );
  }
}