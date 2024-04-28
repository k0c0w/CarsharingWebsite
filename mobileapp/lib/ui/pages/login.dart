import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/login/cubit.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';


class LoginPageWidget extends StatelessWidget {
  const LoginPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<LoginPageCubit>(
      create: (context) => LoginPageCubit(getIt<AuthBloc>(), context),
      child: const _View(),
    );
  }
}

class _View extends StatelessWidget {
  static const _emailAllowedSymbols = r"[a-zA-Z0-9.!#$%&'*+\-/=?^_`{|}~\\(),:;<>@\[\]\w]";

  const _View();

  @override
  Widget build(BuildContext context) {
    final cubit = context.read<LoginPageCubit>();
    final loginRequestSent = context.select((LoginPageCubit cubit) =>
    cubit.state.requestSent);

    return Scaffold(
      appBar: DriveAppBar(title: "ЛОГИН"),
      body: SafeArea(
        minimum: const EdgeInsets.symmetric(horizontal: 20),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            const Spacer(),
            TextFormField(
              inputFormatters: [
                FilteringTextInputFormatter.allow(RegExp(_emailAllowedSymbols)),
              ],
              decoration: InputDecoration(
                labelText: "Почта",
                enabledBorder: const UnderlineInputBorder(
                    borderSide: BorderSide(
                      color: DriveColors.lightBlueColor,
                    )
                ),
                disabledBorder: const UnderlineInputBorder(),
                enabled: !loginRequestSent,
              ),
              onChanged: cubit.changeLogin,
            ),
            const SizedBox(height: 20),
            TextFormField(
              obscureText: true,
              decoration: InputDecoration(
                labelText: "Пароль",
                enabledBorder: const UnderlineInputBorder(
                    borderSide: BorderSide(
                      color: DriveColors.lightBlueColor,
                    )
                ),
                disabledBorder: const UnderlineInputBorder(),
                enabled: !loginRequestSent,
              ),
              onChanged: cubit.changePassword,
            ),
            const SizedBox(height: 20),
            BottomButton(
              title: "ВОЙТИ",
              onPressed: loginRequestSent ? null : cubit.onLoginPressed,
            ),
          ],
        ),
      ),
    );
  }
}
