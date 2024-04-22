import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:mobileapp/ui/components/bottom_button.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:provider/provider.dart';


class _ViewCubitState {
  final String login;
  final String password;
  final bool requestSent;
  final String error;

  const _ViewCubitState({
    required this.password,
    required this.requestSent,
    required this.login,
    required this.error,
  });

  _ViewCubitState copyWith({
    String? login,
    String? password,
    bool? requestSent,
    String? error,
  }) {
    return _ViewCubitState(
      login: login ?? this.login,
      password: password ?? this.password,
      requestSent: requestSent ?? this.requestSent,
      error: error ?? this.error,
    );
  }
}

class _ViewCubit extends Cubit<_ViewCubitState> {
  final AuthBloc authBloc;
  final BuildContext buildContext;
  late final StreamSubscription<AuthState> authBlocSubscription;

  _ViewCubit(this.authBloc, this.buildContext)
      : super(const _ViewCubitState(
      password: "",
      requestSent: false,
      login: "",
      error: ""
  )) {
    _onState(authBloc.state);
    authBlocSubscription = authBloc.stream.listen(_onState);
  }

  void _navigateToHome() => Navigator.restorablePushNamedAndRemoveUntil(buildContext, DriveRoutes.home, (route) => false);

  void _onState(AuthState state) {
    final cubitState = this.state;
    if (state is AuthAuthorizedState) {
      _navigateToHome();
    } else if (state is AuthFailureState) {
      emit(cubitState.copyWith(password: "", requestSent: false, error: state.error));
    } else if (state is AuthInProgressState) {
      emit(cubitState.copyWith(requestSent: true));
    }
  }

  void changeLogin(String login) {
    emit(state.copyWith(login: login.trim()));
  }

  void changePassword(String password) {
    emit(state.copyWith(password: password.trim()));
  }

  void onLoginPressed() {
    final state = this.state;

    if (state.login.isEmpty) {
      emit(state.copyWith(error: "Введите логин."));
      return;
    }

    if (state.password.isEmpty) {
      emit(state.copyWith(error: "Введите пароль."));
      return;
    }

    authBloc.add(AuthLoginEvent(login: state.login, password: state.password));
  }

  @override
  Future<void> close() {
    authBlocSubscription.cancel();
    return super.close();
  }
}

class LoginPageWidget extends StatelessWidget {
  const LoginPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<_ViewCubit>(
      create: (context) => _ViewCubit(getIt<AuthBloc>(), context),
      child: const _View(),
    );
  }
}

class _View extends StatelessWidget {
  static const _emailAllowedSymbols = r"[a-zA-Z0-9.!#$%&'*+\-/=?^_`{|}~\\(),:;<>@\[\]\w]";

  const _View();

  @override
  Widget build(BuildContext context) {
    final cubit = context.read<_ViewCubit>();
    final loginRequestSent = context.select((_ViewCubit cubit) =>
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

class CreateAccountButton extends StatelessWidget {
  final VoidCallback onPressed;

  const CreateAccountButton({Key? key, required this.onPressed}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return TextButton(
      onPressed: onPressed,
      child: const Text('Нет аккаунта? Создайте!'),
    );
  }
}

class FormInputSubpage extends StatelessWidget {
  final String label;
  final bool obscureText;
  final ValueChanged<String> onChanged;

  const FormInputSubpage({
    Key? key,
    required this.label,
    this.obscureText = false,
    required this.onChanged,
  }) : super(key: key);

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
