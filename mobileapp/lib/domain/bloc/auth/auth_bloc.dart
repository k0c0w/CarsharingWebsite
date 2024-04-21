
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/domain/api_clients/api_client_exceptions.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/domain/bloc/auth/auth_bloc_states.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';

import '../../api_clients/auth_client.dart';

class AuthBloc extends Bloc<AuthEvent, AuthState> {
  final _sessionDataProvider = SessionDataProvider();
  final _authApiClient = AuthApiClient();

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

    try {
      final token = await _authApiClient.auth(event.login, event.password);

      await _sessionDataProvider.setJwtToken(token);

      emit(AuthAuthorizedState());
    }
    on ApiClientException catch(e) {
      String errorMessage;

      switch(e.type) {
        case ApiClientExceptionType.network:
          errorMessage = "Сервер не доступен. Проверьте подключение к интернету.";
          break;
        case ApiClientExceptionType.auth:
          errorMessage = "Не верный логин или пароль.";
          break;
        case ApiClientExceptionType.sessionExpired:
        case ApiClientExceptionType.other:
          errorMessage = "Произошла ошибка во время выполнения запроса. Повторите попытку.";
          break;
      }

      emit(AuthFailureState(error: errorMessage));
    }
    catch (e) {
      emit(AuthFailureState(error: "Произошла неизвестная ошибка. Повторите попытку."));
    }
  }

  Future<void> _onLogout(event, emit) async {
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
    //todo : check if token is outdated
    final isAuth = token != null;

    final newState = true ? AuthAuthorizedState() : AuthUnauthorizedState();
    emit(newState);
  }
}
