//
//  Generated code. Do not modify.
//  source: analytics.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:async' as $async;
import 'dart:core' as $core;

import 'package:grpc/service_api.dart' as $grpc;
import 'package:protobuf/protobuf.dart' as $pb;

import 'analytics.pb.dart' as $1;
import 'package:mobileapp/utils/grpc/google/empty.pb.dart' as $0;

export 'analytics.pb.dart';

@$pb.GrpcServiceName('analytics.AnalyticsService')
class AnalyticsServiceClient extends $grpc.Client {
  static final _$subscribeTariffsUsageUpdates = $grpc.ClientMethod<$0.Empty, $1.SubscriptionInfo>(
      '/analytics.AnalyticsService/SubscribeTariffsUsageUpdates',
      ($0.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $1.SubscriptionInfo.fromBuffer(value));

  AnalyticsServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options,
        interceptors: interceptors);

  $grpc.ResponseFuture<$1.SubscriptionInfo> subscribeTariffsUsageUpdates($0.Empty request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$subscribeTariffsUsageUpdates, request, options: options);
  }
}

@$pb.GrpcServiceName('analytics.AnalyticsService')
abstract class AnalyticsServiceBase extends $grpc.Service {
  $core.String get $name => 'analytics.AnalyticsService';

  AnalyticsServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.Empty, $1.SubscriptionInfo>(
        'SubscribeTariffsUsageUpdates',
        subscribeTariffsUsageUpdates_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.Empty.fromBuffer(value),
        ($1.SubscriptionInfo value) => value.writeToBuffer()));
  }

  $async.Future<$1.SubscriptionInfo> subscribeTariffsUsageUpdates_Pre($grpc.ServiceCall call, $async.Future<$0.Empty> request) async {
    return subscribeTariffsUsageUpdates(call, await request);
  }

  $async.Future<$1.SubscriptionInfo> subscribeTariffsUsageUpdates($grpc.ServiceCall call, $0.Empty request);
}
