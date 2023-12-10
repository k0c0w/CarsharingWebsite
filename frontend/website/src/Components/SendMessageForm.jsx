import { Form, InputGroup } from 'react-bootstrap';
import { OutlinedInput, Button } from '@mui/material';
import { useState } from 'react';
import API from '../httpclient/axios_client';


const SendMessageForm = ({ sendMessage }) => {
    const [message, setMessage] = useState('');
    
    return <Form
        onSubmit={e => {
            e.preventDefault();
            sendMessage(message);
            setMessage('');
        }}>
        <InputGroup style={{display:'flex', alignItems:'center'}}>
            <OutlinedInput sx={{
                "& fieldset": { border: 'none' },
            }} multiline maxRows={3} fullWidth style={{ maxWidth: '65%' }} type="user" placeholder="Сообщение..."
                onChange={e => setMessage(e.target.value)} value={message} />
            <Button variant={"contained"} fullWidth style={{ maxWidth:'35%', borderRadius:'0px' }} color={"success"} type="submit" disabled={message === ""}>Отправить</Button>
        </InputGroup>
    </Form>
}

export function OccasionSendMessageForm({sendMessage}) {
    const [message, setMessage] = useState(null);
    const [attachments, setAttachments] = useState([]);
    const [blockSendButton, setBlockSendButton] = useState(false);
    const [sendErrorMessage, setSendErrorMessage] = useState(null);

    function fileAttachmentsChanged() {

    }

    async function onSendButtonClicked(e) {
        e.preventDefault();
        if (message == null && attachments.length == 0)
            return;

        setBlockSendButton(true);
        try{
            let attachmentId = null;

            if (attachments.length > 0){
                const creationStatus = await API.addAttachment(attachments);
                if (creationStatus.successed){

                }
                else{
                    setSendErrorMessage("Ошибка отправки.");
                    return;
                }
            }
            const messageInput = {
                message: message, 
                attachmentId: attachmentId
            };
            sendMessage(messageInput);
        }
        finally
        {
            setBlockSendButton(false);
        }
    }

    return <Form
        encType='multipart/form-data'
        onSubmit={e => {
            e.preventDefault();
            onSendButtonClicked();
        }}>
        <InputGroup style={{display:'flex', alignItems:'center'}}>
            <OutlinedInput sx={{
                "& fieldset": { border: 'none' },
            }} multiline maxRows={3} fullWidth style={{ maxWidth: '65%' }} type="user" placeholder="Сообщение..."
                onChange={e => setMessage(e.target.value)} value={message} />
                {attachments.length <= 5 &&
                    <div style={{ maxWidth:'5%'}}>
                        <svg xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" xmlnSketch="http://www.bohemiancoding.com/sketch/ns" width="59px" height="63px" viewBox="0 0 59 63" version="1.1">
                            <defs/>
                            <g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd" sketchType="MSPage">
                                <path d="M36.3,46.5 L12.1,22.4 C9.4,19.7 9.1,15.6 11.8,12.9 L11.8,12.9 C14.5,10.2 19.1,10.1 21.8,12.8 L53.6,44.4 C57.8,48.6 58.2,54.9 54.5,58.5 L54.5,58.5 C50.8,62.1 44.5,61.7 40.3,57.6 L4.7,22.1 C-0.1,17.4 -0.2,9.8 4.3,5.3 L5.3,4.3 C9.8,-0.2 17.4,-0.1 22.2,4.7 L46,28.4" id="Paper-clip" stroke="#6B6C6E" stroke-width="2" sketchType="MSShapeGroup"/>
                            </g>
                        </svg>
                        <input accept="image/jpg, image/jpeg, image/png, application/pdf" multiple type="file" style={{opacity: 0}} onChange={fileAttachmentsChanged}/>
                    </div>
                }
            <Button variant={"contained"} fullWidth style={{ maxWidth:'30%', borderRadius:'0px' }} color={"success"} type="submit" disabled={blockSendButton}>Отправить</Button>
        </InputGroup>
        <div className='chat-send-error'>
            {sendErrorMessage}
        </div>
    </Form>
}

export default SendMessageForm;
