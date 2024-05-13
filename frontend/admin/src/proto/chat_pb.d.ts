import * as jspb from 'google-protobuf'

import * as google_protobuf_timestamp_pb from 'google-protobuf/google/protobuf/timestamp_pb';
import * as google_protobuf_wrappers_pb from 'google-protobuf/google/protobuf/wrappers_pb';
import * as google_protobuf_empty_pb from 'google-protobuf/google/protobuf/empty_pb';


export class FromClientMessage extends jspb.Message {
  getTopicname(): string;
  setTopicname(value: string): FromClientMessage;

  getText(): string;
  setText(value: string): FromClientMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FromClientMessage.AsObject;
  static toObject(includeInstance: boolean, msg: FromClientMessage): FromClientMessage.AsObject;
  static serializeBinaryToWriter(message: FromClientMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FromClientMessage;
  static deserializeBinaryFromReader(message: FromClientMessage, reader: jspb.BinaryReader): FromClientMessage;
}

export namespace FromClientMessage {
  export type AsObject = {
    topicname: string,
    text: string,
  }
}

export class MessageAuthor extends jspb.Message {
  getId(): string;
  setId(value: string): MessageAuthor;

  getName(): string;
  setName(value: string): MessageAuthor;

  getIsmanager(): boolean;
  setIsmanager(value: boolean): MessageAuthor;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): MessageAuthor.AsObject;
  static toObject(includeInstance: boolean, msg: MessageAuthor): MessageAuthor.AsObject;
  static serializeBinaryToWriter(message: MessageAuthor, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): MessageAuthor;
  static deserializeBinaryFromReader(message: MessageAuthor, reader: jspb.BinaryReader): MessageAuthor;
}

export namespace MessageAuthor {
  export type AsObject = {
    id: string,
    name: string,
    ismanager: boolean,
  }
}

export class Message extends jspb.Message {
  getId(): string;
  setId(value: string): Message;

  getAuthor(): MessageAuthor | undefined;
  setAuthor(value?: MessageAuthor): Message;
  hasAuthor(): boolean;
  clearAuthor(): Message;

  getText(): string;
  setText(value: string): Message;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Message.AsObject;
  static toObject(includeInstance: boolean, msg: Message): Message.AsObject;
  static serializeBinaryToWriter(message: Message, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Message;
  static deserializeBinaryFromReader(message: Message, reader: jspb.BinaryReader): Message;
}

export namespace Message {
  export type AsObject = {
    id: string,
    author?: MessageAuthor.AsObject,
    text: string,
  }
}

export class TopicInfoMessage extends jspb.Message {
  getTopicname(): string;
  setTopicname(value: string): TopicInfoMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TopicInfoMessage.AsObject;
  static toObject(includeInstance: boolean, msg: TopicInfoMessage): TopicInfoMessage.AsObject;
  static serializeBinaryToWriter(message: TopicInfoMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TopicInfoMessage;
  static deserializeBinaryFromReader(message: TopicInfoMessage, reader: jspb.BinaryReader): TopicInfoMessage;
}

export namespace TopicInfoMessage {
  export type AsObject = {
    topicname: string,
  }
}

export class FromServerMessage extends jspb.Message {
  getTopicinfo(): TopicInfoMessage | undefined;
  setTopicinfo(value?: TopicInfoMessage): FromServerMessage;
  hasTopicinfo(): boolean;
  clearTopicinfo(): FromServerMessage;

  getMessage(): Message | undefined;
  setMessage(value?: Message): FromServerMessage;
  hasMessage(): boolean;
  clearMessage(): FromServerMessage;

  getEventCase(): FromServerMessage.EventCase;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): FromServerMessage.AsObject;
  static toObject(includeInstance: boolean, msg: FromServerMessage): FromServerMessage.AsObject;
  static serializeBinaryToWriter(message: FromServerMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): FromServerMessage;
  static deserializeBinaryFromReader(message: FromServerMessage, reader: jspb.BinaryReader): FromServerMessage;
}

export namespace FromServerMessage {
  export type AsObject = {
    topicinfo?: TopicInfoMessage.AsObject,
    message?: Message.AsObject,
  }

  export enum EventCase { 
    EVENT_NOT_SET = 0,
    TOPICINFO = 1,
    MESSAGE = 2,
  }
}

export class ChatSelectorMessage extends jspb.Message {
  getTopic(): string;
  setTopic(value: string): ChatSelectorMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ChatSelectorMessage.AsObject;
  static toObject(includeInstance: boolean, msg: ChatSelectorMessage): ChatSelectorMessage.AsObject;
  static serializeBinaryToWriter(message: ChatSelectorMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ChatSelectorMessage;
  static deserializeBinaryFromReader(message: ChatSelectorMessage, reader: jspb.BinaryReader): ChatSelectorMessage;
}

export namespace ChatSelectorMessage {
  export type AsObject = {
    topic: string,
  }
}

export class ChatHistorySelectorMessage extends jspb.Message {
  getTopic(): string;
  setTopic(value: string): ChatHistorySelectorMessage;

  getOffset(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setOffset(value?: google_protobuf_wrappers_pb.Int32Value): ChatHistorySelectorMessage;
  hasOffset(): boolean;
  clearOffset(): ChatHistorySelectorMessage;

  getLimit(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setLimit(value?: google_protobuf_wrappers_pb.Int32Value): ChatHistorySelectorMessage;
  hasLimit(): boolean;
  clearLimit(): ChatHistorySelectorMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ChatHistorySelectorMessage.AsObject;
  static toObject(includeInstance: boolean, msg: ChatHistorySelectorMessage): ChatHistorySelectorMessage.AsObject;
  static serializeBinaryToWriter(message: ChatHistorySelectorMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ChatHistorySelectorMessage;
  static deserializeBinaryFromReader(message: ChatHistorySelectorMessage, reader: jspb.BinaryReader): ChatHistorySelectorMessage;
}

export namespace ChatHistorySelectorMessage {
  export type AsObject = {
    topic: string,
    offset?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    limit?: google_protobuf_wrappers_pb.Int32Value.AsObject,
  }
}

export class MyChatHistorySelectorMessage extends jspb.Message {
  getOffset(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setOffset(value?: google_protobuf_wrappers_pb.Int32Value): MyChatHistorySelectorMessage;
  hasOffset(): boolean;
  clearOffset(): MyChatHistorySelectorMessage;

  getLimit(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setLimit(value?: google_protobuf_wrappers_pb.Int32Value): MyChatHistorySelectorMessage;
  hasLimit(): boolean;
  clearLimit(): MyChatHistorySelectorMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): MyChatHistorySelectorMessage.AsObject;
  static toObject(includeInstance: boolean, msg: MyChatHistorySelectorMessage): MyChatHistorySelectorMessage.AsObject;
  static serializeBinaryToWriter(message: MyChatHistorySelectorMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): MyChatHistorySelectorMessage;
  static deserializeBinaryFromReader(message: MyChatHistorySelectorMessage, reader: jspb.BinaryReader): MyChatHistorySelectorMessage;
}

export namespace MyChatHistorySelectorMessage {
  export type AsObject = {
    offset?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    limit?: google_protobuf_wrappers_pb.Int32Value.AsObject,
  }
}

export class ChatHistoryMessage extends jspb.Message {
  getHistoryList(): Array<Message>;
  setHistoryList(value: Array<Message>): ChatHistoryMessage;
  clearHistoryList(): ChatHistoryMessage;
  addHistory(value?: Message, index?: number): Message;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ChatHistoryMessage.AsObject;
  static toObject(includeInstance: boolean, msg: ChatHistoryMessage): ChatHistoryMessage.AsObject;
  static serializeBinaryToWriter(message: ChatHistoryMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ChatHistoryMessage;
  static deserializeBinaryFromReader(message: ChatHistoryMessage, reader: jspb.BinaryReader): ChatHistoryMessage;
}

export namespace ChatHistoryMessage {
  export type AsObject = {
    historyList: Array<Message.AsObject>,
  }
}

export class ActiveTopicsMessage extends jspb.Message {
  getTopicList(): Array<string>;
  setTopicList(value: Array<string>): ActiveTopicsMessage;
  clearTopicList(): ActiveTopicsMessage;
  addTopic(value: string, index?: number): ActiveTopicsMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ActiveTopicsMessage.AsObject;
  static toObject(includeInstance: boolean, msg: ActiveTopicsMessage): ActiveTopicsMessage.AsObject;
  static serializeBinaryToWriter(message: ActiveTopicsMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ActiveTopicsMessage;
  static deserializeBinaryFromReader(message: ActiveTopicsMessage, reader: jspb.BinaryReader): ActiveTopicsMessage;
}

export namespace ActiveTopicsMessage {
  export type AsObject = {
    topicList: Array<string>,
  }
}

export class SendMessageResultMessage extends jspb.Message {
  getAccepted(): boolean;
  setAccepted(value: boolean): SendMessageResultMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SendMessageResultMessage.AsObject;
  static toObject(includeInstance: boolean, msg: SendMessageResultMessage): SendMessageResultMessage.AsObject;
  static serializeBinaryToWriter(message: SendMessageResultMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SendMessageResultMessage;
  static deserializeBinaryFromReader(message: SendMessageResultMessage, reader: jspb.BinaryReader): SendMessageResultMessage;
}

export namespace SendMessageResultMessage {
  export type AsObject = {
    accepted: boolean,
  }
}

