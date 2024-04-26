import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/drawer/events.dart';
import 'package:mobileapp/bloc/pages/drawer/states.dart';
import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/profile_cases.dart';

class DrawerBloc extends Bloc<DrawerBlocEvent, DrawerBlocState> {

  DrawerBloc() : super(const DrawerBlocState.loading()) {
    on<DrawerBlocEvent>(_dispatchEvent, transformer: sequential());
  }

  Future<void> _dispatchEvent(DrawerBlocEvent event, emitter) async {
    final future = switch(event) {
      DrawerBlocLoadEvent() => _onLoad(emitter),
      DrawerBlocErrorEvent() => _onError(emitter),
      DrawerBlocLoadedEvent(:final name, :final secondName, :final profileConfirmed)
      => _onLoaded(name, secondName, profileConfirmed, emitter),
    };
    await future;
  }

  Future<void> _onLoad(emitter) async {
    emitter(const DrawerBlocState.loading());

    final getProfileResult = await GetProfileUseCase()();

    final event = switch (getProfileResult) {
      Error<Profile>() => const DrawerBlocEvent.error(),
      Ok<Profile>(:final value) => DrawerBlocEvent.loaded(value.name, value.secondName, value.isConfirmed),
    };

    add(event);
  }

  Future<void> _onError(emitter) async {
    await Future<void>.delayed(const Duration(seconds: 3));
    add(const DrawerBlocEvent.load());
  }

  Future<void> _onLoaded(
      String name,
      String secondName,
      bool profileConfirmed,
      emitter) async {
    final mainTitle = "$name $secondName";
    final confirmationTitle = profileConfirmed ? "Аккаунт подтвержден" : "Аккаунт не подтвержден";

    emitter(DrawerBlocState.loaded(mainTitle, confirmationTitle));
  }
}