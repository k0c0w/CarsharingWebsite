import React, {useEffect, useState} from 'react';
import '../css/popup-chat.css';
import { SendMessageForm, OccasionSendMessageForm } from '../Components/SendMessageForm';
import MessageContainer, {OccasionMessageContainer } from "../Components/MessageContainer";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import API from '../httpclient/axios_client';
import { AuthData } from '../Components/Auth/AuthWrapper';
import { ChatClient } from '../httpclient/grpc_clients.ts';

export default function PopupChat () {
    const { user } = AuthData();
    const [hiding, setHideFlag] = useState(true);
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
                const iHaveOpenOccasion = response.openedOccasionId != null;
                setIHaveOpenOccasion(iHaveOpenOccasion);
                if (iHaveOpenOccasion){
                    setMyOccasionId(response.openedOccasionId);
                }
            }
        }

        fetchOccasion();
    }, [user]);

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
                    {user.isAuthenticated && !iHaveOpenOccasion && !occasionCreationRequestSent &&
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
                    {user.isAuthenticated && iHaveOpenOccasion && 
                        <OccasionChat occasionId={myOccasionId} onCloseOccasionRecieved={onCloseOccasionRecieved}/>}
                </>}
            </div>
        </div>
                    );
}

function OccasionChat({occasionId, onCloseOccasionRecieved}) {
    const { getToken } = AuthData();
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
            attachments: [],
            isFromManager: receivedMessage.isFromManager
        }
        if (receivedMessage.attachmentId) {
            const attachmentInfo = await API.getAttachmentInfo(receivedMessage.attachmentId);
            if (attachmentInfo.successed){
                message.attachments = attachmentInfo.attachments;

            }
            else{
                message.attachments = [{download_url:attachmentInfo.defaultAttachment, content_type:"image/jpg", file_name: "not found.jpg"}];
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
          .withUrl(process.env.REACT_APP_WEBSITE_OCCASION_CHAT_URL , { 
            accessTokenFactory: getToken, 
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
            await loadOccasionChatHistory();
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
    const [client] = useState(() => new ChatClient());
    const [messages, setMessages] = useState([]);
  
    const sendMessage = async (message) => {
      try {
        await client.sendMessage(message.trim());
      } catch (e) {
        console.log(e)
      }
    }

    const onMessage = (message) => setMessages(old => [...old, message]);

    useEffect(() => {
        (async () => {
            const history = await client.getHistory();
            setMessages(history);
            await client.subscribeOnMessages(onMessage);
        })();

        return () => client.cancelSubscription();
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
