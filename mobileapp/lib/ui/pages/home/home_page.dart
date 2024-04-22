import 'dart:async';
import 'package:flutter/material.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';
import 'package:mobileapp/domain/providers/location_provider.dart';
import 'package:mobileapp/ui/Components/appbar.dart';
import 'package:mobileapp/ui/components/drawer.dart';
import 'package:mobileapp/ui/pages/home/modal_page.dart';
import 'package:yandex_mapkit/yandex_mapkit.dart';


class HomePageWidget extends StatelessWidget {
  const HomePageWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return const _View();
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

  @override
  Widget build(BuildContext context) {

    return SafeArea(
        child: Scaffold(
          key: scaffoldKey,
          extendBodyBehindAppBar: true,
          appBar: DriveHomePageAppBar(openDrawer: _openDrawer),
          drawer: const DriveDrawer(),
          body: Column(
            children: [
              Expanded(
                child: YandexMap(
                  onMapCreated: (controller) {
                    mapControllerCompleter.complete(controller);
                    //todo delete
                    showModalBottomSheet(context: context, builder: HomePageRentModalWidget().build);
                  },
                ),
              )
            ],
          ),
        )
    );
  }
}