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
import 'package:mobileapp/ui/pages/pages_list.dart';

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

    await Future.delayed(const Duration(seconds: 5));

    final profile = Profile(
        name: "Василий",
        secondName: "Якупов",
        email: "example@example.com",
        birthDate: DateTime(1990, 2, 4),
        balance: 0,
        isConfirmed: false,
    );

    final mapModel = ProfilePageBlocStateLoadedMapModel(
      accountStatus: ProfilePageBlocStateLoadedMapModel.property(text: profile.isConfirmed ? "" : "Аккаунт не подтвержден!"),
      email: ProfilePageBlocStateLoadedMapModel.property(text:profile.email),
      age: ProfilePageBlocStateLoadedMapModel.property(text: DateFormat('dd.MM.yyyy').format(profile.birthDate)),
      balance: ProfilePageBlocStateLoadedMapModel.property(text:profile.balance.toString()),
      name: ProfilePageBlocStateLoadedMapModel.property(text:profile.name),
      secondName: ProfilePageBlocStateLoadedMapModel.property(text:profile.secondName),
      driverLicense: ProfilePageBlocStateLoadedMapModel.property(text:profile.driverLicense ?? ""),
      passport: ProfilePageBlocStateLoadedMapModel.property(text:profile.passport ?? ""),
    );

    emitter(ProfilePageBlocState.loaded(mapModel));
    emitter(const ProfilePageBlocState.loadError("Ошибка при загрузке профиля"));
  }

  void _onExitPressed(event, emitter) {
    authBloc.add(AuthLogoutEvent());
    Navigator
        .of(buildContext)
        .pushNamedAndRemoveUntil(DriveRoutes.appLoader, (_) => false);
  }

  Future<void> _onNameChanged(ProfilePageNameChangedEvent event, emitter) async {

  }

  Future<void> _onSecondNameChanged(
      ProfilePageSecondNameChangedEvent event,
      emitter) async {

  }

  Future<void> _onAgeChanged(ProfilePageBirthdateChangedEvent event, emitter)
  async {
    final formatter = DateFormat("dd.MM.yyyy");
    final newDateTitle = formatter.format(event.birthDate);

  }

  Future<void> _onEmailChanged(ProfilePageEmailChangedEvent event, emitter)
  async {

  }

  Future<void> _onPassportChanged(ProfilePagePassportChangedEvent event, emitter)
  async {

  }

  Future<void> _onDriverLicenseChanged(ProfilePageDriverLicenseChangedEvent event, emitter)
  async {

  }
}
