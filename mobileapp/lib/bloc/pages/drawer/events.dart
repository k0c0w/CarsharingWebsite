import 'package:freezed_annotation/freezed_annotation.dart';

part 'events.freezed.dart';

@freezed
sealed class DrawerBlocEvent with _$DrawerBlocEvent {
  const factory DrawerBlocEvent.load() = DrawerBlocLoadEvent;
  const factory DrawerBlocEvent.error() = DrawerBlocErrorEvent;
  const factory DrawerBlocEvent.loaded(String name, String secondName, bool profileConfirmed) = DrawerBlocLoadedEvent;
}
