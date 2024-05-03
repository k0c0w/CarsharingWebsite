import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/utils/date_formatter.dart';

part 'map_models.freezed.dart';

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
          && runtimeType == other.runtimeType;

  @override
  int get hashCode =>0;

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
