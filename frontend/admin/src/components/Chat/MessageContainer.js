import React,{ useEffect, useRef } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../../theme';
import { AiFillFile } from 'react-icons/ai';
import API from '../../httpclient/axios_client';


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
            <div key={i} className={isFromClient(m) ? 'other-message' : 'user-message'}>
                <div  style={{marginTop:"0px"}} id={i}>
                    {m.attachments && m.attachments.map((x,i) => 
                        <div key={i} className='attachments-container'>
                            <Attachment link={x.download_url} contentType={x.content_type} fileName={x.file_name}/>
                        </div>
                    )}
                    <div className='message bg-primary'>{m.text}</div>
                </div>
                <div className='info-message-text'>{m.authorName}</div>
            </div>
            )
        )}
    </div>
}


function Attachment({fileName, link, contentType}) {
    const imgRef = useRef(null);

    async function downloadAttachment(){
        const fileResult = await API.getAttachmnet(link);
        if (!fileResult.successed)
            return;
        
        const href = URL.createObjectURL(fileResult.file);

        if (contentType.startsWith("image/") && imgRef.current)
        {
            imgRef.current.src = href;
        }
        else{
            const aElement = document.createElement("a");
            aElement.setAttribute("download", fileName);
            aElement.href = href;
            aElement.setAttribute("target", "_blank");
            aElement.click();
            URL.revokeObjectURL(href);
        }
    }
    useEffect(() => {
        if (contentType.startsWith("image/"))
            downloadAttachment();

    }, []);

    if (contentType.startsWith("image/"))
        return <img ref={imgRef} alt={fileName} style={{maxWidth: 200}}/>

    return <AiFillFile color='#1475cf' onClick={() => downloadAttachment()}/>
}
export default MessageContainer;
    