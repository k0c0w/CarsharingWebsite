import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/utils/date_formatter.dart';

part 'profile_map_model.freezed.dart';

@freezed
class ProfilePageBlocStateLoadedMapModelProperty
    with _$ProfilePageBlocStateLoadedMapModelProperty {
  const factory ProfilePageBlocStateLoadedMapModelProperty({
    required String text,
    @Default("")
    String error,
  }) = _ProfilePageBlocStateLoadedMapModelProperty;
}

class ProfilePageBlocStateLoadedMapModel {
  ProfilePageBlocStateLoadedMapModelProperty name;
  ProfilePageBlocStateLoadedMapModelProperty secondName;
  ProfilePageBlocStateLoadedMapModelProperty age;
  ProfilePageBlocStateLoadedMapModelProperty email;
  ProfilePageBlocStateLoadedMapModelProperty passport;
  ProfilePageBlocStateLoadedMapModelProperty driverLicense;
  ProfilePageBlocStateLoadedMapModelProperty balance;
  ProfilePageBlocStateLoadedMapModelProperty accountStatus;
  bool _confirmed = false;


  ProfilePageBlocStateLoadedMapModel({
    required this.name,
    required this.secondName,
    required this.age,
    required this.email,
    required this.passport,
    required this.driverLicense,
    required this.balance,
    required this.accountStatus,
    bool confirmed = false,
  }) {
    _confirmed = confirmed;
  }

  @override
  bool operator ==(Object other) => other is ProfilePageBlocStateLoadedMapModel
          && runtimeType == other.runtimeType
          && name == other.name
          && secondName == other.secondName
          && age == other.age
          && email == other.email
          && driverLicense == other.driverLicense
          && accountStatus == other.accountStatus
          && balance == other.balance
          && passport == other.passport;

  @override
  int get hashCode => name.hashCode ^ secondName.hashCode ^ age.hashCode ^ email.hashCode ^ driverLicense.hashCode ^ accountStatus.hashCode ^ balance.hashCode ^ passport.hashCode;

  Profile toProfile() {
    return Profile(
      name: name.text,
      secondName: secondName.text,
      birthDate: DateTimeFormat.toStringFormatter.parse(age.text),
      balance: double.parse(balance.text),
      email: email.text,
      passport: passport.text,
      driverLicense: driverLicense.text,
      isConfirmed: _confirmed,
    );
  }
}
