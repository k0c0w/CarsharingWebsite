import 'package:bloc/bloc.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/state.dart';

class SubscriptionsCubit extends Cubit<SubscriptionsCubitState> {
  SubscriptionsCubit(super.initialState);

  void openActiveSubscriptions() {
    emit(SubscriptionsCubitState.activeSubscriptions);
  }

  void openHistory() {
    emit(SubscriptionsCubitState.subscriptionHistory);
  }
}

