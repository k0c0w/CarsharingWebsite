import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';

part 'state.freezed.dart';

@freezed
sealed class HomePageBlocState with _$HomePageBlocState {
  const factory HomePageBlocState.loaded({
    required List<Car> cars,
    required List<Tariff> tariffs,
    @Default(0)
    int selectedTariffIndex,
    int? selectedCarId,
    @Default(false)
    bool requestSent,
  }) = HomePageBlocLoadedState;

  const factory HomePageBlocState.renting() = HomePageBlocRentingState;
  const factory HomePageBlocState.successfulRent() = HomePageBlocSuccessfulRentState;
  const factory HomePageBlocState.unsuccessfulRent() = HomePageBlocUnsuccessfulRentState;
  const factory HomePageBlocState.loadError({String? error}) = HomePageBlocLoadErrorState;
  const factory HomePageBlocState.loading() = HomePageBlocLoadingState;
}
