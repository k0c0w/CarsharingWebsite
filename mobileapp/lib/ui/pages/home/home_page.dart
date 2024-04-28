import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';
import 'package:mobileapp/domain/providers/location_provider.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/components/drawer.dart';
import 'package:mobileapp/ui/components/error_page.dart';
import 'package:mobileapp/ui/pages/home/modal_page.dart';
import 'package:mobileapp/ui/pages/pages_list.dart';
import 'package:yandex_mapkit/yandex_mapkit.dart';

class HomePageWidget extends StatelessWidget {
  const HomePageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider<HomePageBloc>(
      create: (context) {
        final bloc = HomePageBloc(const HomePageBlocState.loading());
        bloc.add(const HomePageBlocEvent.load());

        return bloc;
      },
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

  final LocationProvider locationProvider = LocationProvider();
  final mapControllerCompleter = Completer<YandexMapController>();

  _ViewState();

  Future<void> _initPermission() async {
    if (!await locationProvider.checkPermission()) {
      await locationProvider.requestPermission();
    }
    await _fetchCurrentLocation();
  }

  Future<void> _fetchCurrentLocation() async {
    GeoPoint location;
    final defLocation = locationProvider.defaultLocation;
    try {
      location = await locationProvider.getCurrentLocation();
    } catch (_) {
      location = defLocation;
    }
    _moveToCurrentLocation(location);
  }

  Future<void> _moveToCurrentLocation(
      GeoPoint appLatLong,
      ) async {
    (await mapControllerCompleter.future).moveCamera(
      animation: const MapAnimation(type: MapAnimationType.linear, duration: 1),
      CameraUpdate.newCameraPosition(
        CameraPosition(
          target: Point(
            latitude: appLatLong.lat,
            longitude: appLatLong.long,
          ),
          zoom: 12,
        ),
      ),
    );
  }

  @override
  void initState() {
    super.initState();
    _initPermission().ignore();
  }

  void _openDrawer () => scaffoldKey.currentState!.openDrawer();

  void _onCarRentPressed(HomePageBloc bloc, DateTime startDate, DateTime endDate)
    => bloc.add(HomePageBlocEvent.tryBook(startDate, endDate));

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
            buildWhen: (ctx, state) => state is HomePageBlocLoadErrorState
              || state is HomePageBlocLoadingState || state is HomePageBlocLoadErrorState,
            builder: (ctx, state) {
              if (state is HomePageBlocLoadErrorState) {
                return LoadPageErrorMessageAtCenter(
                  customErrorMessage: state.error,
                  onRetryPressed: () => ctx
                      .read<HomePageBloc>()
                      .add(const HomePageBlocEvent.load()),
                );
              }

              final bloc = context.read<HomePageBloc>();
              final screenWidgets = <Widget>[
                Expanded(
                  child: YandexMap(
                    onMapCreated: (controller) {
                      mapControllerCompleter.complete(controller);
                      //todo delete
                      showModalBottomSheet(
                          context: context,
                          builder: HomePageRentModalWidget(injectableBloc: bloc,)
                              .build);
                    },
                  ),
                ),
              ];
              if (state is HomePageBlocLoadedState) {
                //todo: add cars placemarks
              }

              return Column(
                children: screenWidgets,
              );
            },
          )
        )
    );
  }
}