import React, {useEffect, useState} from 'react';
import '../css/popup-chat.css';
import SendMessageForm, {OccasionSendMessageForm} from '../Components/SendMessageForm';
import MessageContainer, {OccasionMessageContainer } from "../Components/MessageContainer";
import { HubConnectionBuilder, LogLevel, TransportType } from '@microsoft/signalr'
import API from '../httpclient/axios_client';

export default function PopupChat () {
    const [hiding, setHideFlag] = useState(false);
    const [iHaveOpenOccasion, setIHaveOpenOccasion] = useState(null);
    const [myOccasionId, setMyOccasionId] = useState(null);
    const [occasionTypes, setOccasionTypes] = useState([]);
    const [occasionCreationRequestSent, setOccasionCreationRequestSent] = useState(false);

    function onCloseOccasionRecieved() {
        setMyOccasionId(null);
        setIHaveOpenOccasion(false);
    }

    async function tryCreateOccasion(occasionType) {
        setOccasionCreationRequestSent(true);
        try{
            const creationResult = await API.openNewOccasion(occasionType, occasionType);
            if (creationResult.successed){
                setMyOccasionId(creationResult.createdOccasionId);
                setIHaveOpenOccasion(true);
            }
            else{
                console.log(creationResult.errorMessage);
            }
        }
        finally{
            setOccasionCreationRequestSent(false);
        }
    }

    useEffect(() => {
        async function fetchOccasion(){
            const response = await API.loadMyOccasion();
            if (response?.successed){
                if (response.openedOccasionId){
                    setIHaveOpenOccasion(true);
                    setMyOccasionId(response.openedOccasionId);
                }
                else{
                    setIHaveOpenOccasion(false);
                }
            }
        }

        fetchOccasion();
    }, []);

    useEffect(() => {
        async function fetchOccasionTypes() {
            const response = await API.getOccasionTypes();
            if (response)
                setOccasionTypes(response);
        }
        
        fetchOccasionTypes();
    }, []);

    const switchHidingFlag = () => {
        setHideFlag(!hiding);
    }

    return (
        <div className='popup-chat' style={{ height:`${hiding?"30px":"60vh"}` }}>
            <div className='popup-chat-container'>
                <div className='popup-chat-header'  style={{flexGrow:0.5, display:'flex', flexDirection:'row', zIndex:'10'}}>
                    <div style={{zIndex:'10', color:'#767575'}} onClick={() => switchHidingFlag()}>=</div>
                </div>
                {!hiding && iHaveOpenOccasion != null &&
                <>
                    {!iHaveOpenOccasion && !occasionCreationRequestSent &&
                        <div className="dropdown">
                            <button className="dropbtn">Происшествие</button>
                            <div className="dropdown-content">
                                {occasionTypes.map((occasion, key) =>
                                    <div key={key} onClick={() => tryCreateOccasion(occasion)}>{occasion}</div>
                                )}
                            </div>
                        </div>
                    }
                    {!iHaveOpenOccasion && <DefaultSupportChat/>}
                    {iHaveOpenOccasion && <OccasionChat occasionId={myOccasionId} onCloseOccasionRecieved={onCloseOccasionRecieved}/>}
                </>}
            </div>
        </div>
        );
}

function OccasionChat({occasionId, onCloseOccasionRecieved}) {
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);

    const sendMessage = async ({text, attachmentId}) => {
        try {
          const messageModel = {
            Text: text,
            AttachmentId: attachmentId,
            OccasionId: occasionId,
            Time: new Date().toJSON(),
          };
    
          await connection.invoke('SendMessage', messageModel);
        } catch (e) {
          console.log(e)
        }
      }
    
    async function processMessage(receivedMessage) {
        const message = {
            text: receivedMessage.text,
            authorName: receivedMessage.authorName,
            attachmets: []
        }
        if (receivedMessage.attachmentId) {
            const attachmentInfo = await API.getAttachmentInfo(receivedMessage.attachmentId);
            if (attachmentInfo.successed){
                message.attachments = attachmentInfo.attachments;

            }
            else{
                message.attachmets = [{download_url:attachmentInfo.defaultAttachment, content_type:"image/jpg"}];
            }
        }

        setMessages(messages => [...messages, message])
    }  


    async function loadOccasionChatHistory() {
        const response = await API.loadOccasionHistory(occasionId);
        if (response?.successed){
            setMessages(response.messages);
        }
    }

    async function createHubConnection() {
        const con = new HubConnectionBuilder()
          .withUrl("https://localhost:7129/occasion_chat", { 
            accessTokenFactory: () => localStorage.getItem("token") 
        })
          .configureLogging(LogLevel.Information)
          .withAutomaticReconnect()
          .build();

        con.on('ReceiveMessage',
         (message) => {
            console.log(message);
            processMessage(message);
        });
        con.on('OccassionClosed', 
        () => {onCloseOccasionRecieved()});

        await con.start();
        setConnection(con);
      }

    useEffect(() => {
        async function init() {
            //await loadOccasionChatHistory();
            await createHubConnection();
        }
        init();

        return () => connection?.stop()
    }, []);

    return <>
        <div className='message-container-wrapper' style={{flexGrow:7}}>
            <OccasionMessageContainer messages={messages} />
        </div>
        <div className='popup-chat-input' >
            <OccasionSendMessageForm sendMessage={sendMessage} className='popup-chat-input' style={{flexGrow:0.5, width:'100%'}} />
        </div>
    </>
}

function DefaultSupportChat() {
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);
    const [connectedRoomId, setConnectedRoomId] = useState();

    async function onRecieveRoomId(roomId) {
        const history = await API.getChatHistory(roomId);
        setMessages(history);
        setConnectedRoomId(roomId);
      }

    const joinRoom = async () => {
      try {
        const connection = new HubConnectionBuilder()
          .withUrl("https://localhost:7129/chat", { accessTokenFactory: () => localStorage.getItem("token") })
          .configureLogging(LogLevel.Information)
          .withAutomaticReconnect()
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

        return () => connection?.stop();
    }, []);

    return <>
        <div className='message-container-wrapper' style={{flexGrow:7}}>
            <MessageContainer messages={messages} />
        </div>
        <div className='popup-chat-input' >
            <SendMessageForm sendMessage={sendMessage} className='popup-chat-input' style={{flexGrow:0.5, width:'100%'}} />
        </div>
    </>
}
