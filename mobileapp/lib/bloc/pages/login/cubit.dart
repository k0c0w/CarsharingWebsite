import 'dart:async';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/bloc/pages/login/state.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

class LoginPageCubit extends Cubit<LoginPageCubitState> {
  final AuthBloc authBloc;
  final BuildContext buildContext;
  late final StreamSubscription<AuthState> authBlocSubscription;

  LoginPageCubit(this.authBloc, this.buildContext)
      : super(const LoginPageCubitState(
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
