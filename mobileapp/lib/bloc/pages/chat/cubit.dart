import 'package:bloc/bloc.dart';
import 'package:mobileapp/bloc/pages/chat/state.dart';
import 'package:mobileapp/utils/grpc/chat_grpc_client.dart';

class ChatCubit extends Cubit<ChatState>{

  final ChatGrpcClient _client;

  ChatCubit(this._client, super.initialState);

  Future<void> sendMessage(String message) async {
    if (state.messageSent) {
      return;
    }

    message = message.trim();
    if (message.isEmpty) {
      emit(state.copyWith(errorMessage: "Сообщение пустое."));
      return;
    }

    emit(state.copyWith(messageSent: true));

    final accepted = await _client.sendMessage(message);

    if (!accepted) {
      emit(state.copyWith(errorMessage: "Сообщение не отправлено"));
    }

    emit(state.copyWith(messageSent: false));
  }
}