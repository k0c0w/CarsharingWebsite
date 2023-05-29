import React, { useEffect, useState } from "react";
import API from "../httpclient/axios_client";
import '../styles/chat.css';
import { useLocation } from "react-router-dom";
import { Button } from '@mui/material';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import MessageContainer from "../components/Chat/MessageContainer";
import SendMessageForm from "../components/Chat/SendMessageForm";


export default function Chat ({ savedConnection, setSavedConnection }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [connection, setConnection] = useState(savedConnection)
    const [messages, setMessages] = useState([])
    const query = new URLSearchParams(useLocation().search);
    const [connectionId, setConnectionId] = useState("");
    var _connection_id;

    var isConnected = () => {
        var boolean = connection === null;
        return !boolean;
    }

    useEffect (()=>{
        _connection_id = query.get("connection_id")
        if (savedConnection === undefined) {
            debugger;
            setConnectionId( _connection_id );
            joinRoom( _connection_id )
        }
        else {
            setConnection( savedConnection );
            debugger;
            configureConnection( savedConnection, _connection_id )
            console.log(connection);
        }
        return ()=>{
            console.log(savedConnection)
        }
    },[])

    const configureConnection = async (_connection, _connection_id) => {
        
        debugger

        setConnection(_connection);
        setSavedConnection(_connection);

        _connection.on('ReceiveMessage', (role, message) => {
            setMessages(messages => [...messages, { role, message }]) 
        })
  
        _connection.on('ReceiveLatestMessages', (_roles, _messages) => {
                debugger;
                for (let i = 0; i < _roles.length; i++) {
                    console.log(_roles[i]);
                    console.log(_messages[i])
                    setMessages(messages => [...messages, { role: _roles[i], message: _messages[i] }])
                    console.log(`---------${messages}`) 
                }
                // setMessages(arr);
                console.log(messages);
                debugger
        })
    
        _connection.onclose(e => {
            setConnection()
            setMessages([])
        })
        
        debugger

        console.log(_connection_id)

        if (_connection._connectionState === "Disconnected")
            await _connection.start()
        
        setConnection(_connection);
        setSavedConnection(_connection);
        console.log(connection)

        await _connection.invoke('ConnectTechSupporToClient', { ConnectionId: _connection_id }) //JoinRoom 
        
        await _connection.invoke('GetLatestMessages', { MemberTypeInt: 1, Text: "sss" })
        debugger;
    } 

    const joinRoom = async (connection_id) => {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl('https://localhost:7129/chat')
          .configureLogging(LogLevel.Information)
          .build()

        setConnection(connection);

        configureConnection(connection , connection_id);

      } catch (e) {
        console.log(e)
      }
    }
  
    const sendMessage = async message => {
      try {
        var role = 1
        debugger
        await connection.invoke('SendMessage', { MemberTypeInt: role, Text: message })
      } catch (e) {
        console.log(e)
      }
    }
  
    const closeConnection = async () => {
      try {
          debugger
          await connection.stop()
          setSavedConnection();
      } catch (e) {
        console.log(e)
      }
    }

    const getLatestMessages = async () => {
        try {
            await connection.invoke('GetLatestMessages', { MemberTypeInt: 1, Text: "sss" })
        } catch (e) {
            console.log(e)
            debugger
        }
      }  
  
    return (
      <div className='app'>
        { isConnected() &&  //{() => closeConnection()}</div>
        <div className='leave-room'>
          <Button variant='danger' onClick={() => closeConnection()}>
            Leave Room
          </Button>
        </div>
        }
  
        <div className='chat' >
          { isConnected() && <>
          <MessageContainer messages={messages} />
          <SendMessageForm sendMessage={sendMessage} />
          </> }
          { !isConnected() && <>
          <Button variant='outlined' className='chat-button-start' onClick={()=>joinRoom(connectionId)}>Открыть диаолог</Button>
          </> }
        </div>
      </div>
    )
}