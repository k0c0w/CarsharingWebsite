import React, { useEffect, useState } from "react";
import API from "../httpclient/axios_client";
import '../styles/chat.css';
import { useLocation } from "react-router-dom";
import { Button } from '@mui/material';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Location } from "react-router-dom";
import MessageContainer from "../components/Chat/MessageContainer";
import SendMessageForm from "../components/Chat/SendMessageForm";


export default function Chat () {
    const [connection, setConnection] = useState()
    const [messages, setMessages] = useState([])
    const query = new URLSearchParams(useLocation().search);
    var connection_id;

    var isConnected = () => {
        var boolean = connection === null;
        console.log(messages)
        return !boolean;
    }

    useEffect (()=>{
        connection_id = query.get("connection_id");
        joinRoom(connection_id)

        return (connection) => {
            debugger
            connection.close();
        }
    },[])

    const joinRoom = async (connection_id) => {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl('https://localhost:7129/chat')
          .configureLogging(LogLevel.Information)
          .build()
        
        connection.on('ReceiveMessage', (role, message) => {
          debugger;
          setMessages(messages => [...messages, { role, message }]) 
        })
  
  
        connection.onclose(e => {
          setConnection()
          setMessages([])
        })
  
        await connection.start()
        await connection.invoke('ConnectTechSuppor', { ConnectionId: connection_id }) //JoinRoom 
        setConnection(connection)
      } catch (e) {
        console.log(e)
      }
    }
  
    const sendMessage = async message => {
      try {
        var role = 1
        await connection.invoke('SendMessage', { MemberTypeInt: role, Text: message })
      } catch (e) {
        console.log(e)
      }
    }
  
    const closeConnection = async () => {
      try {
          await connection.stop()
          alert("LOL");
      } catch (e) {
        console.log(e)
      }
    }
  
    return (
      <div className='app'>
        { isConnected() &&
        <div className='leave-room'>
          <Button variant='danger' onClick={() => closeConnection()}>
            Leave Room
          </Button>
        </div>
        }
  
        <div className='chat'>
          { isConnected() && <>
          <MessageContainer messages={messages} />
          <SendMessageForm sendMessage={sendMessage} />
          </> }
          { !isConnected() && <>
          <Button variant='outlined' className='chat-button-start' onClick={()=>joinRoom(connection_id)}>Открыть диаолог</Button>
          </> }
        </div>
      </div>
    )
}