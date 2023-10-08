import React, {useState} from 'react';
import '../css/popup-chat.css';
import SendMessageForm from '../Components/SendMessageForm';
import MessageContainer from "../Components/MessageContainer";

export default function PopupChat () {
    const [messages, setMessages] = useState([]);
    const [hiding, setHideFlag] = useState(false);

    const sendMessage = (message) => {
        var elem = {
            text: message,
            isFromManager: true
        }
        setMessages([...messages, elem]);
    }

    var switchHidingFlag = () => {
        setHideFlag(!hiding);
    }

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
