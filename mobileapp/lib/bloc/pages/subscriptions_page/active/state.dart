import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/booked_car/booked_car.dart';

part 'state.freezed.dart';

@freezed
sealed class ActiveSubscriptionsState with _$ActiveSubscriptionsState {
  const factory ActiveSubscriptionsState.loaded({required List<BookedCar> cars, String? error}) = ActiveSubscriptionsLoadedState;
  const factory ActiveSubscriptionsState.loadError({String? error}) = ActiveSubscriptionsLoadErrorState;
  const factory ActiveSubscriptionsState.loading() = ActiveSubscriptionsLoadingState;
}
