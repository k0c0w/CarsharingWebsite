class AuthApiClient {

  Future<String> auth(String login, String password) async {
    if (login=="login" && password =="password") {
      print("got jwt");
      return "jwtToken";
    }

    throw Error();
  }
}