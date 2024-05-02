import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/bloc.dart';
import 'package:mobileapp/bloc/pages/home_page/events.dart';
import 'package:mobileapp/bloc/pages/home_page/map_search_area.dart';
import 'package:mobileapp/bloc/pages/home_page/state.dart';
import 'package:mobileapp/domain/entities/car/car.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';
import 'package:mobileapp/domain/providers/location_provider.dart';
import 'package:mobileapp/main.dart';
import 'package:mobileapp/ui/pages/home/book_page.dart';
import 'package:yandex_mapkit/yandex_mapkit.dart';

class MapWidget extends StatefulWidget {
  const MapWidget({super.key});

  @override
  State<StatefulWidget> createState() => _MapState();
}

class _MapState extends State<MapWidget> {

  final LocationProvider locationProvider = getIt<LocationProvider>();
  final mapControllerCompleter = Completer<YandexMapController>();

  void _onMapCreated(YandexMapController controller, BuildContext context) async {
    mapControllerCompleter.complete(controller);

    final position = await controller.getUserCameraPosition();
    final positionTarget = position!.target;
    final anchorPoint = GeoPoint(positionTarget.latitude, positionTarget.longitude);
    final radius = 500.0;//position.zoom;
    final searchArea = MapSearchArea(anchorPoint, radius: radius);

    context.read<HomePageBloc>().add(HomePageBlocEvent.initialLoad(searchArea));
  }

  void _onCameraPositionChanged(
      CameraPosition position,
      CameraUpdateReason updateReason,
      bool finished,
      BuildContext context
      ) {
    if (!finished) {
      return;
    }

    final target = position.target;
    final geoPoint = GeoPoint(target.latitude, target.longitude);
    final zoom = position.zoom;
    context.read<HomePageBloc>()
        .add(HomePageBlocEvent.changeAnchor(MapSearchArea(geoPoint)));
  }

  PlacemarkMapObject _carToPlaceMark(Car car, void Function(Car) onTap) {
    final carPos = car.location;
    final mapId = car.id;
    return PlacemarkMapObject(
        mapId: MapObjectId(mapId.toString()), 
        point: Point(latitude: carPos.lat, longitude: carPos.long),
        isDraggable: false,
        isVisible: true,
        onTap: (_, __) => onTap(car),
        icon: PlacemarkIcon.single(PlacemarkIconStyle(
            image: BitmapDescriptor.fromAssetImage('assets/car.png'),
            scale: 2
          )
        )
    );
  }

  void _openBookPage(Car car, BuildContext context) {
    final bloc = context.read<HomePageBloc>();
    showModalBottomSheet(context: context, builder: HomePageCarBookingWidget(injectableBloc: bloc).build);
  }

  List<PlacemarkMapObject> _carsToPlaceMarks(List<Car> cars, BuildContext context) {
    return cars
        .map((car) => _carToPlaceMark(car, (car) => _openBookPage(car, context)))
        .toList();
  }

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
      CameraUpdate.newCameraPosition(_createCameraPosition(appLatLong)),
    );
  }

  CameraPosition _createCameraPosition(GeoPoint point, [double zoom = 12])
    => CameraPosition(
      target: Point(
        latitude: point.lat,
        longitude: point.long,
      ),
      zoom: 12,
    );

  @override
  void initState() {
    super.initState();
    _initPermission().ignore();
  }

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<HomePageBloc, HomePageBlocState>(
      buildWhen: (prev, current) => prev is! HomePageBlocLoadedState && current is HomePageBlocLoadedState
      ||  prev is HomePageBlocLoadedState && current is HomePageBlocLoadedState
          && !listEquals(prev.cars, current.cars),
      builder: (ctx, state) {
        final List<MapObject<dynamic>> carPlaceMarks =
          state is HomePageBlocLoadedState ? _carsToPlaceMarks(state.cars, ctx) : [];

        return YandexMap(
          onCameraPositionChanged: (camPos, updateReason, finished)
                => _onCameraPositionChanged(camPos, updateReason, finished, ctx),
          rotateGesturesEnabled: false,
          tiltGesturesEnabled: false,
          zoomGesturesEnabled: false,

          onMapCreated: (mapController) => _onMapCreated(mapController, ctx),
          mapObjects: carPlaceMarks,
        );
      },
    );
  }
}