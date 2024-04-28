import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/bloc.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/events.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/active/state.dart';
import 'package:mobileapp/ui/components/car_card.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/components/error_page.dart';

class SubscriptionsActiveList extends StatelessWidget {
  const SubscriptionsActiveList({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<ActiveSubscriptionsPageBloc>(
      create: (_) {
        final bloc = ActiveSubscriptionsPageBloc(const ActiveSubscriptionsState.loading());
        bloc.add(const ActiveSubscriptionsEvent.load());

        return bloc;
      },
      lazy: false,
      child: _View(),
    );
  }
}

class _View extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<ActiveSubscriptionsPageBloc, ActiveSubscriptionsState>(
        builder: (ctx, state) {
          final bloc = ctx.read<ActiveSubscriptionsPageBloc>();
          return switch (state) {
            ActiveSubscriptionsLoadedState(: final cars)
            => ListView.builder(
                scrollDirection: Axis.vertical,
                shrinkWrap: true,
                itemCount: cars.length,
                itemBuilder: (context, index) {
                  final car = cars[index];
                  final carId = car.id;
                  final isOpened = car.isOpen;

                  return SubscriptionCard(
                      isOpened: isOpened,
                      onEngineStart: () => bloc.add(ActiveSubscriptionsEvent.startEngine(carId)),
                      onLightsUp: () => bloc.add(ActiveSubscriptionsEvent.turnLightsCar(carId)),
                      onOpenOrCloseCar: isOpened ?
                          () => bloc.add(ActiveSubscriptionsEvent.closeCar(carId))
                          : () => bloc.add(ActiveSubscriptionsEvent.openCar(carId)),
                      name: "${car.brand} ${car.model}",
                      description: car.licensePlate
                  );
                }
            ),
            ActiveSubscriptionsLoadingState() => const CenterCircularProgressIndicator(),
            ActiveSubscriptionsLoadErrorState(:final error) => LoadPageErrorMessageAtCenter(
              customErrorMessage: error,
              onRetryPressed: () => bloc.add(const ActiveSubscriptionsEvent.load()),
            ),
            _ => throw UnimplementedError("Unknown state."),
          };
        });
  }
}
