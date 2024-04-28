
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/events.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/state.dart';
import 'package:mobileapp/domain/entities/booked_car/booked_car.dart';

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
      //todo: загрузить список подписок

      final cars = [const BookedCar(id: 1, model: "Land", brand: "Cruiser", licensePlate: "А126ВУ", isOpen: false)];
      emit(ActiveSubscriptionsState.loaded(cars: cars));
    } catch(e) {
      emit(const ActiveSubscriptionsState.loadError());
    }
  }
}