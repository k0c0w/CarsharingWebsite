import 'package:freezed_annotation/freezed_annotation.dart';

part 'geopoint.freezed.dart';
part 'geopoint.g.dart';

@freezed
class GeoPoint with _$GeoPoint {

  static GeoPoint moscowLocationGeoPoint = const GeoPoint(55.7522200, 37.6155600);


  const factory GeoPoint(
      double lat,
      double long
      ) = _GeoPoint;

  factory GeoPoint.fromJson(Map<String, Object?> json)
  => _$GeoPointFromJson(json);
}
