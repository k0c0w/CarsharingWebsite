import 'package:freezed_annotation/freezed_annotation.dart';

part 'events.freezed.dart';

@freezed
sealed class ProfilePageBlocEvent with _$ProfilePageBlocEvent {
  const factory ProfilePageBlocEvent.load({@Default(true) bool allowCache}) = ProfilePageLoadEvent;
  const factory ProfilePageBlocEvent.exitPressed() = ProfilePageExitEvent;
  const factory ProfilePageBlocEvent.nameChanged(String name) = ProfilePageNameChangedEvent;
  const factory ProfilePageBlocEvent.secondNameChanged(String secondName) = ProfilePageSecondNameChangedEvent;
  const factory ProfilePageBlocEvent.ageChanged(DateTime birthDate) = ProfilePageBirthdateChangedEvent;
  const factory ProfilePageBlocEvent.emailChanged(String email) = ProfilePageEmailChangedEvent;
  const factory ProfilePageBlocEvent.passportChanged(String passport) = ProfilePagePassportChangedEvent;
  const factory ProfilePageBlocEvent.driverLicenseChanged(String license) = ProfilePageDriverLicenseChangedEvent;
}
