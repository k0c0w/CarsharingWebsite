import { useEffect, useRef } from 'react';

const MessageContainer = ({ messages }) => {
    const messageRef = useRef();

    const isFromClient = (message) => { debugger; return message.role == "0" }
    const isFromTechSupport = (message) => message.role == "1"
    const isInformation = (message) => message.role == "information"


    useEffect(() => {
        if (messageRef && messageRef.current) {
            const { scrollHeight, clientHeight } = messageRef.current;
            messageRef.current.scrollTo({ left: 0, top: scrollHeight - clientHeight, behavior: 'smooth' });
        }
    }, [messages]);

    return <div ref={messageRef} className='message-container' >
        {messages.map((m, index) => (
            <>
                { isFromClient(m) &&
                    <div key={index} className='other-message' style={{marginTop:"25px"}}>
                        <div className='message bg-primary'>{m.message}</div>
                    </div>
                }
                { isFromTechSupport(m) &&
                    <div key={index} className='user-message' style={{marginTop:"25px"}}>
                        <div className='message bg-primary'>{m.message}</div>
                    </div>
                }
                { isInformation(m) &&
                    <div key={index} className='info-message' style={{marginTop:"25px"}}>
                        <div className='info-message-text bg-primary'>{m.message}</div>
                    </div>
                }
            </>
        )
        )}
    </div>
}

export default MessageContainer;
