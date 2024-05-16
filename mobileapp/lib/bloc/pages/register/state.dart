import 'package:freezed_annotation/freezed_annotation.dart';

part 'state.freezed.dart';

@freezed
class ProfilePageCubitState with _$ProfilePageCubitState {

  const factory ProfilePageCubitState({
    required String password,
    required String confirmPassword,
    required String email,
    required DateTime? birthDate,
    required String name,
    required String secondName,
    required bool requestSent,
    required String error,
  }) = _ProfilePageCubitState;
}