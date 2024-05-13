import 'package:freezed_annotation/freezed_annotation.dart';

part 'profile.freezed.dart';
part 'profile.g.dart';

@freezed
class Profile with _$Profile {
  const factory Profile({
    required String name,
    required String secondName,
    required String email,
    required DateTime birthDate,
    required double balance,
    @Default(false)
    bool isConfirmed,
    String? passport,
    String? driverLicense,
  }) = _$ProfilePageLoadEvent;

  factory Profile.fromJson(Map<String, Object?> json)
    => _$ProfileFromJson(json);
}