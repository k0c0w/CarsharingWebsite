
import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:intl/intl.dart';
import 'package:mobileapp/bloc/pages/chat/cubit.dart';
import 'package:mobileapp/bloc/pages/chat/state.dart';
import 'package:mobileapp/domain/entities/message/message.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/components/error_page.dart';
import 'package:mobileapp/utils/extensions.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/utils/date_formatter.dart';
import 'package:mobileapp/utils/grpc/chat_grpc_client.dart';

class SupportChatPageWidget extends StatelessWidget {
  const SupportChatPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
          appBar: DriveAppBar(title: "ПОДДЕРЖКА",),
          body: BlocProvider<ChatCubit>(
            create: (_) => ChatCubit(getIt<ChatGrpcClient>(), const ChatState.loading()),
            lazy: false,
            child: _View(),
          ),
        );
  }
}

class _View extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    final state = context.select((ChatCubit value) => value.state);

    return state.map(
        loading: (_) => const CenterCircularProgressIndicator(),
        loadError: (errState) =>
            LoadPageErrorMessageAtCenter(
              customErrorMessage: errState.error,
            ),
        loaded: (_) => _ChatView()
    );
  }
}

class _ChatView extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: <Widget>[
        _MessageContainer(context.read<ChatCubit>().messageStream),
        _MessageForm()
      ],
    );
  }
}

class _MessageContainer extends StatefulWidget {
  final Stream<Message> _messageStream;

  const _MessageContainer(this._messageStream);

  @override
  State<StatefulWidget> createState() => _MessageContainerState(_messageStream);
}

class _MessageContainerState extends State<_MessageContainer> {

  late final StreamSubscription<Message> _streamSubscription;
  _MessageContainerState(Stream<Message> messageStream) {
    _streamSubscription = messageStream.listen((_) => setState(() {}));
  }

  @override
  void dispose() {
    _streamSubscription.cancel().ignore();
    super.dispose();
  }

  Widget createMessageContainer(DateFormat to24hFormatter, Message message, {ValueKey<String>? bubbleKey}) {
    return Container(
      key: bubbleKey,
      padding: const EdgeInsets.only(left: 14,right: 14,top: 10,bottom: 10),
      child: Align(
        alignment: (message.isFromManager?Alignment.topLeft:Alignment.topRight),
        child: _CardBubble(
            text: message.text,
            time: to24hFormatter.format(message.time),
            backgroundColor: message.isFromManager?Colors.grey.shade200:DriveColors.lightBlueColor),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final messages = context.select((ChatCubit cub) => (cub.state as ChatLoadedState).messages)
        .reversed
        .toList();
    final to24hFormatter = DateTimeFormat.to24HoursFormatter;

    return ListView.builder(
            shrinkWrap: true,
            reverse: true,
            padding: const EdgeInsets.only(top: 10, bottom: 100),
            physics: const ScrollPhysics(),
            itemBuilder: (context, index){
              if (index == messages.length) {
                return null;
              }

              final message = messages[index];
              if (index + 1 == messages.length || !messages[index + 1].time.isSameDate(message.time)) {
                return Column(
                  key: ValueKey(message.id),
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    Center(
                      child: Text(DateTimeFormat.toRussianDateFormatter.format(message.time)),
                    ),
                    createMessageContainer(to24hFormatter, message),
                  ],
                );
              }

              return createMessageContainer(to24hFormatter, message, bubbleKey: ValueKey(message.id));
            },
          );


  }
}

class _CardBubble extends StatelessWidget {
  final String text;
  final String time;
  final Color backgroundColor;

  const _CardBubble({
    required this.text,
    required this.time,
    required this.backgroundColor,
  });

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    return ConstrainedBox(
        constraints: BoxConstraints(maxWidth: size.width * 2 / 3),
        child: Card(
          color: backgroundColor,
          child: Stack(
            children: <Widget>[
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: RichText(
                  text: TextSpan(
                    children: <TextSpan>[
                      //real message
                      TextSpan(
                          text: "$text    ",
                          style: const TextStyle(
                            color: Colors.black,
                          )
                      ),

                      //fake additionalInfo as placeholder
                      TextSpan(
                          text: time,
                          style: const TextStyle(
                              color: Colors.transparent
                          )
                      ),
                    ],
                  ),
                ),
              ),

              //real additionalInfo
              Positioned(
                right: 8.0,
                bottom: 4.0,
                child: Text(
                  time,
                  style: const TextStyle(
                    fontSize: 9,
                  ),
                ),
              )
            ],
          ),
        )
    )
    ;
  }
}

class _MessageForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _MessageFormState();
}

class _MessageFormState extends State<_MessageForm> {

  final _textEditingController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    final error = context.select((ChatCubit value) => (value.state as ChatLoadedState).errorMessage);
    final messageSent = context.select((ChatCubit value) => (value.state as ChatLoadedState).messageSent);
    final cubit = context.read<ChatCubit>();
    return Align(
      alignment: Alignment.bottomLeft,
      child: Container(
        padding: const EdgeInsets.only(left: 10,bottom: 10,top: 10),
        height: 60,
        width: double.infinity,
        color: Colors.white,
        child: Row(
          children: <Widget>[
            const SizedBox(width: 15,),
            Expanded(
              child: TextField(
                controller: _textEditingController,
                decoration: InputDecoration(
                    contentPadding: error == null ? null : const EdgeInsets.only(top: 3),
                    hintText: "Введите сообщение",
                    hintStyle: const TextStyle(color: Colors.black54),
                    border: InputBorder.none,
                    errorMaxLines: 1,
                    errorText: error
                ),
              ),
            ),
            const SizedBox(width: 15,),
            FloatingActionButton(
              onPressed: messageSent ? null :
                  () async {
                    final accepted = await cubit.sendMessage(_textEditingController.text);
                    if (accepted) {
                      _textEditingController.clear();
                    }
              },
              backgroundColor: DriveColors.deepBlueColor,
              elevation: 0,
              child: const Icon(Icons.send,color: Colors.white,size: 15,),
            ),
          ],
        ),
      ),
    );
  }
}