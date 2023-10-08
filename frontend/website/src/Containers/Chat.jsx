import  SendMessageForm  from '../Components/SendMessageForm'
import  MessageContainer  from '../Components/MessageContainer'
import { Button } from '@mui/material';
import "../css/chat.css";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { useState } from 'react'
import API from '../httpclient/axios_client';

const Chat = () => {
  const [connection, setConnection] = useState()
  const [messages, setMessages] = useState([])
  const [connectedRoomId, setConnectedRoomId] = useState();

  async function onRecieveRoomId(roomId) {
    const history = await API.getChatHistory(roomId);
    setMessages(history);
    setConnectedRoomId(roomId);
  }

  const joinRoom = async () => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl('https://localhost:7129/chat')
        .configureLogging(LogLevel.Information)
        .build();

      connection.on('RecieveMessage', (message) => setMessages(messages => [...messages, message]));
        
      connection.on('RecieveRoomId', (roomId) => onRecieveRoomId(roomId));

      connection.onclose(e => {
        setConnection();
        setMessages([]);
      });

      await connection.start();
      setConnection(connection);
    } catch (e) {
      console.log(e);
    }
  }

  const sendMessage = async (message) => {
    try {
      const messageModel = {
        Text: message,
        Time: new Date().toJSON(),
        RoomId: connectedRoomId,
      };

      await connection.invoke('SendMessage', messageModel);
    } catch (e) {
      console.log(e)
    }
  }

  return (
    <div className='app'>
      <div className='chat'>
        { connection!=null && <>
        <MessageContainer messages={messages} />
        <SendMessageForm sendMessage={sendMessage} />
        </> }
        { !(connection!=null) && <>
        <Button variant='outlined' className='chat-button-start' onClick={()=>joinRoom()}>Открыть диаолог</Button>
        </> }
      </div>
    </div>
  )
}

export default Chat
