import 'package:freezed_annotation/freezed_annotation.dart';

part 'states.freezed.dart';

@freezed
sealed class DrawerBlocState with _$DrawerBlocState {
  const factory DrawerBlocState.loading() = DrawerBlocLoadingState;
  const factory DrawerBlocState.error() = DrawerBlocLoadErrorState;
  const factory DrawerBlocState.loaded(String nameAndSecondName, String accountConfirmedTitle) = DrawerBlocLoadedState;
}
