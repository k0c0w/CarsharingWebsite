import 'dart:async';
import 'package:bloc/bloc.dart';
import 'package:mobileapp/bloc/pages/chat/state.dart';
import 'package:mobileapp/domain/entities/message/message.dart';
import 'package:mobileapp/utils/grpc/chat_grpc_client.dart';

class ChatCubit extends Cubit<ChatState>{

  final ChatGrpcClient _client;
  late final StreamSubscription<Message>? _streamSubscription;
  final _messageStreamController = StreamController<Message>(sync: true);

  ChatCubit(this._client, super.initialState) {
    _init();
  }

  Stream<Message> get messageStream
    => _messageStreamController.stream.asBroadcastStream();


  void _onMessageReceived(Message message) {
    _messageStreamController.add(message);
    state.mapOrNull(
      loaded: (st) => st.messages.add(message)
    );
  }

  Future<void> _init() async {
    try{
      final history = await _client.receiveHistory();
      final stream = _client.receiveMessage();

      emit(ChatState.loaded(messages: history));
      _streamSubscription = stream.listen(_onMessageReceived);
    } catch(e) {
      emit(const ChatState.loadError(error: "Ошибка при загрузке чата."));
    }
  }

  Future<bool> sendMessage(String message) async {
    if (this.state is! ChatLoadedState){
      return false;
    }

    final state = this.state as ChatLoadedState;
    if (state.messageSent) {
      return false;
    }

    message = message.trim();
    if (message.isEmpty) {
      emit(state.copyWith(errorMessage: "Сообщение пустое."));
      return false;
    }

    emit(state.copyWith(messageSent: true));

    bool accepted = false;
    try{
      accepted = await _client.sendMessage(message);
      if (!accepted) {
        emit(state.copyWith(errorMessage: "Сообщение не отправлено."));
      }
    } catch(e) {
      emit(state.copyWith(errorMessage: "Ошибка при отправке сообщения."));
    }

    emit(state.copyWith(messageSent: false, errorMessage: null));

    return accepted;
  }

  @override
  Future<void> close() {
    _streamSubscription?.cancel().ignore();
    _messageStreamController.close().ignore();
    return super.close();
  }
}