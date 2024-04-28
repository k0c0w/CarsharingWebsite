import 'dart:async';
import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter/cupertino.dart';
import 'package:intl/intl.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/bloc/pages/profile_page/events.dart';
import 'package:mobileapp/bloc/pages/profile_page/map_models.dart';
import 'package:mobileapp/bloc/pages/profile_page/states.dart';
import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/profile_cases.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:mobileapp/utils/date_formatter.dart';

class ProfilePageBloc extends Bloc<ProfilePageBlocEvent, ProfilePageBlocState> {
  final BuildContext buildContext;
  final AuthBloc authBloc;

  ProfilePageBloc(super.initialState, { required this.authBloc, required this.buildContext}) {
    on<ProfilePageNameChangedEvent>(_onNameChanged, transformer: sequential());
    on<ProfilePageSecondNameChangedEvent>(_onSecondNameChanged, transformer: sequential());
    on<ProfilePageBirthdateChangedEvent>(_onAgeChanged, transformer: sequential());
    on<ProfilePageEmailChangedEvent>(_onEmailChanged, transformer: sequential());
    on<ProfilePagePassportChangedEvent>(_onPassportChanged, transformer: sequential());
    on<ProfilePageDriverLicenseChangedEvent>(_onDriverLicenseChanged, transformer: sequential());
    on<ProfilePageExitEvent>(_onExitPressed, transformer: droppable());
    on<ProfilePageLoadEvent>(_loadProfileInfo, transformer: droppable());
  }

  Future<void> _loadProfileInfo(event, emitter) async {
    emitter(const ProfilePageBlocState.loading());

    final profileResult = await GetProfileUseCase()();

    if (profileResult is Error) {
      emitter(const ProfilePageBlocState.loadError("Что-то пошло не так..."));
    } else if (profileResult is Ok<Profile>) {
      final profile = profileResult.value;

      final mapModel = ProfilePageBlocStateLoadedMapModel(
        accountStatus: ProfilePageBlocStateLoadedMapModelProperty(text: profile.isConfirmed ? "" : "Аккаунт не подтвержден!"),
        email: ProfilePageBlocStateLoadedMapModelProperty(text:profile.email),
        age: ProfilePageBlocStateLoadedMapModelProperty(text: DateFormat('dd.MM.yyyy').format(profile.birthDate)),
        balance: ProfilePageBlocStateLoadedMapModelProperty(text:profile.balance.toString()),
        name: ProfilePageBlocStateLoadedMapModelProperty(text:profile.name),
        secondName: ProfilePageBlocStateLoadedMapModelProperty(text:profile.secondName),
        driverLicense: ProfilePageBlocStateLoadedMapModelProperty(text:profile.driverLicense ?? ""),
        passport: ProfilePageBlocStateLoadedMapModelProperty(text:profile.passport ?? ""),
      );

      emitter(ProfilePageBlocState.loaded(mapModel));
    }
  }

  void _onExitPressed(event, emitter) {
    authBloc.add(AuthLogoutEvent());
    Navigator
        .of(buildContext)
        .pushNamedAndRemoveUntil(DriveRoutes.appLoader, (_) => false);
  }

  Future<void> _onNameChanged(ProfilePageNameChangedEvent event, emitter) async {
    final nameUpdateResult = await UpdateProfileNameUseCase()(event.name);

    if(state is ProfilePageBlocStateLoaded) {
      final model = (state as ProfilePageBlocStateLoaded).model;
      model.name = switch(nameUpdateResult) {
        Error(:final error) =>
            model.name.copyWith(error: error),
        Ok(:final value) =>
            model.name.copyWith(text: value, error: ""),
      };

      emitter(state);
    }
  }

  Future<void> _onSecondNameChanged(
      ProfilePageSecondNameChangedEvent event,
      emitter) async {
      final secondNameUpdateResult = await UpdateProfileSecondNameUseCase()(event.secondName);
      if (state is ProfilePageBlocStateLoaded) {
        final model = (state as ProfilePageBlocStateLoaded).model;
        model.secondName = switch(secondNameUpdateResult) {
          Error(:final error) =>
              model.secondName.copyWith(error: error),
          Ok(:final value) =>
              model.secondName.copyWith(
                  text: value, error: ""),
        };
        emitter(state);
      }
  }


  Future<void> _onAgeChanged(ProfilePageBirthdateChangedEvent event, emitter)
  async {
    final birthDateUpdateResult = await UpdateProfileBirthDateUseCase()(event.birthDate);

    if(state is ProfilePageBlocStateLoaded) {
      final model = (state as ProfilePageBlocStateLoaded).model;
      model.age = switch(birthDateUpdateResult) {
        Error(:final error) =>
            model.age.copyWith(error: error),
        Ok(:final value) =>
            model.age.copyWith(
                text: DateTimeFormat.toStringFormatter.format(value), error: ""),
      };
      emitter(state);
    }
  }

  Future<void> _onEmailChanged(ProfilePageEmailChangedEvent event, emitter)
  async {
    final emailUpdateResult = await UpdateProfileEmailUseCase()(event.email);

    if(state is ProfilePageBlocStateLoaded) {
      final model = (state as ProfilePageBlocStateLoaded).model;
      model.email = switch(emailUpdateResult) {
        Error(:final error) =>
            model.email.copyWith(error: error),
        Ok(:final value) =>
            model.email.copyWith(
                text: value, error: ""),
      };
      emitter(state);
    }
  }

  Future<void> _onPassportChanged(ProfilePagePassportChangedEvent event, emitter)
  async {
    final passportUpdateResult = await UpdateProfileEmailUseCase()(event.passport);

    if(state is ProfilePageBlocStateLoaded) {
      final model = (state as ProfilePageBlocStateLoaded).model;
      model.passport = switch(passportUpdateResult) {
        Error(:final error) =>
            model.passport.copyWith(error: error),
        Ok(:final value) =>
            model.passport.copyWith(
                text: value, error: ""),
      };
      emitter(state);
    }
  }

  Future<void> _onDriverLicenseChanged(ProfilePageDriverLicenseChangedEvent event, emitter)
  async {
    final licenseUpdateResult = await UpdateProfileEmailUseCase()(event.license);

    if(state is ProfilePageBlocStateLoaded) {
      final model = (state as ProfilePageBlocStateLoaded).model;
      model.driverLicense = switch(licenseUpdateResult) {
        Error(:final error) =>
            model.driverLicense.copyWith(error: error),
        Ok(:final value) =>
            model.driverLicense.copyWith(
                text: value, error: ""),
      };
      emitter(state);
    }
  }
}
