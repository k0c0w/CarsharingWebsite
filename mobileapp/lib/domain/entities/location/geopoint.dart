class GeoPoint {
  final double lat;
  final double long;

  const GeoPoint({
    required this.lat,
    required this.long,
  });
}

class MoscowLocationGeoPoint extends GeoPoint {
  const MoscowLocationGeoPoint({
    super.lat = 55.7522200,
    super.long = 37.6155600,
  });
}