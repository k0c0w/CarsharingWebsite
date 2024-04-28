import 'package:freezed_annotation/freezed_annotation.dart';

part 'events.freezed.dart';

@freezed
sealed class HomePageBlocEvent with _$HomePageBlocEvent {
  const factory HomePageBlocEvent.load() = HomePageBlocLoadEvent;
  const factory HomePageBlocEvent.selectCar(int carId)
  = HomePageBlocSelectCarEvent;
  const factory HomePageBlocEvent.tryBook(DateTime startRent, DateTime endDate)
    = HomePageBlocTryRentEvent;
  const factory HomePageBlocEvent.selectAnotherTariff(int tariffIndex) = HomePageBlocSelectAnotherTariffEvent;
}
