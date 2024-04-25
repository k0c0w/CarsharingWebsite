import 'package:freezed_annotation/freezed_annotation.dart';

part 'map_models.freezed.dart';

@freezed
class _ProfilePageBlocStateLoadedMapModelProperty
    with _$ProfilePageBlocStateLoadedMapModelProperty {
  const factory _ProfilePageBlocStateLoadedMapModelProperty({
    required String text,
    required String error,
  }) = __ProfilePageBlocStateLoadedMapModelProperty;
}

@Freezed(
  copyWith: false,
  equal: true,
)
class ProfilePageBlocStateLoadedMapModel with _$ProfilePageBlocStateLoadedMapModel {
  const factory ProfilePageBlocStateLoadedMapModel({
    required _ProfilePageBlocStateLoadedMapModelProperty name,
    required _ProfilePageBlocStateLoadedMapModelProperty secondName,
    required _ProfilePageBlocStateLoadedMapModelProperty age,
    required _ProfilePageBlocStateLoadedMapModelProperty email,
    required _ProfilePageBlocStateLoadedMapModelProperty passport,
    required _ProfilePageBlocStateLoadedMapModelProperty driverLicense,
    required _ProfilePageBlocStateLoadedMapModelProperty balance,
    required _ProfilePageBlocStateLoadedMapModelProperty accountStatus,
  }) = _ProfilePageBlocStateLoadedMapModel;

  static _ProfilePageBlocStateLoadedMapModelProperty property({String error = "", String text = ""})
  => _ProfilePageBlocStateLoadedMapModelProperty(text: text, error: error);
}
