import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/register/cubit.dart';
import 'package:mobileapp/bloc/pages/register/state.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
import 'package:mobileapp/ui/components/text_field.dart';

class RegisterPageWidget extends StatelessWidget {
  const RegisterPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<ProfilePageCubit>(
      create: (ctx) => ProfilePageCubit(ctx),
      child: const _View(),
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  @override
  Widget build(BuildContext context) {
    final cubit = context.read<ProfilePageCubit>();
    final error = context.select((ProfilePageCubit cubit) => cubit.state.error);
    final requestSent = context.select((ProfilePageCubit state) => cubit.state.requestSent);

    return Scaffold(
      resizeToAvoidBottomInset : false,
      appBar: DriveAppBar(title: "Регистрация"),
      body: SafeArea(
        top: true,
        minimum: const EdgeInsets.symmetric(horizontal: 20),
          child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            DriveTextFromField(
              enabled: !requestSent,
              label: 'Почта',
              onChange: cubit.changeEmail,
            ),
            const SizedBox(height: 20),
            DriveTextFromField(
              enabled: !requestSent,
              label: 'Имя',
              onChange: cubit.changeName,
            ),
            const SizedBox(height: 20),
            DriveTextFromField(
              enabled: !requestSent,
              label: 'Фамилия',
              onChange: cubit.changeSecondName,
            ),
            const SizedBox(height: 20),
            DriveTextFromField(
              label: 'Дата рождения',
              onChange: cubit.changeDate,
              enabled: !requestSent,
            ),
            const SizedBox(height: 20),
            DriveTextFromField(
              enabled: !requestSent,
              label: 'Пароль',
              onChange: cubit.changePassword,
              obscureText: true,
            ),
            const SizedBox(height: 20),
            DriveTextFromField(
              enabled: !requestSent,
              label: 'Повтор пароля',
              onChange: cubit.changePasswordRepetition,
              obscureText: true,
            ),
            const SizedBox(height: 20),
            if (error.isNotEmpty)
              Text(
                error,
                style: DriveTextStyles.errorLabel,
              ),
            const SizedBox(height: 20),
            BottomButton(
                title: 'Зарегестрироваться',
                onPressed: requestSent ? null : cubit.onRegisterPressed,
            ),
          ],
        ),
      ),
    );
  }
}



class FormInputSubpage extends StatelessWidget {
  final String label;
  final bool obscureText;
  final ValueChanged<String> onChanged;

  const FormInputSubpage({
    super.key,
    required this.label,
    this.obscureText = false,
    required this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      decoration: InputDecoration(
        labelText: label,
        border: const OutlineInputBorder(),
      ),
      obscureText: obscureText,
      onChanged: onChanged,
    );
  }
}

