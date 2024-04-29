import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/auth_cases.dart';
import 'package:mobileapp/main.dart';

class AuthBloc extends Bloc<AuthEvent, AuthState> {
  final _sessionDataProvider = getIt<SessionDataProvider>();

  AuthBloc(super.initialState) {
    on<AuthEvent>(_dispatchEvent, transformer: sequential());
  }

  Future<void> _dispatchEvent(AuthEvent event, emit) {
    if (event is AuthLoginEvent) {
      return _onLogin(event, emit);
    } else if (event is AuthLogoutEvent) {
      return _onLogout(event, emit);
    } else if (event is AuthCheckStatusEvent) {
      return _onStatusCheck(event, emit);
    }

    return Future.value();
  }

  Future<void> _onLogin(AuthLoginEvent event, emit) async {
    if (event.login.isEmpty || event.password.isEmpty) {
      emit(AuthFailureState(error: "Сначала заполните оба поля."));
      return;
    }

    final getTokenResult = await SignInUserUseCase()(event.login, event.password);
    if(getTokenResult is Error<String>) {
      emit(AuthFailureState(error:getTokenResult.error));
    } else if (getTokenResult is Ok<String>) {
      await _sessionDataProvider.setJwtToken(getTokenResult.value);
      emit(AuthAuthorizedState());
    }
  }

  Future<void> _onLogout(AuthLogoutEvent event, emit) async {
    try{
      await _sessionDataProvider.deleteJwtToken();
      emit(AuthUnauthorizedState());
    }
    catch (e) {
      emit(AuthFailureState(error: "Произшла ошибка при выходе. Сессия не была удалена!"));
    }
  }

  Future<void> _onStatusCheck(event, emit) async {
    final token = await _sessionDataProvider.getJwtToken();

    if (token == null) {
      emit(AuthUnauthorizedState());
      return;
    }

    final validateSessionResult = await ValidateSessionUseCase()();
    if (validateSessionResult is Ok<bool> && validateSessionResult.value) {
      emit(AuthAuthorizedState());
      return;
    }

    emit(AuthUnauthorizedState());
  }
}
