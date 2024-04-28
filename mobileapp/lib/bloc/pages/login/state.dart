class LoginPageCubitState {
  final String login;
  final String password;
  final bool requestSent;
  final String error;

  const LoginPageCubitState({
    required this.password,
    required this.requestSent,
    required this.login,
    required this.error,
  });

  LoginPageCubitState copyWith({
    String? login,
    String? password,
    bool? requestSent,
    String? error,
  }) {
    return LoginPageCubitState(
      login: login ?? this.login,
      password: password ?? this.password,
      requestSent: requestSent ?? this.requestSent,
      error: error ?? this.error,
    );
  }
}