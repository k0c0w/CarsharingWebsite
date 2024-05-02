import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/components/drawer.dart';
import 'package:mobileapp/ui/components/error_page.dart';
import 'package:mobileapp/ui/components/specific/home_page/map.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';

class HomePageWidget extends StatelessWidget {
  const HomePageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<HomePageBloc>(
      create: (_) => HomePageBloc(const HomePageBlocState.loading()),
      lazy: false,
      child: const _View(),
    );
  }
}

class _View extends StatefulWidget {
  const _View();
  @override
  State<StatefulWidget> createState() => _ViewState();
}

class _ViewState extends State<_View> {
  final GlobalKey<ScaffoldState> scaffoldKey = GlobalKey<ScaffoldState>();

  _ViewState();

  void _openDrawer () => scaffoldKey.currentState!.openDrawer();

  @override
  Widget build(BuildContext context) {

    return SafeArea(
        top: true,
        minimum: const EdgeInsets.only(top: 10),
        child: Scaffold(
          key: scaffoldKey,
          extendBodyBehindAppBar: true,
          appBar: DriveHomePageAppBar(openDrawer: _openDrawer),
          drawer: const DriveDrawer(),
          body: BlocConsumer<HomePageBloc, HomePageBlocState>(
            listener: (ctx, state) {
              if (state is HomePageBlocRentingState) {
                Navigator.of(context).pop();
              } else if(state is HomePageBlocSuccessfulRentState) {
                Navigator.of(ctx).pushNamed(DriveRoutes.userSubscriptions);
              } else if (state is HomePageBlocUnsuccessfulRentState) {
                ScaffoldMessenger.of(ctx).showSnackBar(
                  const SnackBar(content: Text('Аренда автомобиля не удалась.')),
                );
              }
            },
            buildWhen: (prev, state) => state is HomePageBlocLoadErrorState
              || state is HomePageBlocLoadingState || state is HomePageBlocLoadErrorState,
            builder: (ctx, state) {
              if (state is HomePageBlocLoadErrorState) {
                return LoadPageErrorMessageAtCenter(
                  customErrorMessage: state.error,
                  /*
                  onRetryPressed: () => ctx
                      .read<HomePageBloc>()
                      .add(HomePageBlocEvent.load),

                   */
                );
              }

              return const MapWidget();
            },
          )
        )
    );
  }
}