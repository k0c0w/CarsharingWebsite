import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/domain/providers/session_data_provider.dart';
import 'package:mobileapp/domain/results.dart';

class GetProfileUseCase {
  final SessionDataProvider sessionDataProvider = SessionDataProvider();

  Future<Result<Profile>> call() async {
    final profile = Profile(
      name: "Василий",
      secondName: "Якупов",
      email: "example@example.com",
      birthDate: DateTime(1990, 2, 4),
      balance: 0,
      isConfirmed: false,
    );

    return Ok(profile);
  }
}

class UpdateProfileNameUseCase {

  Future<Result<String>> call(String name) async {
    return Ok<String>("Марсель");
  }
}

class UpdateProfileSecondNameUseCase {

  Future<Result<String>> call(String secondName) async {
    return Ok<String>("Хамитов");
  }
}

class UpdateProfileEmailUseCase {

  Future<Result<String>> call(String email) async {
    return Ok<String>("example2@mail.ru");
  }
}

class UpdateProfileBirthDateUseCase {

  Future<Result<DateTime>> call(DateTime birthDate) async {
    return Ok<DateTime>(DateTime(1993, 2, 28));
  }
}

class UpdatePassportUseCase {

  Future<Result<String>> call(String passport) async {
    return Ok<String>("9218232029");
  }
}

class UpdateLicenseUseCase {

  Future<Result<String>> call(String license) async {
    return Ok<String>("2345352232");
  }
}
