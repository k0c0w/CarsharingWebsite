
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class SignInUserUseCase extends UseCase<bool> {

  Future<Result<String>> call(String login, String password) async {
    return Ok("jwt_token");
  }
}

class ValidateSessionUseCase extends UseCase<bool> {

  Future<Result<bool>> call() async {
    return Ok(true);
  }
}

class SignUpUseCase extends UseCase<bool> {

  Future<Result<bool>> call() async {
    return Ok(false);
  }
}