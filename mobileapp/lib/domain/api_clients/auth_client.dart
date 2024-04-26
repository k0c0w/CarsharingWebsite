class AuthApiClient {

  Future<String> auth(String login, String password) async {
    if (login == "login" && password == "password")
      return "jwtToken";

    throw Error();
  }
}