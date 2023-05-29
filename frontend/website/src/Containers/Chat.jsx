import  SendMessageForm  from '../Components/SendMessageForm'
import  MessageContainer  from '../Components/MessageContainer'
import { Button } from '@mui/material';
import "../css/chat.css";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import { useEffect, useState } from 'react'


const Chat = () => {
  const [connection, setConnection] = useState()
  const [messages, setMessages] = useState([])
  const [receivedMessages, setRecMessages] = useState([]);
  const [users, setUsers] = useState([])


  useEffect(() => {
    return () => {
      closeConnection();  
    };
  }, [])


  const joinRoom = async () => {
    try {
      const connection = new HubConnectionBuilder()
        .withUrl('https://localhost:7129/chat')
        .configureLogging(LogLevel.Information)
        .build()

      connection.on('ReceiveMessage', (role, message) => {
        setMessages(messages => [...messages, { role, message }]) 
      })


      connection.onclose(e => {
        setConnection()
        setMessages([])
        setUsers([])
      })

      await connection.start()
      await connection.invoke('CreateChatRoom', {  }) //JoinRoom 
      setConnection(connection)
    } catch (e) {
      console.log(e)
    }
  }

  const sendMessage = async message => {
    try {
      var role = 0
      await connection.invoke('SendMessage', { MemberTypeInt: role, Text: message })
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
    <div className='app'>
      { (connection!=null) &&
      <div className='leave-room'>
        <Button variant='danger' onClick={() => closeConnection()}>
          Leave Room
        </Button>
      </div>
      }


      <div className='chat'>
        { (connection!=null) && <>
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
