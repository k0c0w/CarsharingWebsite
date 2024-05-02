import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/events.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/state.dart';
import 'package:mobileapp/domain/entities/booked_car/booked_car.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/subscriptions_cases.dart';

class ActiveSubscriptionsPageBloc
    extends Bloc<ActiveSubscriptionsEvent, ActiveSubscriptionsState> {
  ActiveSubscriptionsPageBloc(super.initialState) {
    on<ActiveSubscriptionsTurnLightsEvent>(_onTurnLights, transformer: droppable());
    on<ActiveSubscriptionsStartEngineEvent>(_onStartEngine, transformer: droppable());
    on<ActiveSubscriptionsOpenCarEvent>(_onOpen, transformer: droppable());
    on<ActiveSubscriptionsCloseCarEvent>(_onClose, transformer: droppable());
    on<ActiveSubscriptionsLoadEvent>(_onLoad, transformer: sequential());
  }

  Future<void> _onTurnLights(ActiveSubscriptionsTurnLightsEvent event, emit) async {
    try{

    } catch (e) {
    }
  }

  Future<void> _onOpen(ActiveSubscriptionsOpenCarEvent event, emit) async {
    try {

    } catch (e) {

    }
  }

  Future<void> _onClose(ActiveSubscriptionsCloseCarEvent event, emit) async {
    try {

    } catch(e) {

    }
  }

  Future<void> _onStartEngine(ActiveSubscriptionsStartEngineEvent event, emit) async {
    try{

    } catch(e) {

    }
  }

  Future<void> _onLoad(ActiveSubscriptionsLoadEvent event, emit) async {
    emit(const ActiveSubscriptionsState.loading());
    try{
      final bookedCars = await RetrieveSubscriptionsUseCase()();
      if (bookedCars is Error<List<BookedCar>>) {
        emit(const ActiveSubscriptionsState.loadError());
        return;
      }

      emit(ActiveSubscriptionsState.loaded(cars: (bookedCars as Ok<List<BookedCar>>).value));
    } catch(e) {
      emit(const ActiveSubscriptionsState.loadError());
    }
  }
}