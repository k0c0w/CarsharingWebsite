import { useEffect, useRef } from 'react';

const MessageContainer = ({ messages }) => {
    const messageRef = useRef();
    debugger;
    const isFromClient = (message) => !message.isFromManager;
    const isFromTechSupport = (message) => message.isFromManager;

    useEffect(() => {
        if (messageRef && messageRef.current) {
            const { scrollHeight, clientHeight } = messageRef.current;
            messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' });
        }
    }, [messages]);

    return <div ref={messageRef} className='message-container' >
        {messages.map((m, i) => (
                <div key={i}>
                    { isFromClient(m) &&
                        <div className='user-message' style={{marginTop:"25px"}}>
                            <div className='message bg-primary'>{m.text}</div>
                        </div>
                    }
                    { isFromTechSupport(m) &&
                        <div className='other-message' style={{marginTop:"25px"}}>
                            <div className='message bg-primary'>{m.text}</div>
                            <div>{m.authorName}</div>
                        </div>
                    }
                </div>
            )
        )}
    </div>
}

export default MessageContainer;
