import React, {useEffect, useState} from 'react';
import '../css/popup-chat.css';
import SendMessageForm from '../Components/SendMessageForm';
import MessageContainer from "../Components/MessageContainer";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import API from '../httpclient/axios_client';


export default function PopupChat () {
    const [hiding, setHideFlag] = useState(false);

    const switchHidingFlag = () => {
        setHideFlag(!hiding);
    }

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
          .withUrl(process.env.REACT_APP_WEBSITE_CHAT_URL, { accessTokenFactory: () => localStorage.getItem("token") })
          .configureLogging(LogLevel.Information)
          .build();

        connection.on('RecieveMessage', (message) => setMessages(messages => [...messages, message]));

        connection.on('RecieveRoomId', (roomId) => onRecieveRoomId(roomId));

        connection.onclose(() => {
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

    useEffect(() => {
        joinRoom();
    }, []);

    return (
        <div className='popup-chat' style={{ height:`${hiding?"30px":"60vh"}` }}>
            <div className='popup-chat-container'>
                <div className='popup-chat-header'  style={{flexGrow:0.5, display:'flex', flexDirection:'row', zIndex:'10'}}>
                    <div style={{zIndex:'10', color:'#767575'}} onClick={() => switchHidingFlag()}>=</div>
                </div>
                {!hiding && <>
                <div className='message-container-wrapper' style={{flexGrow:7}}>
                    <MessageContainer messages={messages} />
                </div>
                <div className='popup-chat-input' >
                    <SendMessageForm sendMessage={sendMessage} className='popup-chat-input' style={{flexGrow:0.5, width:'100%'}} />
                </div>
                </>}
            </div>
        </div>
        );
}
