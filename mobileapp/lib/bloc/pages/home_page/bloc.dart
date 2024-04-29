import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';
import 'package:mobileapp/domain/entities/tariff/tariff.dart';

class HomePageBloc extends Bloc<HomePageBlocEvent, HomePageBlocState> {
  HomePageBloc(super.initialState) {
    on<HomePageBlocEvent>(_onEvent, transformer: droppable());
  }

  Future<void> _onEvent(HomePageBlocEvent event, emit) async {

    final future = switch (event) {
      HomePageBlocLoadEvent() => _loadInitialData(emit),
      HomePageBlocTryRentEvent() => _tryRentCar(event, emit),
      HomePageBlocSelectAnotherTariffEvent(:final tariffIndex) => _selectAnotherTariff(tariffIndex, emit),
      HomePageBlocSelectCarEvent(:final carId) =>  _selectCar(carId, emit),
    };

    await future;
  }

  Future<void> _loadInitialData(emit) async {
    emit(const HomePageBlocState.loading());

    try {
      await Future.delayed(Duration(seconds: 5));

      final cars = <Car>[
        Car(id: 1, model: "Crown", brand: "Toyota", carModelDescription: "gdfgdfg",
            location: GeoPoint(55, 55), licensePlate: "А726CY116"),
      ];

      final tariffs = <Tariff>[
        Tariff(1, "TRAVEL", 2500),
      ];

      emit(HomePageBlocState.loaded(cars: cars, tariffs: tariffs));
    } catch (e) {
      emit(HomePageBlocState.loadError(error: "Ошибка при загрузке"));
    }
  }

  Future<void> _tryRentCar(HomePageBlocTryRentEvent event, emit) async {
    if (state is! HomePageBlocLoadedState) {
      return;
    }
    final oldState = (state as HomePageBlocLoadedState);

    emit(const HomePageBlocState.renting());
    try {


      //todo: rent car here

      emit(const HomePageBlocState.successfulRent());
      emit(const HomePageBlocEvent.load());
    } catch (e) {
      // only on unsucessfull error
      emit(const HomePageBlocState.unsuccessfulRent());
      // on other networks
      // emit(HomePageBlocState.loadError());
    }
  }

  Future<void> _selectAnotherTariff(int tariffIndex, emit) async {
    if (state is! HomePageBlocLoadedState) {
      return;
    }
    final oldState = (state as HomePageBlocLoadedState);
    emit(const HomePageBlocState.loading());


    try {
      final cars = <Car>[

      ];

      // todo: try to load cars from tariff

      final newState = oldState.copyWith(selectedTariffIndex: tariffIndex, cars: cars);
      emit(newState);
    }
    catch(e){
      emit(HomePageBlocState.loadError(error: "Ошибка при загрузке"));
    }
  }

  Future<void> _selectCar(int carId, emit) async {

  }
}

