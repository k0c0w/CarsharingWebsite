import React, { useState, useEffect } from "react";
import '../styles/chat.css';
import { Button } from '@mui/material';

import {OccasionMessageContainer} from "../components/Chat/MessageContainer";
import  {OccasionSendMessageForm} from "../components/Chat/SendMessageForm";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

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

export function OccasionChat({occasionId, onLeaveRoom}) {
  const [connection, setConnection] = useState(null);
  const [messages, setMessages] = useState([]);

  async function loadHistory() {
    setMessages([]);
  }

  async function startConnection() {
    try {
        const localConnection = new HubConnectionBuilder()
            .withUrl(process.env.REACT_APP_WEBSITE_OCCASION_CHAT_URL, { accessTokenFactory: () => localStorage.getItem("token") })
            .configureLogging(LogLevel.Information)
            .build();

        localConnection.on('RecieveMessage', (message) => {
            setMessages(messages => [...messages, message])
        });
        
        localConnection.onclose(() => {
            onLeaveRoom();
        });

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
            RoomId: activeOccasion.roomId,
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
            <OccasionSendMessageForm sendMessage={sendMessage} />
          </div>
        </div>
}