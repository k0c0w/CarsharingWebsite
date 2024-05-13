//
//  Generated code. Do not modify.
//  source: backend/Chat.Microservice/protos/chat.proto
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
import 'package:mobileapp/utils/grpc/google/empty.pb.dart' as $1;
import 'chat.pb.dart' as $0;
export 'chat.pb.dart';

@$pb.GrpcServiceName('chat.MessagingService')
class MessagingServiceClient extends $grpc.Client {
  static final _$sendMessage = $grpc.ClientMethod<$0.FromClientMessage, $0.SendMessageResultMessage>(
      '/chat.MessagingService/SendMessage',
      ($0.FromClientMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.SendMessageResultMessage.fromBuffer(value));
  static final _$getChatStream = $grpc.ClientMethod<$1.Empty, $0.FromServerMessage>(
      '/chat.MessagingService/GetChatStream',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.FromServerMessage.fromBuffer(value));
  static final _$getChatStreamByTopic = $grpc.ClientMethod<$0.ChatSelectorMessage, $0.FromServerMessage>(
      '/chat.MessagingService/GetChatStreamByTopic',
      ($0.ChatSelectorMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.FromServerMessage.fromBuffer(value));

  MessagingServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options,
        interceptors: interceptors);

  $grpc.ResponseFuture<$0.SendMessageResultMessage> sendMessage($0.FromClientMessage request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$sendMessage, request, options: options);
  }

  $grpc.ResponseStream<$0.FromServerMessage> getChatStream($1.Empty request, {$grpc.CallOptions? options}) {
    return $createStreamingCall(_$getChatStream, $async.Stream.fromIterable([request]), options: options);
  }

  $grpc.ResponseStream<$0.FromServerMessage> getChatStreamByTopic($0.ChatSelectorMessage request, {$grpc.CallOptions? options}) {
    return $createStreamingCall(_$getChatStreamByTopic, $async.Stream.fromIterable([request]), options: options);
  }
}

@$pb.GrpcServiceName('chat.MessagingService')
abstract class MessagingServiceBase extends $grpc.Service {
  $core.String get $name => 'chat.MessagingService';

  MessagingServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.FromClientMessage, $0.SendMessageResultMessage>(
        'SendMessage',
        sendMessage_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.FromClientMessage.fromBuffer(value),
        ($0.SendMessageResultMessage value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Empty, $0.FromServerMessage>(
        'GetChatStream',
        getChatStream_Pre,
        false,
        true,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($0.FromServerMessage value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.ChatSelectorMessage, $0.FromServerMessage>(
        'GetChatStreamByTopic',
        getChatStreamByTopic_Pre,
        false,
        true,
        ($core.List<$core.int> value) => $0.ChatSelectorMessage.fromBuffer(value),
        ($0.FromServerMessage value) => value.writeToBuffer()));
  }

  $async.Future<$0.SendMessageResultMessage> sendMessage_Pre($grpc.ServiceCall call, $async.Future<$0.FromClientMessage> request) async {
    return sendMessage(call, await request);
  }

  $async.Stream<$0.FromServerMessage> getChatStream_Pre($grpc.ServiceCall call, $async.Future<$1.Empty> request) async* {
    yield* getChatStream(call, await request);
  }

  $async.Stream<$0.FromServerMessage> getChatStreamByTopic_Pre($grpc.ServiceCall call, $async.Future<$0.ChatSelectorMessage> request) async* {
    yield* getChatStreamByTopic(call, await request);
  }

  $async.Future<$0.SendMessageResultMessage> sendMessage($grpc.ServiceCall call, $0.FromClientMessage request);
  $async.Stream<$0.FromServerMessage> getChatStream($grpc.ServiceCall call, $1.Empty request);
  $async.Stream<$0.FromServerMessage> getChatStreamByTopic($grpc.ServiceCall call, $0.ChatSelectorMessage request);
}
@$pb.GrpcServiceName('chat.ManagementService')
class ManagementServiceClient extends $grpc.Client {
  static final _$getChatHistory = $grpc.ClientMethod<$0.ChatHistorySelectorMessage, $0.ChatHistoryMessage>(
      '/chat.ManagementService/GetChatHistory',
      ($0.ChatHistorySelectorMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.ChatHistoryMessage.fromBuffer(value));
  static final _$getMyChatHistory = $grpc.ClientMethod<$0.MyChatHistorySelectorMessage, $0.ChatHistoryMessage>(
      '/chat.ManagementService/GetMyChatHistory',
      ($0.MyChatHistorySelectorMessage value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.ChatHistoryMessage.fromBuffer(value));
  static final _$getActiveTopics = $grpc.ClientMethod<$1.Empty, $0.ActiveTopicsMessage>(
      '/chat.ManagementService/GetActiveTopics',
      ($1.Empty value) => value.writeToBuffer(),
      ($core.List<$core.int> value) => $0.ActiveTopicsMessage.fromBuffer(value));

  ManagementServiceClient($grpc.ClientChannel channel,
      {$grpc.CallOptions? options,
      $core.Iterable<$grpc.ClientInterceptor>? interceptors})
      : super(channel, options: options,
        interceptors: interceptors);

  $grpc.ResponseFuture<$0.ChatHistoryMessage> getChatHistory($0.ChatHistorySelectorMessage request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getChatHistory, request, options: options);
  }

  $grpc.ResponseFuture<$0.ChatHistoryMessage> getMyChatHistory($0.MyChatHistorySelectorMessage request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getMyChatHistory, request, options: options);
  }

  $grpc.ResponseFuture<$0.ActiveTopicsMessage> getActiveTopics($1.Empty request, {$grpc.CallOptions? options}) {
    return $createUnaryCall(_$getActiveTopics, request, options: options);
  }
}

@$pb.GrpcServiceName('chat.ManagementService')
abstract class ManagementServiceBase extends $grpc.Service {
  $core.String get $name => 'chat.ManagementService';

  ManagementServiceBase() {
    $addMethod($grpc.ServiceMethod<$0.ChatHistorySelectorMessage, $0.ChatHistoryMessage>(
        'GetChatHistory',
        getChatHistory_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.ChatHistorySelectorMessage.fromBuffer(value),
        ($0.ChatHistoryMessage value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$0.MyChatHistorySelectorMessage, $0.ChatHistoryMessage>(
        'GetMyChatHistory',
        getMyChatHistory_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $0.MyChatHistorySelectorMessage.fromBuffer(value),
        ($0.ChatHistoryMessage value) => value.writeToBuffer()));
    $addMethod($grpc.ServiceMethod<$1.Empty, $0.ActiveTopicsMessage>(
        'GetActiveTopics',
        getActiveTopics_Pre,
        false,
        false,
        ($core.List<$core.int> value) => $1.Empty.fromBuffer(value),
        ($0.ActiveTopicsMessage value) => value.writeToBuffer()));
  }

  $async.Future<$0.ChatHistoryMessage> getChatHistory_Pre($grpc.ServiceCall call, $async.Future<$0.ChatHistorySelectorMessage> request) async {
    return getChatHistory(call, await request);
  }

  $async.Future<$0.ChatHistoryMessage> getMyChatHistory_Pre($grpc.ServiceCall call, $async.Future<$0.MyChatHistorySelectorMessage> request) async {
    return getMyChatHistory(call, await request);
  }

  $async.Future<$0.ActiveTopicsMessage> getActiveTopics_Pre($grpc.ServiceCall call, $async.Future<$1.Empty> request) async {
    return getActiveTopics(call, await request);
  }

  $async.Future<$0.ChatHistoryMessage> getChatHistory($grpc.ServiceCall call, $0.ChatHistorySelectorMessage request);
  $async.Future<$0.ChatHistoryMessage> getMyChatHistory($grpc.ServiceCall call, $0.MyChatHistorySelectorMessage request);
  $async.Future<$0.ActiveTopicsMessage> getActiveTopics($grpc.ServiceCall call, $1.Empty request);
}
