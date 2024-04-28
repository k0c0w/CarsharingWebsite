import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:mobileapp/bloc/payment/events.dart';
import 'package:mobileapp/bloc/payment/state.dart';

class PaymentPageBloc extends Bloc<PaymentBlocPayEvent, PaymentPageState> {
  PaymentPageBloc(super.initialState) {
    on<PaymentBlocPayEvent>(_onPay, transformer: sequential());
  }

  Future<void> _onPay(event, emit) async {
    emit(PaymentPageState.pending);

    try {

      await Future.delayed(const Duration(seconds: 5));

      //todo: pay money
      emit(PaymentPageState.success);
    } catch (e) {
      emit(PaymentPageState.error);
    }
  }
}