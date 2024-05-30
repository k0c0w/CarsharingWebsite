import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/bloc/pages/home_page/map_search_area.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';

part 'state.freezed.dart';

@freezed
sealed class HomePageBlocState with _$HomePageBlocState {
  const factory HomePageBlocState.loaded({
    required List<Car> cars,
    required List<Tariff> tariffs,
    required MapSearchArea mapAnchor,
    required int? selectedTariffIndex,
    int? selectedCarId,
  }) = HomePageBlocLoadedState;

  const factory HomePageBlocState.renting() = HomePageBlocRentingState;
  const factory HomePageBlocState.successfulRent() = HomePageBlocSuccessfulRentState;
  const factory HomePageBlocState.unsuccessfulRent({String? error}) = HomePageBlocUnsuccessfulRentState;
  const factory HomePageBlocState.loadError({String? error}) = HomePageBlocLoadErrorState;
  const factory HomePageBlocState.loading() = HomePageBlocLoadingState;
}
