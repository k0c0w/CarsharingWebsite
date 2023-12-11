import {useEffect, useRef} from 'react';
import { AiFillFile } from 'react-icons/ai';
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

export function OccasionMessageContainer ({ messages }) {
    const messageRef = useRef();
    const isFromClient = (message) => !message.isFromManager;

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
                <div className={isFromClient(m) ? 'message-sent':'message-rcvd'} style={{marginTop:"0px"}} id={i}>
                    {m.attachments && 
                        <div className='attachments-container'>
                            {m.attachmets.map((x, i) => <Attachment key={i} info={x} number={i}/>)}
                        </div>
                    }
                    <div className='message'>{m.text}</div>
                </div>
                <div className='author'>{m.authorName}</div>
            </>
            )
        )}
    </div>
}

const Attachment = ({key, info, number}) => {

    function BodyByContentType({contentType, link}) {
        if (contentType.startsWith("image/"))
        return <img src={link} alt={link} style={{maxWidth: 200}}/>
        else 
        return <AiFillFile color='#1475cf' src={link} />
    }

    return <div key={key} figure={number} className='attachment'><BodyByContentType contentType={info.content_type} link={info.download_link} /></div>
}

export default MessageContainer;
