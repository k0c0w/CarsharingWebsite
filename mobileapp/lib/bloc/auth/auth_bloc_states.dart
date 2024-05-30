import "dart:core";

abstract class AuthState {
  @override
  bool operator ==(Object other) => identical(this, other)
      || runtimeType == other.runtimeType;

  @override
  int get hashCode => 0;
}

class AuthAuthorizedState extends AuthState {
}

class AuthFailureState extends AuthState {
  final String error;

  AuthFailureState({required this.error});

  @override
  bool operator ==(Object other) => identical(this, other)
      || other is AuthFailureState
          && runtimeType == other.runtimeType
          && error == other.error;

  @override
  int get hashCode => error.hashCode;
}

class AuthInProgressState extends AuthState {
}

class AuthCheckStatusInProgressState extends AuthState {
}

class AuthUnauthorizedState extends AuthState {
}