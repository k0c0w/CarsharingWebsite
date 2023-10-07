import { useEffect, useState } from "react";
import API from "../httpclient/axios_client"
import '../styles/chats-container.css';
import { useNavigate } from "react-router-dom";
import RoomList from "../components/Chat/RoomList";
import Chat from "./Chat";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


export default function Chats () {
    const [chats, setChats] = useState([]);
    const [messages, setMessages] = useState([])
    const [activeRoomId, setActiveRoomId] = useState();
    const [connection, setConnection] = useState();

    
    
      useEffect( () => {
        const startConnection = async () => {
            try {
            const localConnection = new HubConnectionBuilder()
             .withUrl('https://localhost:7129/chat')
              .configureLogging(LogLevel.Information)
             .build();
        
             localConnection.on('RecieveMessage', (message) =>
                setMessages(messages => [...messages, message]));
        
                localConnection.on('NewRoomCreated', roomInfo => setChats([...chats, roomInfo]));
        
                localConnection.on('OnlineRooms', (roomList) => {
                  setChats(roomList);
            });
        
            localConnection.onclose(e => {
                setConnection();
                setMessages([]);
                setUsers([]);
            });
        
            await localConnection.start();
                setConnection(localConnection);
            } catch (e) {
            console.log(e);
            }
        }
        startConnection()
      }, []
      );


    const joinRoom = async (roomId) => {
        try {
                setActiveRoomId(roomId);
                await connection.invoke('JoinRoom', roomId);
            } catch (e) {
            console.log(e)
            }   
    }

    const leaveRoom = async (roomId) => {
        try {
                setActiveRoomId(roomId);
                await connection.invoke('LeaveRoom', roomId);
            } catch (e) {
            console.log(e)
            }   
    }

    const sendMessage = async (message) => {
        try {
          const messageModel = {
            Text: message,
            Time: new Date().toJSON(),
            RoomId: activeRoomId,
          };
    
          await connection.invoke('SendMessage', messageModel);
        } catch (e) {
          console.log(e)
        }
      }

    const closeConnection = async () => {
      try {
        await connection.stop()
      } catch (e) {
        console.log(e)
      }
    }

    return (
        <>
            {!activeRoomId && <RoomList  roomInfos={chats} joinRoom = {(id) => {debugger; setActiveRoomId(id); joinRoom(id);}} a/>}
            {activeRoomId && <Chat roomId={activeRoomId} leaveRoom = {leaveRoom} messages = {messages} sendMessage = {sendMessage} setActiveRoomId = {setActiveRoomId}/>}
        </>
    )
}