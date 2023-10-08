import {useEffect, useRef} from 'react';
import '../css/popup-chat.css';

// eslint-disable-next-line react/prop-types
const MessageContainer = ({ messages }) => {
    const messageRef = useRef();
    const isFromClient = (message) => !message.isFromManager;
    const isFromTechSupport = (message) => message.isFromManager;

    useEffect(() => {
        if (messageRef && messageRef.current) {
            const { scrollHeight, clientHeight } = messageRef.current;
            messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' });
        }
    }, [messages]);

    return <div ref={messageRef} className='message-container' >
        {/* eslint-disable-next-line react/prop-types */}
        {messages?.map((m, i) => (
            <>
                    { isFromClient(m) &&
                        <div className='message-sent' style={{marginTop:"0px"}} id={i}>
                            <div className='message'>{m.text}</div>
                        </div>
                    }
                    { isFromTechSupport(m) && <>
                        <div className='message-rcvd' style={{marginTop:"0px"}} id={i}>
                            <div className='message'>{m.text}</div>
                            
                        </div>
                        <div className='author'>{m.authorName}</div>
                        </>
                    }
            </>
            )
        )}
    </div>
}

export default MessageContainer;
