import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/drawer/events.dart';
import 'package:mobileapp/bloc/pages/drawer/states.dart';
import 'package:mobileapp/domain/entities/profile/profile.dart';
import 'package:mobileapp/domain/providers/user_info_provider.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/profile_cases.dart';
import 'package:mobileapp/map_models/drawer_user.dart';

class DrawerBloc extends Bloc<DrawerBlocEvent, DrawerBlocState> {
  final DrawerUserInfoDataProvider _drawerUserInfoProvider;

  DrawerBloc(this._drawerUserInfoProvider) : super(const DrawerBlocState.loading()) {
    on<DrawerBlocEvent>(_dispatchEvent, transformer: sequential());
  }

  Future<void> _dispatchEvent(DrawerBlocEvent event, emitter) async {
    final future = switch(event) {
      DrawerBlocLoadEvent() => _onLoad(emitter),
      DrawerBlocErrorEvent() => _onError(emitter),
    };
    await future;
  }

  Future<void> _onLoad(emitter) async {
    emitter(const DrawerBlocState.loading());

    var drawerUserInfo = _drawerUserInfoProvider.getSavedUserInfo();

    if (drawerUserInfo == null) {
      final getProfileResult = await GetProfileUseCase()();

      if (getProfileResult is Ok<Profile>) {
        final profile = getProfileResult.value;
        drawerUserInfo = DrawerUserInfo(name: profile.name,
            secondName: profile.secondName,
            isConfirmed: profile.isConfirmed);
        await _drawerUserInfoProvider.saveUserInfo(drawerUserInfo);
      } else if (getProfileResult is Error<Profile>) {
        //to restart load
        add(const DrawerBlocEvent.error());
        return;
      }
    }


    final mainTitle = "${drawerUserInfo?.name} ${drawerUserInfo?.secondName}";
    final confirmationTitle = drawerUserInfo?.isConfirmed ?? false
        ? "Аккаунт подтвержден" : "Аккаунт не подтвержден";

    emitter(DrawerBlocState.loaded(mainTitle, confirmationTitle));
  }

  Future<void> _onError(emitter) async {
    await Future<void>.delayed(const Duration(seconds: 3));
    add(const DrawerBlocEvent.load());
  }
}