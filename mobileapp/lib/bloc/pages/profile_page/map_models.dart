import 'package:freezed_annotation/freezed_annotation.dart';

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

  ProfilePageBlocStateLoadedMapModel({
    required this.name,
    required this.secondName,
    required this.age,
    required this.email,
    required this.passport,
    required this.driverLicense,
    required this.balance,
    required this.accountStatus,
  });

  @override
  bool operator ==(Object other) => other is ProfilePageBlocStateLoadedMapModel
          && runtimeType == other.runtimeType;

  @override
  int get hashCode =>0;
}
