import React, { useState, useEffect } from "react";
import '../styles/chat.css';
import { Button } from '@mui/material';

import {OccasionMessageContainer} from "../components/Chat/MessageContainer";
import  {OccasionSendMessageForm} from "../components/Chat/SendMessageForm";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import API from "../httpclient/axios_client";

export default function Chat ({sendMessage, messages, leaveRoom, isDocumentsEnabled=true}) {

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

  async function loadHistory() {
    const occasion = await API.getOccasionById(occasionId);
    if (!occasion.successed){
      setErrorMessage("Ошибка при открытии диалога.");
      onLeaveRoom();
      return;
    }
    setOccasionIssuerId(occasion.occasion.issuerId);
    setMessages([]);
  }

  async function processMessage(receivedMessage) {
    const message = {
        text: receivedMessage.text,
        authorName: receivedMessage.authorName,
        attachments: []
    }
    if (receivedMessage.attachmentId) {
        const attachmentInfo = await API.getAttachmentInfo(receivedMessage.attachmentId);
        if (attachmentInfo.successed){
            message.attachments = attachmentInfo.attachments;
        }
        else{
            message.attachments = [{download_url:attachmentInfo.defaultAttachment, content_type:"image/jpg"}];
        }
    }

    setMessages(message => [...messages, message])
}  

  async function startConnection() {
    try {
        const localConnection = new HubConnectionBuilder()
            .withUrl("https://localhost:7129/occasion_chat", { accessTokenFactory: () => localStorage.getItem("token") })
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

  const leaveRoom = async (roomId) => {
      try {
          //await connection.invoke('LeaveRoom', roomId);
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