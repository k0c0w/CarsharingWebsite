import 'package:freezed_annotation/freezed_annotation.dart';

part 'events.freezed.dart';

@freezed
sealed class ActiveSubscriptionsEvent with _$ActiveSubscriptionsEvent {

  const factory ActiveSubscriptionsEvent.load() = ActiveSubscriptionsLoadEvent;
  const factory ActiveSubscriptionsEvent.startEngine(int carId) = ActiveSubscriptionsStartEngineEvent;
  const factory ActiveSubscriptionsEvent.openCar(int carId) = ActiveSubscriptionsOpenCarEvent;
  const factory ActiveSubscriptionsEvent.closeCar(int carId) = ActiveSubscriptionsCloseCarEvent;
  const factory ActiveSubscriptionsEvent.turnLightsCar(int carId) = ActiveSubscriptionsTurnLightsEvent;
}
