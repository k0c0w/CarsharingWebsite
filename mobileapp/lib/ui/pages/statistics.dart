
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:mobileapp/bloc/pages/statistics/cubit.dart';
import 'package:mobileapp/bloc/pages/statistics/state.dart';
import 'package:mobileapp/domain/api_clients/rabbitmq_client.dart';
import 'package:mobileapp/domain/entities/tariffs_usage/tariffs_usage.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/Components/styles.dart';
import 'package:mobileapp/ui/components/appbar.dart';
import 'package:mobileapp/ui/components/center_circular_progress_indicator.dart';
import 'package:mobileapp/ui/components/error_page.dart';

class TariffUsageStatisticsPageWidget extends StatelessWidget {

  const TariffUsageStatisticsPageWidget();

  @override
  Widget build(BuildContext context) {
    return BlocProvider<StatisticsCubit>(
      create: (_) {
        final cubit = StatisticsCubit(
            getIt<RabbitMqClient>(), const StatisticsState.subscribing());
        cubit.trySubscribeUpdates().ignore();

        return cubit;
      },
      lazy: false,
      child: Scaffold(
        appBar: DriveAppBar(title: 'ДНЕВНАЯ СТАТИСТИКА',),
        body: _View(),
      ),
    );
  }
}

class _View extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final cubit = context.read<StatisticsCubit>();
    return BlocBuilder<StatisticsCubit, StatisticsState>(
        builder: (_, state) {

          return state.map(
              subscribing: (_) => const CenterCircularProgressIndicator(),
              subscriptionError: (_) =>
                  LoadPageErrorMessageAtCenter(
                    onRetryPressed: () => cubit.trySubscribeUpdates(),),
              received: (state) {
                if (state.stats.isEmpty) {
                  return const Center(
                    child: Text(
                        "Похоже, что никто не использовал сервис сегодня."),
                  );
                }

                return _ListView(state.stats);
              }
          );
        });
  }
}

class _ListView extends StatelessWidget {
  static const List<Color> colorsPool = [
    DriveColors.lightBlueColor,
    DriveColors.deepBlueColor,
    DriveColors.darkBlueColor
  ];

  final List<TariffUsageStats> stats;

  const _ListView(this.stats);

  @override
  Widget build(BuildContext context) {
    return Padding(
        padding: const EdgeInsets.only(top: 20),
        child: ListView.builder(
          itemCount: stats.length,
          itemBuilder: (ctx, index) {
            final item = stats[index];

            return Padding(
              padding: const EdgeInsets.symmetric(vertical: 15),
              child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    Text(item.tariffName, style: GoogleFonts.orbitron(
                        color: colorsPool[index % colorsPool.length],
                        textStyle: const TextStyle(fontSize: 15, fontWeight: FontWeight.w600)
                    ),
                    ),
                    Text(item.usageCount.toString(),
                      style: const TextStyle(
                        color: Colors.black,
                        fontSize: 15,
                      ),
                    )
                  ]),
            );
          },
        )
    );
  }

}
