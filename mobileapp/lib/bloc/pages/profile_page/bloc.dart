import 'dart:async';
import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter/cupertino.dart';
import 'package:intl/intl.dart';
import 'package:mobileapp/bloc/auth/auth_bloc.dart';
import 'package:mobileapp/bloc/auth/auth_bloc_events.dart';
import 'package:mobileapp/bloc/pages/profile_page/events.dart';
import 'package:mobileapp/map_models/profile_map_model.dart';
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

  Future<void> _loadProfileInfo(ProfilePageLoadEvent event, emitter) async {
    emitter(const ProfilePageBlocState.loading());

    final profileResult = await GetProfileUseCase()(allowCached: event.allowCache);

    if (profileResult is Error) {
      emitter(const ProfilePageBlocState.loadError("Что-то пошло не так..."));
    } else if (profileResult is Ok<Profile>) {
      final profile = profileResult.value;

      final mapModel = ProfilePageBlocStateLoadedMapModel(
        accountStatus: ProfilePageBlocStateLoadedMapModelProperty(text: profile.isConfirmed ? "" : "Аккаунт не подтвержден!"),
        email: ProfilePageBlocStateLoadedMapModelProperty(text:profile.email),
        age: ProfilePageBlocStateLoadedMapModelProperty(text: DateTimeFormat.toStringFormatter.format(profile.birthDate)),
        balance: ProfilePageBlocStateLoadedMapModelProperty(text:profile.balance.toString()),
        name: ProfilePageBlocStateLoadedMapModelProperty(text:profile.name),
        secondName: ProfilePageBlocStateLoadedMapModelProperty(text:profile.secondName),
        driverLicense: ProfilePageBlocStateLoadedMapModelProperty(text:profile.driverLicense ?? ""),
        passport: ProfilePageBlocStateLoadedMapModelProperty(text:profile.passport ?? ""),
        confirmed: profile.isConfirmed,
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
    if(state is ProfilePageBlocStateLoaded) {
      final currState = state as ProfilePageBlocStateLoaded;
      final model = currState.model;
      final nameUpdateResult = await UpdateProfileNameUseCase()(event.name, model.toProfile());
      model.name = switch(nameUpdateResult) {
        Error(:final error) =>
            model.name.copyWith(error: error),
        Ok(:final value) =>
            model.name.copyWith(text: value.name, error: ""),
      };

      emitter(currState.copyWith(model: model));
    }
  }

  Future<void> _onSecondNameChanged(
      ProfilePageSecondNameChangedEvent event,
      emitter) async {
      if (state is ProfilePageBlocStateLoaded) {
        final currState = (state as ProfilePageBlocStateLoaded);
        final model = currState.model;
        final secondNameUpdateResult = await UpdateProfileSecondNameUseCase()(event.secondName, model.toProfile());
        model.secondName = switch(secondNameUpdateResult) {
          Error(:final error) =>
              model.secondName.copyWith(error: error),
          Ok(:final value) =>
              model.secondName.copyWith(
                  text: value.secondName, error: ""),
        };

        emitter(currState.copyWith(model: model));
      }
  }


  Future<void> _onAgeChanged(ProfilePageBirthdateChangedEvent event, emitter)
  async {
    if(state is ProfilePageBlocStateLoaded) {
      final currState = state as ProfilePageBlocStateLoaded;
      final model = currState.model;
      final birthDateUpdateResult = await UpdateProfileBirthDateUseCase()(event.birthDate, model.toProfile());
      model.age = switch(birthDateUpdateResult) {
        Error(:final error) =>
            model.age.copyWith(error: error),
        Ok(:final value) =>
            model.age.copyWith(
                text: DateTimeFormat.toStringFormatter.format(value.birthDate), error: ""),
      };

      emitter(currState.copyWith(model: model));
    }
  }

  Future<void> _onEmailChanged(ProfilePageEmailChangedEvent event, emitter)
  async {

    if(state is ProfilePageBlocStateLoaded) {
      final currState = state as ProfilePageBlocStateLoaded;
      final model = currState.model;
      final emailUpdateResult = await UpdateProfileEmailUseCase()(event.email, model.toProfile());

      model.email = switch(emailUpdateResult) {
        Error(:final error) =>
            model.email.copyWith(error: error),
        Ok(:final value) =>
            model.email.copyWith(
                text: value.email, error: ""),
      };

      emitter(currState.copyWith(model: model));
    }
  }

  Future<void> _onPassportChanged(ProfilePagePassportChangedEvent event, emitter)
  async {
    if(state is ProfilePageBlocStateLoaded) {
      final currState = state as ProfilePageBlocStateLoaded;
      final model = currState.model;
      final passportUpdateResult = await UpdateProfileEmailUseCase()(event.passport, model.toProfile());

      model.passport = switch(passportUpdateResult) {
        Error(:final error) =>
            model.passport.copyWith(error: error),
        Ok(:final value) =>
            model.passport.copyWith(
                text: value.passport ?? "", error: ""),
      };
      emitter(currState.copyWith(model: model));
    }
  }

  Future<void> _onDriverLicenseChanged(ProfilePageDriverLicenseChangedEvent event, emitter)
  async {
    if(state is ProfilePageBlocStateLoaded) {
      final currState = state as ProfilePageBlocStateLoaded;
      final model = currState.model;
      final licenseUpdateResult = await UpdateProfileEmailUseCase()(event.license, model.toProfile());

      model.driverLicense = switch(licenseUpdateResult) {
        Error(:final error) =>
            model.driverLicense.copyWith(error: error),
        Ok(:final value) =>
            model.driverLicense.copyWith(
                text: value.driverLicense?.toString() ?? "", error: ""),
      };
      emitter(currState.copyWith(model: model));
    }
  }
}
