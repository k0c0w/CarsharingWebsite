import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';

class GetProfileUseCase extends UseCase<Profile> {

  Future<Result<Profile>> call() async {
    final profile = Profile(
      name: "Василий",
      secondName: "Якупов",
      email: "example@example.com",
      birthDate: DateTime(1990, 2, 4),
      balance: 150.55,
      isConfirmed: true,
    );

    return Ok(profile);
  }
}

class UpdateProfileNameUseCase extends UseCase<String> {

  Future<Result<String>> call(String name) async {
    return Ok<String>("Марсель");
  }
}

class UpdateProfileSecondNameUseCase extends UseCase<String> {

  Future<Result<String>> call(String secondName) async {
    return Ok<String>("Хамитов");
  }
}

class UpdateProfileEmailUseCase extends UseCase<String> {

  Future<Result<String>> call(String email) async {
    return Ok<String>("example2@mail.ru");
  }
}

class UpdateProfileBirthDateUseCase extends UseCase<String> {

  Future<Result<DateTime>> call(DateTime birthDate) async {
    return Ok<DateTime>(DateTime(1993, 2, 28));
  }
}

class UpdatePassportUseCase extends UseCase<String> {

  Future<Result<String>> call(String passport) async {
    return Ok<String>("9218232029");
  }
}

class UpdateLicenseUseCase  extends UseCase<String> {

  Future<Result<String>> call(String license) async {
    return Ok<String>("2345352232");
  }
}
