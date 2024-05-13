import React, { useState, useEffect } from "react";
import '../styles/chat.css';
import { Button } from '@mui/material';
import MessageContainer, {OccasionMessageContainer} from "../components/Chat/MessageContainer";
import  SendMessageForm, {OccasionSendMessageForm} from "../components/Chat/SendMessageForm";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { ChatClient } from "../httpclient/grpc_clients.ts";
import API from "../httpclient/axios_client";

export default function Chat ({topic, leaveRoom}) {
    const [client] = useState(() => new ChatClient(topic));
    const [messages, setMessages] = useState([]);

    const onMessageReceived = (message) => setMessages(old => [...old, message]);

    async function sendMessage(text) {
      await client.sendMessage(topic, text.trim());
    }

    useEffect(() => {
      let subscription;

      (async () => {
        const history = await client.getHistory(topic);
        setMessages(history);

        subscription = await client.subscribeOnMessages(topic, onMessageReceived);
      })();

      return () => subscription?.cancelSubscribtion();
    }, []);

    return (
      <div className='app'>
        <div className='leave-room'>
          <Button variant='danger' onClick={leaveRoom}>
            Leave Room
          </Button>
        </div>
  
        <div className='chat' >
          <MessageContainer messages={messages} />
          <SendMessageForm sendMessage={sendMessage} />
        </div>
      </div>
    )
}

export function OccasionChat({occasionId, onLeaveRoom, setErrorMessage}) {
  const [connection, setConnection] = useState(null);
  const [occasionIssuerId, setOccasionIssuerId] = useState(null);
  const [messages, setMessages] = useState([]);

  function onCloseOccasionRecieved() {
    setOccasionIssuerId(null);
    connection?.stop();
    setConnection(null);
    setErrorMessage("Occasion was closed");
  }

  async function loadHistory() {
    const occasion = await API.getOccasionById(occasionId);
    if (!occasion.successed){
      setErrorMessage("Ошибка при открытии диалога.");
      onLeaveRoom();
      return;
    }
    setOccasionIssuerId(occasion.occasion.issuerId);
    const occasionHistory = await API.loadOccasionHistory(occasionId);
    if (occasionHistory.successed)
      setMessages(occasionHistory.messages);
  }

  async function processMessage(receivedMessage) {
    const message = {
        text: receivedMessage.text,
        authorName: receivedMessage.authorName,
        attachments: [],
        isFromManager: receivedMessage.isFromManager
    }
    if (receivedMessage.attachmentId) {
        const attachmentInfo = await API.getAttachmentInfo(receivedMessage.attachmentId);
        if (attachmentInfo.successed){
            message.attachments = attachmentInfo.attachments;

        }
        else{
            message.attachments = [{download_url:attachmentInfo.defaultAttachment, content_type:"image/jpg", file_name:"not foung.jpg"}];
        }
    }

    setMessages(messages => [...messages, message])
  }  

  async function startConnection() {
    try {
        const localConnection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_OCCASION_CHAT_URL, { accessTokenFactory: () => localStorage.getItem("token") })
            .configureLogging(LogLevel.Information)
            .build();

        localConnection.on('ReceiveMessage',
         (message) => {
            console.log(message);
            processMessage(message);
        });
        localConnection.on('OccassionClosed', 
        () => {onCloseOccasionRecieved()});
        
        localConnection.onclose(() => {
            onLeaveRoom();
        });

        await localConnection.start();
        setConnection(localConnection);
    } catch (e) {
        console.log(e);
    }
  }

  const leaveRoom = async () => {
      try {
          connection?.stop();
          onLeaveRoom();
      } catch (e) {
          console.log(e)
      }
  }

  const sendMessage = async ({text, attachmentId}) => {
    try {
        const messageModel = {
            Text: text,
            Time: new Date().toJSON(),
            OccasionId: occasionId,
            AttachmentId: attachmentId
        };
      
        await connection.invoke('SendMessage', messageModel);
    } catch (e) {
        console.log(e)
    }
  }

  useEffect( () => {
    async function init() {
      await loadHistory();
      await startConnection();
      //await connection.invoke('JoinRoom', occasionId);
    }

    init();
    return () => connection?.stop();
  }, []);
  

  return  <div className='app'>
          <div className='leave-room'>
            <Button variant='danger' onClick={leaveRoom}>
              Leave Room
            </Button>
          </div>
  
          <div className='chat' >
            <OccasionMessageContainer messages={messages} />
            <OccasionSendMessageForm issuerId={occasionIssuerId} sendMessage={sendMessage} />
          </div>
        </div>
}