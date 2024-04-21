import 'package:geolocator/geolocator.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';

class LocationProvider {
  final defaultLocation = const MoscowLocationGeoPoint();

  Future<GeoPoint> getCurrentLocation() {
    return Geolocator.getCurrentPosition().then((value) {
      return GeoPoint(lat: value.latitude, long: value.longitude);
    }).catchError(
          (_) => defaultLocation,
    );
  }

  Future<bool> requestPermission() {
    return Geolocator.requestPermission()
        .then((value) =>
    value == LocationPermission.always ||
        value == LocationPermission.whileInUse)
        .catchError((_) => false);
  }

  Future<bool> checkPermission() {
    return Geolocator.checkPermission()
        .then((value) =>
    value == LocationPermission.always ||
        value == LocationPermission.whileInUse)
        .catchError((_) => false);
  }
}