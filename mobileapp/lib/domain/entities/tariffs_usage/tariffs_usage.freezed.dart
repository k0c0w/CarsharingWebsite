// coverage:ignore-file
// GENERATED CODE - DO NOT MODIFY BY HAND
// ignore_for_file: type=lint
// ignore_for_file: unused_element, deprecated_member_use, deprecated_member_use_from_same_package, use_function_type_syntax_for_parameters, unnecessary_const, avoid_init_to_null, invalid_override_different_default_values_named, prefer_expression_function_bodies, annotate_overrides, invalid_annotation_target, unnecessary_question_mark

part of 'tariffs_usage.dart';

// **************************************************************************
// FreezedGenerator
// **************************************************************************

T _$identity<T>(T value) => value;

final _privateConstructorUsedError = UnsupportedError(
    'It seems like you constructed your class using `MyClass._()`. This constructor is only meant to be used by freezed and you are not supposed to need it nor use it.\nPlease check the documentation here for more information: https://github.com/rrousselGit/freezed#adding-getters-and-methods-to-our-models');

TariffUsageStats _$TariffUsageStatsFromJson(Map<String, dynamic> json) {
  return _TariffUsageStats.fromJson(json);
}

/// @nodoc
mixin _$TariffUsageStats {
  @JsonKey(name: "TariffName")
  String get tariffName => throw _privateConstructorUsedError;
  @JsonKey(name: "UsageCount")
  int get usageCount => throw _privateConstructorUsedError;

  Map<String, dynamic> toJson() => throw _privateConstructorUsedError;
  @JsonKey(ignore: true)
  $TariffUsageStatsCopyWith<TariffUsageStats> get copyWith =>
      throw _privateConstructorUsedError;
}

/// @nodoc
abstract class $TariffUsageStatsCopyWith<$Res> {
  factory $TariffUsageStatsCopyWith(
          TariffUsageStats value, $Res Function(TariffUsageStats) then) =
      _$TariffUsageStatsCopyWithImpl<$Res, TariffUsageStats>;
  @useResult
  $Res call(
      {@JsonKey(name: "TariffName") String tariffName,
      @JsonKey(name: "UsageCount") int usageCount});
}

/// @nodoc
class _$TariffUsageStatsCopyWithImpl<$Res, $Val extends TariffUsageStats>
    implements $TariffUsageStatsCopyWith<$Res> {
  _$TariffUsageStatsCopyWithImpl(this._value, this._then);

  // ignore: unused_field
  final $Val _value;
  // ignore: unused_field
  final $Res Function($Val) _then;

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? tariffName = null,
    Object? usageCount = null,
  }) {
    return _then(_value.copyWith(
      tariffName: null == tariffName
          ? _value.tariffName
          : tariffName // ignore: cast_nullable_to_non_nullable
              as String,
      usageCount: null == usageCount
          ? _value.usageCount
          : usageCount // ignore: cast_nullable_to_non_nullable
              as int,
    ) as $Val);
  }
}

/// @nodoc
abstract class _$$TariffUsageStatsImplCopyWith<$Res>
    implements $TariffUsageStatsCopyWith<$Res> {
  factory _$$TariffUsageStatsImplCopyWith(_$TariffUsageStatsImpl value,
          $Res Function(_$TariffUsageStatsImpl) then) =
      __$$TariffUsageStatsImplCopyWithImpl<$Res>;
  @override
  @useResult
  $Res call(
      {@JsonKey(name: "TariffName") String tariffName,
      @JsonKey(name: "UsageCount") int usageCount});
}

/// @nodoc
class __$$TariffUsageStatsImplCopyWithImpl<$Res>
    extends _$TariffUsageStatsCopyWithImpl<$Res, _$TariffUsageStatsImpl>
    implements _$$TariffUsageStatsImplCopyWith<$Res> {
  __$$TariffUsageStatsImplCopyWithImpl(_$TariffUsageStatsImpl _value,
      $Res Function(_$TariffUsageStatsImpl) _then)
      : super(_value, _then);

  @pragma('vm:prefer-inline')
  @override
  $Res call({
    Object? tariffName = null,
    Object? usageCount = null,
  }) {
    return _then(_$TariffUsageStatsImpl(
      tariffName: null == tariffName
          ? _value.tariffName
          : tariffName // ignore: cast_nullable_to_non_nullable
              as String,
      usageCount: null == usageCount
          ? _value.usageCount
          : usageCount // ignore: cast_nullable_to_non_nullable
              as int,
    ));
  }
}

/// @nodoc
@JsonSerializable()
class _$TariffUsageStatsImpl implements _TariffUsageStats {
  const _$TariffUsageStatsImpl(
      {@JsonKey(name: "TariffName") required this.tariffName,
      @JsonKey(name: "UsageCount") required this.usageCount});

  factory _$TariffUsageStatsImpl.fromJson(Map<String, dynamic> json) =>
      _$$TariffUsageStatsImplFromJson(json);

  @override
  @JsonKey(name: "TariffName")
  final String tariffName;
  @override
  @JsonKey(name: "UsageCount")
  final int usageCount;

  @override
  String toString() {
    return 'TariffUsageStats(tariffName: $tariffName, usageCount: $usageCount)';
  }

  @override
  bool operator ==(Object other) {
    return identical(this, other) ||
        (other.runtimeType == runtimeType &&
            other is _$TariffUsageStatsImpl &&
            (identical(other.tariffName, tariffName) ||
                other.tariffName == tariffName) &&
            (identical(other.usageCount, usageCount) ||
                other.usageCount == usageCount));
  }

  @JsonKey(ignore: true)
  @override
  int get hashCode => Object.hash(runtimeType, tariffName, usageCount);

  @JsonKey(ignore: true)
  @override
  @pragma('vm:prefer-inline')
  _$$TariffUsageStatsImplCopyWith<_$TariffUsageStatsImpl> get copyWith =>
      __$$TariffUsageStatsImplCopyWithImpl<_$TariffUsageStatsImpl>(
          this, _$identity);

  @override
  Map<String, dynamic> toJson() {
    return _$$TariffUsageStatsImplToJson(
      this,
    );
  }
}

abstract class _TariffUsageStats implements TariffUsageStats {
  const factory _TariffUsageStats(
          {@JsonKey(name: "TariffName") required final String tariffName,
          @JsonKey(name: "UsageCount") required final int usageCount}) =
      _$TariffUsageStatsImpl;

  factory _TariffUsageStats.fromJson(Map<String, dynamic> json) =
      _$TariffUsageStatsImpl.fromJson;

  @override
  @JsonKey(name: "TariffName")
  String get tariffName;
  @override
  @JsonKey(name: "UsageCount")
  int get usageCount;
  @override
  @JsonKey(ignore: true)
  _$$TariffUsageStatsImplCopyWith<_$TariffUsageStatsImpl> get copyWith =>
      throw _privateConstructorUsedError;
}
