import {useEffect, useRef} from 'react';
import { AiFillFile } from 'react-icons/ai';
import '../css/popup-chat.css';
import API from '../httpclient/axios_client';

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
    const isFromClient = (message) => message.isFromManager === false;
    
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
                    {m.attachments && m.attachments.map(x => 
                        <div key={i} className='attachments-container'>
                            <Attachment link={x.download_url} contentType={x.content_type} fileName={x.file_name}/>
                        </div>
                    )}
                    <div className='message'>{m.text}</div>
                </div>
                <div className='author'>{m.authorName}</div>
            </>
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
