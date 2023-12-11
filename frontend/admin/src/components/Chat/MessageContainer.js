import React,{ useEffect, useRef } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';

const MessageContainer = ({ messages }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const messageRef = useRef();

    useEffect(() => {
        if (messageRef && messageRef.current) {
            const { scrollHeight, clientHeight } = messageRef.current;
            messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' });
        }
    }, [messages]);

    return <div ref={messageRef} className='message-container' style={{backgroundColor: color.primary[500]}}>
        {messages.map((m, index) => (
            <>
                    {!m.isFromManager && <div key={index} className='other-message' style={{marginTop:"25px"}}>
                        <div className='message bg-primary'>{m.text}</div>
                        <div>{m.authorName}</div>
                    </div>}
                    {m.isFromManager && 
                    <div key={index} className='user-message' style={{marginTop:"25px"}}>
                        <div className='message bg-primary'>{m.text}</div>
                        <div>{m.authorName}</div>
                    </div>}
            </>
        ))}
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


const Attachment = ({key, info, number}) => <div key={key} figure={number} className='attachment'>{info}</div>

export default MessageContainer;
    