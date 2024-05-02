import 'package:freezed_annotation/freezed_annotation.dart';
import 'package:mobileapp/domain/entities/location/geopoint.dart';

part 'map_search_area.freezed.dart';

@freezed
class MapSearchArea with _$MapSearchArea {
  const factory MapSearchArea(
      GeoPoint anchorPoint,
      {
        @Default(20)
        double radius
      }
      ) = _MapSearchArea;
}