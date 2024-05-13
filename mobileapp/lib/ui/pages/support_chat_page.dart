
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/chat/cubit.dart';
import 'package:mobileapp/bloc/pages/chat/state.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/utils/grpc/chat_grpc_client.dart';

class SupportChatPageWidget extends StatelessWidget {
  const SupportChatPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: DriveAppBar(title: "ПОДДЕРЖКА",),
      body: BlocProvider<ChatCubit>(
        create: (_) => ChatCubit(getIt<ChatGrpcClient>(), const ChatState()),
        lazy: false,
        child: _View(),
      ),
    );
  }
}

class _View extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        SingleChildScrollView(
          child: _MessageContainer(),

        ),
        Align(
          alignment: Alignment.bottomCenter,
          child: _MessageForm(),
        )
      ],
    );
  }
}

class _MessageContainer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final messages = context.select((ChatCubit cub) => cub.state.messages);

    return ListView.builder(
        itemCount: messages.length,
        shrinkWrap: false,
        itemBuilder: (ctx, i) {
          return Center(
            child: Text(messages[i].text),
          );
        }
    );
  }
}

class _MessageForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _MessageFormState();
}

class _MessageFormState extends State<_MessageForm> {

  String message = "";

  @override
  Widget build(BuildContext context) {
    final error = context.select((ChatCubit value) => value.state.errorMessage);
    return Column(
      children: [
        Text(error,
          style: DriveTextStyles.errorLabel,
        ),
        Row(
          children: [
            TextFormField(
              onChanged: (txt) => setState(() {message = txt;}),
            ),
            IconButton(
                onPressed: () => context.read<ChatCubit>().sendMessage(message),
                icon: const Icon(Icons.arrow_circle_right)
            )
          ],
        ),
      ],
    );
  }
}