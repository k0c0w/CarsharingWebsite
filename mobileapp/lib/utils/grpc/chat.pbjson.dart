//
//  Generated code. Do not modify.
//  source: backend/Chat.Microservice/protos/chat.proto
//
// @dart = 2.12

// ignore_for_file: annotate_overrides, camel_case_types, comment_references
// ignore_for_file: constant_identifier_names, library_prefixes
// ignore_for_file: non_constant_identifier_names, prefer_final_fields
// ignore_for_file: unnecessary_import, unnecessary_this, unused_import

import 'dart:convert' as $convert;
import 'dart:core' as $core;
import 'dart:typed_data' as $typed_data;

@$core.Deprecated('Use fromClientMessageDescriptor instead')
const FromClientMessage$json = {
  '1': 'FromClientMessage',
  '2': [
    {'1': 'topicName', '3': 1, '4': 1, '5': 9, '10': 'topicName'},
    {'1': 'text', '3': 2, '4': 1, '5': 9, '10': 'text'},
  ],
};

/// Descriptor for `FromClientMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List fromClientMessageDescriptor = $convert.base64Decode(
    'ChFGcm9tQ2xpZW50TWVzc2FnZRIcCgl0b3BpY05hbWUYASABKAlSCXRvcGljTmFtZRISCgR0ZX'
    'h0GAIgASgJUgR0ZXh0');

@$core.Deprecated('Use messageAuthorDescriptor instead')
const MessageAuthor$json = {
  '1': 'MessageAuthor',
  '2': [
    {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    {'1': 'name', '3': 2, '4': 1, '5': 9, '10': 'name'},
    {'1': 'isManager', '3': 3, '4': 1, '5': 8, '10': 'isManager'},
  ],
};

/// Descriptor for `MessageAuthor`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List messageAuthorDescriptor = $convert.base64Decode(
    'Cg1NZXNzYWdlQXV0aG9yEg4KAmlkGAEgASgJUgJpZBISCgRuYW1lGAIgASgJUgRuYW1lEhwKCW'
    'lzTWFuYWdlchgDIAEoCFIJaXNNYW5hZ2Vy');

@$core.Deprecated('Use messageDescriptor instead')
const Message$json = {
  '1': 'Message',
  '2': [
    {'1': 'id', '3': 1, '4': 1, '5': 9, '10': 'id'},
    {'1': 'author', '3': 2, '4': 1, '5': 11, '6': '.chat.MessageAuthor', '10': 'author'},
    {'1': 'text', '3': 3, '4': 1, '5': 9, '10': 'text'},
  ],
};

/// Descriptor for `Message`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List messageDescriptor = $convert.base64Decode(
    'CgdNZXNzYWdlEg4KAmlkGAEgASgJUgJpZBIrCgZhdXRob3IYAiABKAsyEy5jaGF0Lk1lc3NhZ2'
    'VBdXRob3JSBmF1dGhvchISCgR0ZXh0GAMgASgJUgR0ZXh0');

@$core.Deprecated('Use topicInfoMessageDescriptor instead')
const TopicInfoMessage$json = {
  '1': 'TopicInfoMessage',
  '2': [
    {'1': 'topicName', '3': 1, '4': 1, '5': 9, '10': 'topicName'},
  ],
};

/// Descriptor for `TopicInfoMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List topicInfoMessageDescriptor = $convert.base64Decode(
    'ChBUb3BpY0luZm9NZXNzYWdlEhwKCXRvcGljTmFtZRgBIAEoCVIJdG9waWNOYW1l');

@$core.Deprecated('Use fromServerMessageDescriptor instead')
const FromServerMessage$json = {
  '1': 'FromServerMessage',
  '2': [
    {'1': 'topicInfo', '3': 1, '4': 1, '5': 11, '6': '.chat.TopicInfoMessage', '9': 0, '10': 'topicInfo'},
    {'1': 'message', '3': 2, '4': 1, '5': 11, '6': '.chat.Message', '9': 0, '10': 'message'},
  ],
  '8': [
    {'1': 'event'},
  ],
};

/// Descriptor for `FromServerMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List fromServerMessageDescriptor = $convert.base64Decode(
    'ChFGcm9tU2VydmVyTWVzc2FnZRI2Cgl0b3BpY0luZm8YASABKAsyFi5jaGF0LlRvcGljSW5mb0'
    '1lc3NhZ2VIAFIJdG9waWNJbmZvEikKB21lc3NhZ2UYAiABKAsyDS5jaGF0Lk1lc3NhZ2VIAFIH'
    'bWVzc2FnZUIHCgVldmVudA==');

@$core.Deprecated('Use chatSelectorMessageDescriptor instead')
const ChatSelectorMessage$json = {
  '1': 'ChatSelectorMessage',
  '2': [
    {'1': 'topic', '3': 1, '4': 1, '5': 9, '10': 'topic'},
  ],
};

/// Descriptor for `ChatSelectorMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List chatSelectorMessageDescriptor = $convert.base64Decode(
    'ChNDaGF0U2VsZWN0b3JNZXNzYWdlEhQKBXRvcGljGAEgASgJUgV0b3BpYw==');

@$core.Deprecated('Use chatHistorySelectorMessageDescriptor instead')
const ChatHistorySelectorMessage$json = {
  '1': 'ChatHistorySelectorMessage',
  '2': [
    {'1': 'topic', '3': 1, '4': 1, '5': 9, '10': 'topic'},
    {'1': 'offset', '3': 2, '4': 1, '5': 11, '6': '.google.protobuf.Int32Value', '10': 'offset'},
    {'1': 'limit', '3': 3, '4': 1, '5': 11, '6': '.google.protobuf.Int32Value', '10': 'limit'},
  ],
};

/// Descriptor for `ChatHistorySelectorMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List chatHistorySelectorMessageDescriptor = $convert.base64Decode(
    'ChpDaGF0SGlzdG9yeVNlbGVjdG9yTWVzc2FnZRIUCgV0b3BpYxgBIAEoCVIFdG9waWMSMwoGb2'
    'Zmc2V0GAIgASgLMhsuZ29vZ2xlLnByb3RvYnVmLkludDMyVmFsdWVSBm9mZnNldBIxCgVsaW1p'
    'dBgDIAEoCzIbLmdvb2dsZS5wcm90b2J1Zi5JbnQzMlZhbHVlUgVsaW1pdA==');

@$core.Deprecated('Use myChatHistorySelectorMessageDescriptor instead')
const MyChatHistorySelectorMessage$json = {
  '1': 'MyChatHistorySelectorMessage',
  '2': [
    {'1': 'offset', '3': 2, '4': 1, '5': 11, '6': '.google.protobuf.Int32Value', '10': 'offset'},
    {'1': 'limit', '3': 3, '4': 1, '5': 11, '6': '.google.protobuf.Int32Value', '10': 'limit'},
  ],
};

/// Descriptor for `MyChatHistorySelectorMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List myChatHistorySelectorMessageDescriptor = $convert.base64Decode(
    'ChxNeUNoYXRIaXN0b3J5U2VsZWN0b3JNZXNzYWdlEjMKBm9mZnNldBgCIAEoCzIbLmdvb2dsZS'
    '5wcm90b2J1Zi5JbnQzMlZhbHVlUgZvZmZzZXQSMQoFbGltaXQYAyABKAsyGy5nb29nbGUucHJv'
    'dG9idWYuSW50MzJWYWx1ZVIFbGltaXQ=');

@$core.Deprecated('Use chatHistoryMessageDescriptor instead')
const ChatHistoryMessage$json = {
  '1': 'ChatHistoryMessage',
  '2': [
    {'1': 'history', '3': 1, '4': 3, '5': 11, '6': '.chat.Message', '10': 'history'},
  ],
};

/// Descriptor for `ChatHistoryMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List chatHistoryMessageDescriptor = $convert.base64Decode(
    'ChJDaGF0SGlzdG9yeU1lc3NhZ2USJwoHaGlzdG9yeRgBIAMoCzINLmNoYXQuTWVzc2FnZVIHaG'
    'lzdG9yeQ==');

@$core.Deprecated('Use activeTopicsMessageDescriptor instead')
const ActiveTopicsMessage$json = {
  '1': 'ActiveTopicsMessage',
  '2': [
    {'1': 'topic', '3': 1, '4': 3, '5': 9, '10': 'topic'},
  ],
};

/// Descriptor for `ActiveTopicsMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List activeTopicsMessageDescriptor = $convert.base64Decode(
    'ChNBY3RpdmVUb3BpY3NNZXNzYWdlEhQKBXRvcGljGAEgAygJUgV0b3BpYw==');

@$core.Deprecated('Use sendMessageResultMessageDescriptor instead')
const SendMessageResultMessage$json = {
  '1': 'SendMessageResultMessage',
  '2': [
    {'1': 'accepted', '3': 1, '4': 1, '5': 8, '10': 'accepted'},
  ],
};

/// Descriptor for `SendMessageResultMessage`. Decode as a `google.protobuf.DescriptorProto`.
final $typed_data.Uint8List sendMessageResultMessageDescriptor = $convert.base64Decode(
    'ChhTZW5kTWVzc2FnZVJlc3VsdE1lc3NhZ2USGgoIYWNjZXB0ZWQYASABKAhSCGFjY2VwdGVk');

