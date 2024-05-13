import { ClientReadableStream, Metadata } from "grpc-web";
import { MessagingServiceClient, ManagementServiceClient } from "../proto/ChatServiceClientPb.ts";
import { ChatSelectorMessage, FromClientMessage, FromServerMessage, Message, ChatHistorySelectorMessage } from "../proto/chat_pb";
import { Empty } from "google-protobuf/google/protobuf/empty_pb.js";

export type SendMessageResult = boolean;

export type MessageAuthor = {
    id: string,
    name: string,
    isManager: Boolean,
}

export type ChatMessage = {
    id: string,
    text: string,
    sender: MessageAuthor,
}

export type Topic = string;

export class ChatClient {
    private readonly chatClient: MessagingServiceClient;
    private readonly chatManagementClient: ManagementServiceClient;

    constructor(){
        this.chatClient = new MessagingServiceClient(process.env.REACT_APP_REGULAR_CHAT_URL ?? "");
        this.chatManagementClient = new ManagementServiceClient(process.env.REACT_APP_REGULAR_CHAT_URL ?? "");
    }

    public async sendMessage(topic :string, text :string) : Promise<SendMessageResult> {
        const request = new FromClientMessage();
        request.setText(text);
        request.setTopicname(topic);
        const metadata = this.createMetadata();

        const response = await this.chatClient.sendMessage(request, metadata);

        return response.getAccepted();
    }

    public async subscribeOnMessages(topic :string, callback : (msg: ChatMessage) => void) : Promise<SubscribtionCancelationTokenSource> {
        const request = new ChatSelectorMessage();
        request.setTopic(topic);
        const metadata = this.createMetadata();

        const stream = this.chatClient.getChatStreamByTopic(request, metadata);
        stream.on("data", (incommingMessage) => {
            if (incommingMessage.hasMessage()) {
                const message = incommingMessage.getMessage()!;
                const chatMessage: ChatMessage = this.toChatMessage(message);

                callback(chatMessage);
            }
        });

        return new SubscribtionCancelationTokenSource(stream);
    }

    public async getHistory(topic :string, offset?: number | null, limit?: number | null): Promise<ChatMessage[]> {
        const request = new ChatHistorySelectorMessage();
        request.setTopic(topic);
        if (limit != null) {
            request.setLimit(limit);
        }
        if (offset != null) {
            request.setOffset(offset);
        }
        const metadata = this.createMetadata();

        const reply = await this.chatManagementClient.getChatHistory(request, metadata);

        return reply.getHistoryList().map(msg => this.toChatMessage(msg));
    } 

    public async getActiveRooms() : Promise<Topic[]> {
        const topics = await this.chatManagementClient.getActiveTopics(new Empty(), this.createMetadata());
        
        return topics.getTopicList();
    }

    private createMetadata() : Metadata {
        return {
            "authorization": `Bearer ${localStorage.getItem("token")}`
        };
    }

    private toChatMessage(message: Message) : ChatMessage {
        const senderInfo = message.getAuthor()!;
        const chatMessage :ChatMessage = {
            id: message.getId(),
            text: message.getText(),
            sender: {
                id: senderInfo.getId(),
                name: senderInfo.getName(),
                isManager: senderInfo.getIsmanager(),
            },
        };

        return chatMessage;
    }
}

class SubscribtionCancelationTokenSource {
    private messageStream: ClientReadableStream<FromServerMessage>;

    constructor(subscription :ClientReadableStream<FromServerMessage>){
        this.messageStream = subscription;
    }

    cancelSubscribtion() {
        this.messageStream.cancel();
    }
}
