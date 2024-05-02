import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/bloc/pages/home_page/map_search_area.dart';

part 'events.freezed.dart';

@freezed
sealed class HomePageBlocEvent with _$HomePageBlocEvent {
  const factory HomePageBlocEvent.initialLoad(MapSearchArea searchParams) = HomePageBlocInitialLoadEvent;
  const factory HomePageBlocEvent.selectCar(int carId)
  = HomePageBlocSelectCarEvent;
  const factory HomePageBlocEvent.tryBook(DateTime startRent, DateTime endDate)
    = HomePageBlocTryRentEvent;
  const factory HomePageBlocEvent.selectAnotherTariff(int tariffIndex) = HomePageBlocSelectAnotherTariffEvent;
  const factory HomePageBlocEvent.changeAnchor(MapSearchArea searchParams) = HomePageBlocChangeAnchorEvent;
}
