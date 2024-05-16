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

class UpdateProfileNameUseCase extends UpdateProfileUseCase {

  Future<Result<Profile>> call(String name, Profile profile) async {
    final vars = _defaultVariables(profile);
    vars["name"] = name;
    final mutationOptions = MutationOptions(
        document: gql(UpdateProfileUseCase.updateMutation),
        variables: vars
    );

    final updateResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if(updateResult.hasException || isUnexecuted(updateResult) || didNotUpdate(updateResult)) {
      return tryDispatchError(updateResult);
    }

    return Ok<Profile>(profile.copyWith(name: name));
  }
}

class UpdateProfileSecondNameUseCase extends UpdateProfileUseCase {

  Future<Result<Profile>> call(String secondName, Profile profile) async {
    final vars = _defaultVariables(profile);
    vars["secondName"] = secondName;
    final mutationOptions = MutationOptions(
        document: gql(UpdateProfileUseCase.updateMutation),
        variables: vars
    );

    final updateResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if(updateResult.hasException || isUnexecuted(updateResult) || didNotUpdate(updateResult)) {
      return tryDispatchError(updateResult);
    }

    return Ok<Profile>(profile.copyWith(secondName: secondName));
  }
}

class UpdateProfileEmailUseCase extends UpdateProfileUseCase {

  Future<Result<Profile>> call(String email, Profile profile) async {
    final vars = _defaultVariables(profile);
    vars["email"] = email;
    final mutationOptions = MutationOptions(
        document: gql(UpdateProfileUseCase.updateMutation),
        variables: vars
    );

    final updateResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if(updateResult.hasException || isUnexecuted(updateResult) || didNotUpdate(updateResult)) {
      return tryDispatchError(updateResult);
    }

    return Ok<Profile>(profile.copyWith(email: email));
  }
}

class UpdateProfileBirthDateUseCase extends UpdateProfileUseCase {

  Future<Result<Profile>> call(DateTime birthDate, Profile profile) async {
    final vars = _defaultVariables(profile);
    vars["birthDate"] = birthDate;
    final mutationOptions = MutationOptions(
        document: gql(UpdateProfileUseCase.updateMutation),
        variables: vars
    );

    final updateResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if(updateResult.hasException || isUnexecuted(updateResult) || didNotUpdate(updateResult)) {
      return tryDispatchError(updateResult);
    }

    return Ok<Profile>(profile.copyWith(birthDate: birthDate));
  }
}

class UpdatePassportUseCase extends UpdateProfileUseCase {

  Future<Result<Profile>> call(String passport, Profile profile) async {
    final vars = _defaultVariables(profile);
    vars["passport"] = passport;
    final mutationOptions = MutationOptions(
        document: gql(UpdateProfileUseCase.updateMutation),
        variables: vars
    );

    final updateResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if(updateResult.hasException || isUnexecuted(updateResult) || didNotUpdate(updateResult)) {
      return tryDispatchError(updateResult);
    }

    return Ok<Profile>(profile.copyWith(passport: passport));
  }
}

class UpdateLicenseUseCase  extends UpdateProfileUseCase {

  Future<Result<Profile>> call(String driverLicense, Profile profile) async {
    final vars = _defaultVariables(profile);
    vars["driverLicense"] = driverLicense;
    final mutationOptions = MutationOptions(
        document: gql(UpdateProfileUseCase.updateMutation),
        variables: vars
    );

    final updateResult = await withTimeOut(graphQlClient.mutate(mutationOptions));

    if(updateResult.hasException || isUnexecuted(updateResult) || didNotUpdate(updateResult)) {
      return tryDispatchError(updateResult);
    }

    return Ok<Profile>(profile.copyWith(driverLicense: driverLicense));
  }
}

abstract class UpdateProfileUseCase extends UseCase<Profile> {
  static const String updateMutation = """
  mutation(\$email: String, 
  \$name: String, \$secondName: String, \$birthDate: DateTime!, \$passport: String,
  \$driverLicense: Int) {
    editProfile(userVm: {
      email: \$email,
      firstName: \$name,
      lastName: \$secondName,
      birthDay: \$birthDate,
      passport: \$passport,
      driverLicense: \$driverLicense
    })
  }
  """;

  bool didNotUpdate(QueryResult<Object?> updateResult) => !updateResult.data!["editProfile"];

  Map<String, dynamic> _defaultVariables(Profile profile)
  => {
    "email": profile.email,
    "name": profile.name,
    "secondName": profile.secondName,
    "birthDate": profile.birthDate.toIso8601String(),
    "passport": profile.passport,
    "driverLicense": num.tryParse(profile.driverLicense ?? ""),
  };
}
