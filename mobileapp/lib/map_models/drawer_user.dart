import 'package:freezed_annotation/freezed_annotation.dart';

part 'drawer_user.freezed.dart';
part 'drawer_user.g.dart';

@freezed
class DrawerUserInfo with _$DrawerUserInfo {

  const factory DrawerUserInfo({
    required String name,
    required String secondName,
    required bool isConfirmed}) = _DrawerUserInfo;

  factory DrawerUserInfo.fromJson(Map<String, dynamic> json) => _$DrawerUserInfoFromJson(json);
}