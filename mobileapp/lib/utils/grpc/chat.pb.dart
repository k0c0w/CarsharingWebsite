//
//  Generated code. Do not modify.
//  source: chat.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:core' as $core;

import 'package:protobuf/protobuf.dart' as $pb;

import 'package:mobileapp/utils/grpc/google/timestamp.pb.dart' as $2;
import 'package:mobileapp/utils/grpc/google/wrappers.pb.dart' as $3;

class FromClientMessage extends $pb.GeneratedMessage {
  factory FromClientMessage({
    $core.String? topicName,
    $core.String? text,
  }) {
    final $result = create();
    if (topicName != null) {
      $result.topicName = topicName;
    }
    if (text != null) {
      $result.text = text;
    }
    return $result;
  }
  FromClientMessage._() : super();
  factory FromClientMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory FromClientMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'FromClientMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'topicName', protoName: 'topicName')
    ..aOS(2, _omitFieldNames ? '' : 'text')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  FromClientMessage clone() => FromClientMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  FromClientMessage copyWith(void Function(FromClientMessage) updates) => super.copyWith((message) => updates(message as FromClientMessage)) as FromClientMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static FromClientMessage create() => FromClientMessage._();
  FromClientMessage createEmptyInstance() => create();
  static $pb.PbList<FromClientMessage> createRepeated() => $pb.PbList<FromClientMessage>();
  @$core.pragma('dart2js:noInline')
  static FromClientMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<FromClientMessage>(create);
  static FromClientMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get topicName => $_getSZ(0);
  @$pb.TagNumber(1)
  set topicName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTopicName() => $_has(0);
  @$pb.TagNumber(1)
  void clearTopicName() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get text => $_getSZ(1);
  @$pb.TagNumber(2)
  set text($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasText() => $_has(1);
  @$pb.TagNumber(2)
  void clearText() => clearField(2);
}

class MessageAuthor extends $pb.GeneratedMessage {
  factory MessageAuthor({
    $core.String? id,
    $core.String? name,
    $core.bool? isManager,
  }) {
    final $result = create();
    if (id != null) {
      $result.id = id;
    }
    if (name != null) {
      $result.name = name;
    }
    if (isManager != null) {
      $result.isManager = isManager;
    }
    return $result;
  }
  MessageAuthor._() : super();
  factory MessageAuthor.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory MessageAuthor.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'MessageAuthor', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'id')
    ..aOS(2, _omitFieldNames ? '' : 'name')
    ..aOB(3, _omitFieldNames ? '' : 'isManager', protoName: 'isManager')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  MessageAuthor clone() => MessageAuthor()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  MessageAuthor copyWith(void Function(MessageAuthor) updates) => super.copyWith((message) => updates(message as MessageAuthor)) as MessageAuthor;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static MessageAuthor create() => MessageAuthor._();
  MessageAuthor createEmptyInstance() => create();
  static $pb.PbList<MessageAuthor> createRepeated() => $pb.PbList<MessageAuthor>();
  @$core.pragma('dart2js:noInline')
  static MessageAuthor getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<MessageAuthor>(create);
  static MessageAuthor? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  $core.String get name => $_getSZ(1);
  @$pb.TagNumber(2)
  set name($core.String v) { $_setString(1, v); }
  @$pb.TagNumber(2)
  $core.bool hasName() => $_has(1);
  @$pb.TagNumber(2)
  void clearName() => clearField(2);

  @$pb.TagNumber(3)
  $core.bool get isManager => $_getBF(2);
  @$pb.TagNumber(3)
  set isManager($core.bool v) { $_setBool(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasIsManager() => $_has(2);
  @$pb.TagNumber(3)
  void clearIsManager() => clearField(3);
}

class Message extends $pb.GeneratedMessage {
  factory Message({
    $core.String? id,
    MessageAuthor? author,
    $core.String? text,
    $2.Timestamp? time,
  }) {
    final $result = create();
    if (id != null) {
      $result.id = id;
    }
    if (author != null) {
      $result.author = author;
    }
    if (text != null) {
      $result.text = text;
    }
    if (time != null) {
      $result.time = time;
    }
    return $result;
  }
  Message._() : super();
  factory Message.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory Message.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'Message', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'id')
    ..aOM<MessageAuthor>(2, _omitFieldNames ? '' : 'author', subBuilder: MessageAuthor.create)
    ..aOS(3, _omitFieldNames ? '' : 'text')
    ..aOM<$2.Timestamp>(4, _omitFieldNames ? '' : 'time', subBuilder: $2.Timestamp.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  Message clone() => Message()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  Message copyWith(void Function(Message) updates) => super.copyWith((message) => updates(message as Message)) as Message;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static Message create() => Message._();
  Message createEmptyInstance() => create();
  static $pb.PbList<Message> createRepeated() => $pb.PbList<Message>();
  @$core.pragma('dart2js:noInline')
  static Message getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<Message>(create);
  static Message? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get id => $_getSZ(0);
  @$pb.TagNumber(1)
  set id($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasId() => $_has(0);
  @$pb.TagNumber(1)
  void clearId() => clearField(1);

  @$pb.TagNumber(2)
  MessageAuthor get author => $_getN(1);
  @$pb.TagNumber(2)
  set author(MessageAuthor v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasAuthor() => $_has(1);
  @$pb.TagNumber(2)
  void clearAuthor() => clearField(2);
  @$pb.TagNumber(2)
  MessageAuthor ensureAuthor() => $_ensure(1);

  @$pb.TagNumber(3)
  $core.String get text => $_getSZ(2);
  @$pb.TagNumber(3)
  set text($core.String v) { $_setString(2, v); }
  @$pb.TagNumber(3)
  $core.bool hasText() => $_has(2);
  @$pb.TagNumber(3)
  void clearText() => clearField(3);

  @$pb.TagNumber(4)
  $2.Timestamp get time => $_getN(3);
  @$pb.TagNumber(4)
  set time($2.Timestamp v) { setField(4, v); }
  @$pb.TagNumber(4)
  $core.bool hasTime() => $_has(3);
  @$pb.TagNumber(4)
  void clearTime() => clearField(4);
  @$pb.TagNumber(4)
  $2.Timestamp ensureTime() => $_ensure(3);
}

class TopicInfoMessage extends $pb.GeneratedMessage {
  factory TopicInfoMessage({
    $core.String? topicName,
  }) {
    final $result = create();
    if (topicName != null) {
      $result.topicName = topicName;
    }
    return $result;
  }
  TopicInfoMessage._() : super();
  factory TopicInfoMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory TopicInfoMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'TopicInfoMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'topicName', protoName: 'topicName')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  TopicInfoMessage clone() => TopicInfoMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  TopicInfoMessage copyWith(void Function(TopicInfoMessage) updates) => super.copyWith((message) => updates(message as TopicInfoMessage)) as TopicInfoMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static TopicInfoMessage create() => TopicInfoMessage._();
  TopicInfoMessage createEmptyInstance() => create();
  static $pb.PbList<TopicInfoMessage> createRepeated() => $pb.PbList<TopicInfoMessage>();
  @$core.pragma('dart2js:noInline')
  static TopicInfoMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<TopicInfoMessage>(create);
  static TopicInfoMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get topicName => $_getSZ(0);
  @$pb.TagNumber(1)
  set topicName($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTopicName() => $_has(0);
  @$pb.TagNumber(1)
  void clearTopicName() => clearField(1);
}

enum FromServerMessage_Event {
  topicInfo, 
  message, 
  notSet
}

class FromServerMessage extends $pb.GeneratedMessage {
  factory FromServerMessage({
    TopicInfoMessage? topicInfo,
    Message? message,
  }) {
    final $result = create();
    if (topicInfo != null) {
      $result.topicInfo = topicInfo;
    }
    if (message != null) {
      $result.message = message;
    }
    return $result;
  }
  FromServerMessage._() : super();
  factory FromServerMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory FromServerMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static const $core.Map<$core.int, FromServerMessage_Event> _FromServerMessage_EventByTag = {
    1 : FromServerMessage_Event.topicInfo,
    2 : FromServerMessage_Event.message,
    0 : FromServerMessage_Event.notSet
  };
  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'FromServerMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..oo(0, [1, 2])
    ..aOM<TopicInfoMessage>(1, _omitFieldNames ? '' : 'topicInfo', protoName: 'topicInfo', subBuilder: TopicInfoMessage.create)
    ..aOM<Message>(2, _omitFieldNames ? '' : 'message', subBuilder: Message.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  FromServerMessage clone() => FromServerMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  FromServerMessage copyWith(void Function(FromServerMessage) updates) => super.copyWith((message) => updates(message as FromServerMessage)) as FromServerMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static FromServerMessage create() => FromServerMessage._();
  FromServerMessage createEmptyInstance() => create();
  static $pb.PbList<FromServerMessage> createRepeated() => $pb.PbList<FromServerMessage>();
  @$core.pragma('dart2js:noInline')
  static FromServerMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<FromServerMessage>(create);
  static FromServerMessage? _defaultInstance;

  FromServerMessage_Event whichEvent() => _FromServerMessage_EventByTag[$_whichOneof(0)]!;
  void clearEvent() => clearField($_whichOneof(0));

  @$pb.TagNumber(1)
  TopicInfoMessage get topicInfo => $_getN(0);
  @$pb.TagNumber(1)
  set topicInfo(TopicInfoMessage v) { setField(1, v); }
  @$pb.TagNumber(1)
  $core.bool hasTopicInfo() => $_has(0);
  @$pb.TagNumber(1)
  void clearTopicInfo() => clearField(1);
  @$pb.TagNumber(1)
  TopicInfoMessage ensureTopicInfo() => $_ensure(0);

  @$pb.TagNumber(2)
  Message get message => $_getN(1);
  @$pb.TagNumber(2)
  set message(Message v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasMessage() => $_has(1);
  @$pb.TagNumber(2)
  void clearMessage() => clearField(2);
  @$pb.TagNumber(2)
  Message ensureMessage() => $_ensure(1);
}

class ChatSelectorMessage extends $pb.GeneratedMessage {
  factory ChatSelectorMessage({
    $core.String? topic,
  }) {
    final $result = create();
    if (topic != null) {
      $result.topic = topic;
    }
    return $result;
  }
  ChatSelectorMessage._() : super();
  factory ChatSelectorMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ChatSelectorMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ChatSelectorMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'topic')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ChatSelectorMessage clone() => ChatSelectorMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ChatSelectorMessage copyWith(void Function(ChatSelectorMessage) updates) => super.copyWith((message) => updates(message as ChatSelectorMessage)) as ChatSelectorMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ChatSelectorMessage create() => ChatSelectorMessage._();
  ChatSelectorMessage createEmptyInstance() => create();
  static $pb.PbList<ChatSelectorMessage> createRepeated() => $pb.PbList<ChatSelectorMessage>();
  @$core.pragma('dart2js:noInline')
  static ChatSelectorMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ChatSelectorMessage>(create);
  static ChatSelectorMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get topic => $_getSZ(0);
  @$pb.TagNumber(1)
  set topic($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTopic() => $_has(0);
  @$pb.TagNumber(1)
  void clearTopic() => clearField(1);
}

class ChatHistorySelectorMessage extends $pb.GeneratedMessage {
  factory ChatHistorySelectorMessage({
    $core.String? topic,
    $3.Int32Value? offset,
    $3.Int32Value? limit,
  }) {
    final $result = create();
    if (topic != null) {
      $result.topic = topic;
    }
    if (offset != null) {
      $result.offset = offset;
    }
    if (limit != null) {
      $result.limit = limit;
    }
    return $result;
  }
  ChatHistorySelectorMessage._() : super();
  factory ChatHistorySelectorMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ChatHistorySelectorMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ChatHistorySelectorMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOS(1, _omitFieldNames ? '' : 'topic')
    ..aOM<$3.Int32Value>(2, _omitFieldNames ? '' : 'offset', subBuilder: $3.Int32Value.create)
    ..aOM<$3.Int32Value>(3, _omitFieldNames ? '' : 'limit', subBuilder: $3.Int32Value.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ChatHistorySelectorMessage clone() => ChatHistorySelectorMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ChatHistorySelectorMessage copyWith(void Function(ChatHistorySelectorMessage) updates) => super.copyWith((message) => updates(message as ChatHistorySelectorMessage)) as ChatHistorySelectorMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ChatHistorySelectorMessage create() => ChatHistorySelectorMessage._();
  ChatHistorySelectorMessage createEmptyInstance() => create();
  static $pb.PbList<ChatHistorySelectorMessage> createRepeated() => $pb.PbList<ChatHistorySelectorMessage>();
  @$core.pragma('dart2js:noInline')
  static ChatHistorySelectorMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ChatHistorySelectorMessage>(create);
  static ChatHistorySelectorMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.String get topic => $_getSZ(0);
  @$pb.TagNumber(1)
  set topic($core.String v) { $_setString(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasTopic() => $_has(0);
  @$pb.TagNumber(1)
  void clearTopic() => clearField(1);

  @$pb.TagNumber(2)
  $3.Int32Value get offset => $_getN(1);
  @$pb.TagNumber(2)
  set offset($3.Int32Value v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasOffset() => $_has(1);
  @$pb.TagNumber(2)
  void clearOffset() => clearField(2);
  @$pb.TagNumber(2)
  $3.Int32Value ensureOffset() => $_ensure(1);

  @$pb.TagNumber(3)
  $3.Int32Value get limit => $_getN(2);
  @$pb.TagNumber(3)
  set limit($3.Int32Value v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasLimit() => $_has(2);
  @$pb.TagNumber(3)
  void clearLimit() => clearField(3);
  @$pb.TagNumber(3)
  $3.Int32Value ensureLimit() => $_ensure(2);
}

class MyChatHistorySelectorMessage extends $pb.GeneratedMessage {
  factory MyChatHistorySelectorMessage({
    $3.Int32Value? offset,
    $3.Int32Value? limit,
  }) {
    final $result = create();
    if (offset != null) {
      $result.offset = offset;
    }
    if (limit != null) {
      $result.limit = limit;
    }
    return $result;
  }
  MyChatHistorySelectorMessage._() : super();
  factory MyChatHistorySelectorMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory MyChatHistorySelectorMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'MyChatHistorySelectorMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOM<$3.Int32Value>(2, _omitFieldNames ? '' : 'offset', subBuilder: $3.Int32Value.create)
    ..aOM<$3.Int32Value>(3, _omitFieldNames ? '' : 'limit', subBuilder: $3.Int32Value.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  MyChatHistorySelectorMessage clone() => MyChatHistorySelectorMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  MyChatHistorySelectorMessage copyWith(void Function(MyChatHistorySelectorMessage) updates) => super.copyWith((message) => updates(message as MyChatHistorySelectorMessage)) as MyChatHistorySelectorMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static MyChatHistorySelectorMessage create() => MyChatHistorySelectorMessage._();
  MyChatHistorySelectorMessage createEmptyInstance() => create();
  static $pb.PbList<MyChatHistorySelectorMessage> createRepeated() => $pb.PbList<MyChatHistorySelectorMessage>();
  @$core.pragma('dart2js:noInline')
  static MyChatHistorySelectorMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<MyChatHistorySelectorMessage>(create);
  static MyChatHistorySelectorMessage? _defaultInstance;

  @$pb.TagNumber(2)
  $3.Int32Value get offset => $_getN(0);
  @$pb.TagNumber(2)
  set offset($3.Int32Value v) { setField(2, v); }
  @$pb.TagNumber(2)
  $core.bool hasOffset() => $_has(0);
  @$pb.TagNumber(2)
  void clearOffset() => clearField(2);
  @$pb.TagNumber(2)
  $3.Int32Value ensureOffset() => $_ensure(0);

  @$pb.TagNumber(3)
  $3.Int32Value get limit => $_getN(1);
  @$pb.TagNumber(3)
  set limit($3.Int32Value v) { setField(3, v); }
  @$pb.TagNumber(3)
  $core.bool hasLimit() => $_has(1);
  @$pb.TagNumber(3)
  void clearLimit() => clearField(3);
  @$pb.TagNumber(3)
  $3.Int32Value ensureLimit() => $_ensure(1);
}

class ChatHistoryMessage extends $pb.GeneratedMessage {
  factory ChatHistoryMessage({
    $core.Iterable<Message>? history,
  }) {
    final $result = create();
    if (history != null) {
      $result.history.addAll(history);
    }
    return $result;
  }
  ChatHistoryMessage._() : super();
  factory ChatHistoryMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ChatHistoryMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ChatHistoryMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..pc<Message>(1, _omitFieldNames ? '' : 'history', $pb.PbFieldType.PM, subBuilder: Message.create)
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ChatHistoryMessage clone() => ChatHistoryMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ChatHistoryMessage copyWith(void Function(ChatHistoryMessage) updates) => super.copyWith((message) => updates(message as ChatHistoryMessage)) as ChatHistoryMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ChatHistoryMessage create() => ChatHistoryMessage._();
  ChatHistoryMessage createEmptyInstance() => create();
  static $pb.PbList<ChatHistoryMessage> createRepeated() => $pb.PbList<ChatHistoryMessage>();
  @$core.pragma('dart2js:noInline')
  static ChatHistoryMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ChatHistoryMessage>(create);
  static ChatHistoryMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<Message> get history => $_getList(0);
}

class ActiveTopicsMessage extends $pb.GeneratedMessage {
  factory ActiveTopicsMessage({
    $core.Iterable<$core.String>? topic,
  }) {
    final $result = create();
    if (topic != null) {
      $result.topic.addAll(topic);
    }
    return $result;
  }
  ActiveTopicsMessage._() : super();
  factory ActiveTopicsMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory ActiveTopicsMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'ActiveTopicsMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..pPS(1, _omitFieldNames ? '' : 'topic')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  ActiveTopicsMessage clone() => ActiveTopicsMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  ActiveTopicsMessage copyWith(void Function(ActiveTopicsMessage) updates) => super.copyWith((message) => updates(message as ActiveTopicsMessage)) as ActiveTopicsMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static ActiveTopicsMessage create() => ActiveTopicsMessage._();
  ActiveTopicsMessage createEmptyInstance() => create();
  static $pb.PbList<ActiveTopicsMessage> createRepeated() => $pb.PbList<ActiveTopicsMessage>();
  @$core.pragma('dart2js:noInline')
  static ActiveTopicsMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<ActiveTopicsMessage>(create);
  static ActiveTopicsMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.List<$core.String> get topic => $_getList(0);
}

class SendMessageResultMessage extends $pb.GeneratedMessage {
  factory SendMessageResultMessage({
    $core.bool? accepted,
  }) {
    final $result = create();
    if (accepted != null) {
      $result.accepted = accepted;
    }
    return $result;
  }
  SendMessageResultMessage._() : super();
  factory SendMessageResultMessage.fromBuffer($core.List<$core.int> i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromBuffer(i, r);
  factory SendMessageResultMessage.fromJson($core.String i, [$pb.ExtensionRegistry r = $pb.ExtensionRegistry.EMPTY]) => create()..mergeFromJson(i, r);

  static final $pb.BuilderInfo _i = $pb.BuilderInfo(_omitMessageNames ? '' : 'SendMessageResultMessage', package: const $pb.PackageName(_omitMessageNames ? '' : 'chat'), createEmptyInstance: create)
    ..aOB(1, _omitFieldNames ? '' : 'accepted')
    ..hasRequiredFields = false
  ;

  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.deepCopy] instead. '
  'Will be removed in next major version')
  SendMessageResultMessage clone() => SendMessageResultMessage()..mergeFromMessage(this);
  @$core.Deprecated(
  'Using this can add significant overhead to your binary. '
  'Use [GeneratedMessageGenericExtensions.rebuild] instead. '
  'Will be removed in next major version')
  SendMessageResultMessage copyWith(void Function(SendMessageResultMessage) updates) => super.copyWith((message) => updates(message as SendMessageResultMessage)) as SendMessageResultMessage;

  $pb.BuilderInfo get info_ => _i;

  @$core.pragma('dart2js:noInline')
  static SendMessageResultMessage create() => SendMessageResultMessage._();
  SendMessageResultMessage createEmptyInstance() => create();
  static $pb.PbList<SendMessageResultMessage> createRepeated() => $pb.PbList<SendMessageResultMessage>();
  @$core.pragma('dart2js:noInline')
  static SendMessageResultMessage getDefault() => _defaultInstance ??= $pb.GeneratedMessage.$_defaultFor<SendMessageResultMessage>(create);
  static SendMessageResultMessage? _defaultInstance;

  @$pb.TagNumber(1)
  $core.bool get accepted => $_getBF(0);
  @$pb.TagNumber(1)
  set accepted($core.bool v) { $_setBool(0, v); }
  @$pb.TagNumber(1)
  $core.bool hasAccepted() => $_has(0);
  @$pb.TagNumber(1)
  void clearAccepted() => clearField(1);
}


const _omitFieldNames = $core.bool.fromEnvironment('protobuf.omit_field_names');
const _omitMessageNames = $core.bool.fromEnvironment('protobuf.omit_message_names');
