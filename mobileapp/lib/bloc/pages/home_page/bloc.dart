import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/map_search_area.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/booking_cases.dart';
import 'package:mobileapp/domain/use_cases/car_cases.dart';
import 'package:mobileapp/domain/use_cases/tariff_cases.dart';

class HomePageBloc extends Bloc<HomePageBlocEvent, HomePageBlocState> {
  HomePageBloc(super.initialState) {
    on<HomePageBlocEvent>(_onEvent, transformer: sequential());
  }

  Future<void> _onEvent(HomePageBlocEvent event, emit) async {

    final future = switch (event) {
      HomePageBlocInitialLoadEvent(:final searchParams) => _initialLoadData(searchParams, emit),
      HomePageBlocTryRentEvent() => _tryRentCar(event, emit),
      HomePageBlocChangeAnchorEvent(:final searchParams) => _changeAnchor(searchParams, emit),
      HomePageBlocSelectAnotherTariffEvent(:final tariffIndex) => _selectAnotherTariff(tariffIndex, emit),
      HomePageBlocSelectCarEvent(:final carId) =>  _selectCar(carId, emit),
    };

    await future;
  }

  Future<void> _changeAnchor(MapSearchArea searchArea, emit) async {
    if (state is! HomePageBlocLoadedState) {
      return;
    }
    final thisState = state as HomePageBlocLoadedState;

    if (thisState.selectedTariffIndex == null) {
      emit(thisState.copyWith(mapAnchor: searchArea));
      return;
    }

    final use = GetCarsByTariffUseCase();
    final carsGetResult = await use(thisState.selectedTariffIndex!, searchArea.anchorPoint, radiusInMeters: searchArea.radius);
    if (carsGetResult is Ok<List<Car>>) {
      emit(thisState.copyWith(mapAnchor: searchArea, cars: carsGetResult.value));
    } else {
      emit(thisState.copyWith(mapAnchor: searchArea));
    }
  }

  Future<void> _initialLoadData(MapSearchArea searchArea, emit) async {
    emit(const HomePageBlocState.loading());

    try {
      final getTariffsResult = await GetActiveTariffsUseCase()();
      if (getTariffsResult is Error<List<Tariff>>) {
        emit(HomePageBlocState.loadError(error: getTariffsResult.error));
        return;
      }
      final tariffs = (getTariffsResult as Ok<List<Tariff>>).value;

      if (tariffs.isEmpty) {
        const HomePageBlocState.loadError(error: "Нет доступных тарифов.");
        return;
      }

      final maybeTariff = tariffs.firstOrNull;
      late final List<Car> cars;
      if (maybeTariff != null){
        final selectedTariffId = maybeTariff.id;
        cars = await _loadTariffRelatedCars(selectedTariffId, searchArea);
      } else {
        cars = [];
      }

      emit(HomePageBlocState.loaded(
          mapAnchor: searchArea,
          cars: cars,
          tariffs: tariffs,
          selectedTariffIndex: 0,
      ));
    } catch (e) {
      emit(const HomePageBlocState.loadError(error: "Ошибка при загрузке"));
    }
  }

  Future<void> _tryRentCar(HomePageBlocTryRentEvent event, emit) async {
    if (state is! HomePageBlocLoadedState) {
      return;
    }

    final oldState = (state as HomePageBlocLoadedState);
    if (oldState.selectedCarId == null) {
      emit(const HomePageBlocState.unsuccessfulRent(error: "Выберите машину."));
      return;
    }

    emit(const HomePageBlocState.renting());
    try {
      final List<Car> cars = oldState.cars;
      final carIndex = cars.indexWhere((element) => element.id == oldState.selectedCarId!);
      final car = cars[carIndex];

      final bookCarUseCase = BookCarUseCase();
      final bookResult = await bookCarUseCase(car.id, event.startRent, event.endDate);

      if (bookResult is Ok<bool> && bookResult.value) {
        emit(const HomePageBlocState.successfulRent());
      } else {
        emit(const HomePageBlocState.unsuccessfulRent(error: "Не удалось забронировать автомобиль."));
      }
    } catch (e) {
      emit(const HomePageBlocState.unsuccessfulRent());
    }
  }

  /*
  final hasLocationPermission = await locationProvider.checkPermission();
  if (!(hasLocationPermission || (await locationProvider.requestPermission()))) {
  return;
  }

  final userLocation = await locationProvider.getCurrentLocation();
  final searchParams = MapSearchArea(userLocation);
  */

  Future<void> _selectAnotherTariff(int tariffIndex, emit) async {
    if (state is! HomePageBlocLoadedState) {
      return;
    }
    final oldState = (state as HomePageBlocLoadedState);
    emit(const HomePageBlocState.loading());

    try {
      final searchArea = oldState.mapAnchor;
      final selectedTariff = oldState.tariffs[tariffIndex];

      emit(oldState.copyWith(cars: [], selectedTariffIndex: tariffIndex, selectedCarId: null, mapAnchor: searchArea));

      final cars = await _loadTariffRelatedCars(selectedTariff.id, searchArea);
      if (state is HomePageBlocLoadedState) {
        emit((state as HomePageBlocLoadedState).copyWith(cars: cars));
      }
    }
    catch(e){
      emit(const HomePageBlocState.loadError(error: "Ошибка при загрузке."));
    }
  }

  Future<void> _selectCar(int carId, emit) async {
    if (state is! HomePageBlocLoadedState) {
      return;
    }
    final loadedState = state as HomePageBlocLoadedState;
    emit(loadedState.copyWith(selectedCarId: carId));
  }

  Future<List<Car>> _loadTariffRelatedCars(int tariffId, MapSearchArea searchArea) async {
    final getCarsUseCase = GetCarsByTariffUseCase();
    final tariffCarsResult = await getCarsUseCase(
        tariffId,
        searchArea.anchorPoint,
        radiusInMeters: searchArea.radius);
    if (tariffCarsResult is Ok<List<Car>>){
      final cars = tariffCarsResult.value;

      return cars;
    } else {
      return <Car>[];
    }
  }
}

