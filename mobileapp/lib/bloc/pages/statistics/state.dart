import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/tariffs_usage/tariffs_usage.dart';

part 'state.freezed.dart';

@freezed
class StatisticsState with _$StatisticsState {
  const factory StatisticsState.subscribing() = _StatisticsSubscribingState;
  const factory StatisticsState.subscriptionError({String? error}) = _StatisticsSubscriptionErrorState;
  const factory StatisticsState.received({
    required List<TariffUsageStats> stats,
  }) = _StatisticsReceivedState;
}