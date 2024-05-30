abstract class AuthEvent {}

class AuthCheckStatusEvent implements AuthEvent {}


class AuthLogoutEvent implements AuthEvent {
  AuthLogoutEvent();
}

class AuthLoginEvent implements AuthEvent {
  final String login;
  final String password;

  AuthLoginEvent({required this.login, required this.password});
}
