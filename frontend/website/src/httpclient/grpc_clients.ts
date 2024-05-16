import { ClientReadableStream, Metadata } from "grpc-web";
import { MessagingServiceClient, ManagementServiceClient } from "../proto/ChatServiceClientPb.ts";
import { MyChatHistorySelectorMessage, FromClientMessage, FromServerMessage, Message } from "../proto/chat_pb";
import { Empty } from "google-protobuf/google/protobuf/empty_pb.js"

export class ChatClient {
    private readonly chatClient: MessagingServiceClient;
    private readonly chatManagementClient: ManagementServiceClient;
    private topic: string | null;
    private stream: ClientReadableStream<FromServerMessage> | null;
 
    constructor(){
        this.chatClient = new MessagingServiceClient(process.env.REACT_APP_REGULAR_CHAT_URL ?? "");
        this.chatManagementClient = new ManagementServiceClient(process.env.REACT_APP_REGULAR_CHAT_URL ?? "");
    }

    public async sendMessage(text :string) {
        if (this.topic == null) {
            throw new Error("Unknown topic");
        }

        const request = new FromClientMessage();
        request.setText(text);
        request.setTopicname(this.topic);
        const metadata = this.createMetadata();

        const response = await this.chatClient.sendMessage(request, metadata);

        return response.getAccepted();
    }

    public async subscribeOnMessages(callback : (msg) => void) {
        if (this.stream != null) {
            throw new Error("Already subscribed!");
        }

        const metadata = this.createMetadata();
        const stream = this.chatClient.getChatStream(new Empty(), metadata);
        this.stream = stream;

        stream.on("data", (incommingMessage) => {
            if (incommingMessage.hasMessage()) {
                const message = incommingMessage.getMessage()!;
                const chatMessage = this.toChatMessage(message);

                callback(chatMessage);
            }
            if (incommingMessage.hasTopicinfo()) {
                const topicInfo = incommingMessage.getTopicinfo()!;
                this.topic = topicInfo.getTopicname();
                console.log(this.topic);
            }
        });
        stream.on("error", (err) => {
            console.log(err);
        });
    }

    public cancelSubscription() {
        this.stream?.cancel();
        this.topic = null;
    }

    public async getHistory(offset?: number | null, limit?: number | null) {
        const request = new MyChatHistorySelectorMessage();

        if (limit != null) {
            request.setLimit(limit);
        }
        if (offset != null) {
            request.setOffset(offset);
        }
        const metadata = this.createMetadata();

        try{
            const reply = await this.chatManagementClient.getMyChatHistory(request, metadata);
            return reply.getHistoryList().map(msg => this.toChatMessage(msg));
        }catch(e) {
            console.log(e);
            throw e;
        }
    } 

    private createMetadata() : Metadata {
        return {
            "Authorization": `Bearer ${sessionStorage.getItem("token")}`
        };
    }

    private toChatMessage(message: Message) {
        const senderInfo = message.getAuthor()!;
        const chatMessage = {
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
