import 'package:bloc/bloc.dart';
import 'package:bloc_concurrency/bloc_concurrency.dart';
import 'package:mobileapp/bloc/pages/payment/events.dart';
import 'package:mobileapp/bloc/pages/payment/state.dart';
import 'package:mobileapp/domain/results.dart';
import 'package:mobileapp/domain/use_cases/payment_cases.dart';

class PaymentPageBloc extends Bloc<PaymentBlocPayEvent, PaymentPageState> {
  PaymentPageBloc(super.initialState) {
    on<PaymentBlocPayEvent>(_onPay, transformer: sequential());
  }

  Future<void> _onPay(PaymentBlocPayEvent event, emit) async {
    emit(PaymentPageState.pending);

    final paymentResult = await PaymentUseCase()(event.money);

    final newState = paymentResult is Ok<bool> && paymentResult.value
      ? PaymentPageState.success : PaymentPageState.error;
    emit(newState);
  }
}