import 'package:graphql_flutter/graphql_flutter.dart';
import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/domain/providers/user_info_provider.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/base.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/map_models/drawer_user.dart';

class GetProfileUseCase extends UseCase<Profile> {
  static const String _profileQuery = """
  query {
    profile {
      userInfo {
        email
        secondName
        name
        birthDate
        balance
        isConfirmed
      }
    }
  }
  """;

  static const String _personalInfoQuery = """
  query {
    personalInfo {
      passport
      driverLicense
    }
  }
  """;

  final _drawerUserInfoProvider = getIt<DrawerUserInfoDataProvider>();

  Future<Result<Profile>> call({bool allowCached = true}) async {
    final fetchPolicy = allowCached ? FetchPolicy.cacheAndNetwork : FetchPolicy.noCache;
    final profileQueryOptions = QueryOptions(document: gql(_profileQuery), fetchPolicy: fetchPolicy);
    final personalInfoQueryOptions = QueryOptions(document: gql(_personalInfoQuery), fetchPolicy: fetchPolicy);

    final profileQueryResult = await withTimeOut(graphQlClient.query(profileQueryOptions));
    if (profileQueryResult.hasException || isUnexecuted(profileQueryResult)) {
      return tryDispatchError(profileQueryResult);
    }

    final personalInfoQueryResult = await withTimeOut(graphQlClient.query(personalInfoQueryOptions));
    if (personalInfoQueryResult.hasException || isUnexecuted(personalInfoQueryResult)) {
      return tryDispatchError(personalInfoQueryResult);
    }

    final profileMap = profileQueryResult.data!["profile"]["userInfo"];
    final personalInfoMap = personalInfoQueryResult.data!["personalInfo"];

    final profile = Profile(
        name: profileMap["name"],
        secondName: profileMap["secondName"],
        email: profileMap["email"],
        balance: profileMap["balance"].toDouble(),
        birthDate: DateTime.parse(profileMap["birthDate"]),
        isConfirmed: profileMap["isConfirmed"],
        driverLicense: personalInfoMap["driverLicense"]?.toString(),
        passport: personalInfoMap["passport"]
    );

    if (!allowCached) {
      try{
        final newUserInfo = DrawerUserInfo(
            name: profile.name,
            secondName: profile.secondName,
            isConfirmed: profile.isConfirmed);
        await _drawerUserInfoProvider.saveUserInfo(newUserInfo);
      }
      catch (e){

      }
    }

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
