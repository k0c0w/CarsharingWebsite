import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/cubit.dart';
import 'package:mobileapp/bloc/pages/subscriptions_page/state.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/pages/subscriptions/active_cars.dart';
import 'package:mobileapp/ui/pages/subscriptions/history_cars.dart';

class SubscriptionsPageWidget extends StatelessWidget {
  const SubscriptionsPageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<SubscriptionsCubit>(
        create: (_) => SubscriptionsCubit(SubscriptionsCubitState.activeSubscriptions),
        child: const _View()
    );
  }
}

class _View extends StatelessWidget {
  const _View();

  @override
  Widget build(BuildContext context) {
    final cubit = context.read<SubscriptionsCubit>();
    final state = context.select((SubscriptionsCubit cubit) => cubit.state);
    final List<bool> isSelected = <bool>[state == SubscriptionsCubitState.activeSubscriptions, state == SubscriptionsCubitState.subscriptionHistory];
    return Scaffold(
      appBar: DriveAppBar(title: "ПОДПИСКИ"),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        children: [
          const Divider(
            height: 20,
            color: Colors.transparent,
          ),
          Center(
              child: ToggleButtons(
                isSelected: isSelected,
                onPressed: (int index) => index == 0 ? cubit.openActiveSubscriptions() : cubit.openHistory(),
                borderRadius: const BorderRadius.all(Radius.circular(10)),
                selectedColor: Colors.white,
                fillColor: const Color.fromRGBO(5, 59, 74, 1),
                color: const Color.fromRGBO(117, 124, 126, 1),
                constraints: const BoxConstraints(
                minHeight: 33.0,
                minWidth: 160.0,
                ),
                children: const [Text("АКТИВНЫЕ"), Text("ИСТОРИЯ")],
              )
          ),
          state == SubscriptionsCubitState.activeSubscriptions
              ? const SubscriptionsActiveList() : const SubscriptionPageHistoryList()
        ],
      ),
    );
  }
}
