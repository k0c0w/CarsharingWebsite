import { Form, InputGroup } from 'react-bootstrap';
import { OutlinedInput, Button } from '@mui/material';
import { useRef, useState } from 'react';
import {AiFillFileImage} from "react-icons/ai"
import {MdDelete} from "react-icons/md"
import API from '../../httpclient/axios_client';


const SendMessageForm = ({ sendMessage }) => {
    //const theme = useTheme();
    //const color = tokens(theme.palette.mode);
    const [message, setMessage] = useState('');

    return <form
        onSubmit={e => {
            e.preventDefault();
            sendMessage(message);
            setMessage('');
        }}>
        <div>
            <TextField fullWidth style={{ maxWidth:'60%' }}  variant={'outlined'} type="user" placeholder="Сообщение..."
                onChange={e => setMessage(e.target.value)} value={message} />
            <Button variant={'outlined'} fullWidth style={{ maxWidth:'20%', height:'55px' }} color={"success"} type="submit" disabled={message === ""}>Send</Button>
        </div>
    </form>
}


export function OccasionSendMessageForm({issuerId, sendMessage}) {
    const [message, setMessage] = useState(null);
    const [attachments, setAttachments] = useState([]);
    const [blockSendButton, setBlockSendButton] = useState(false);
    const [sendErrorMessage, setSendErrorMessage] = useState(null);
    const inputRef = useRef();

    function fileAttachmentsChanged({target: {files}}) {
        if (!files)
            return;
        if (files.length > 5)
        {
            setSendErrorMessage("Не более 5 файлов.");
            return;
        }
        const filesArray = [];
        const length = files.length;
        let i = 0;
        while (i < length){
            filesArray[i] = files.item(i);
            i++;
        }
        setAttachments([...filesArray]);
        if (inputRef?.current) {
            inputRef.current.value = "";
        }
    }

    function removeFileFromAttachmentsByIndex(i) {
        if (-1 < i && i < attachments.length){
            const without = [...attachments];
            without.splice(i);
            setAttachments([...without]);
        }
    }

    async function onSendButtonClicked(e) {
        if (message == null && attachments.length == 0)
            return;

        setBlockSendButton(true);
        try{
            let attachmentId = null;

            if (attachments.length > 0){
                const creationStatus = await API.addAttachment(issuerId, attachments);
                if (creationStatus.successed){
                    attachmentId = creationStatus.attachmentId;
                    setAttachments([]);
                }
                else{
                    setSendErrorMessage("Ошибка отправки.");
                    return;
                }
            }
            sendMessage({text: message ?? "", attachmentId: attachmentId});
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
                    <div style={{ maxWidth:'5%', marginRight: "5px", height:"25px"}} onClick={() => inputRef.current.click()}>
                        <svg xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" xmlnsketch="http://www.bohemiancoding.com/sketch/ns"width="inherit" height="inherit" viewBox="0 0 59 63" version="1.1">
                            <defs/>
                            <g id="Page-1" stroke="none" strokeWidth="1" fill="none" fillRule="evenodd" sketchtype="MSPage">
                                <path d="M36.3,46.5 L12.1,22.4 C9.4,19.7 9.1,15.6 11.8,12.9 L11.8,12.9 C14.5,10.2 19.1,10.1 21.8,12.8 L53.6,44.4 C57.8,48.6 58.2,54.9 54.5,58.5 L54.5,58.5 C50.8,62.1 44.5,61.7 40.3,57.6 L4.7,22.1 C-0.1,17.4 -0.2,9.8 4.3,5.3 L5.3,4.3 C9.8,-0.2 17.4,-0.1 22.2,4.7 L46,28.4" id="Paper-clip" stroke="#6B6C6E" strokeWidth="2" sketchtype="MSShapeGroup"/>
                            </g>
                        </svg>
                        <input ref={inputRef} accept="image/jpg, image/jpeg, image/png, application/pdf" multiple type="file" hidden onChange={fileAttachmentsChanged}/>
                    </div>
                }
            <Button variant={"contained"} fullWidth style={{ maxWidth:'30%', borderRadius:'0px' }} color={"success"} type="submit" disabled={blockSendButton}>Отправить</Button>
        </InputGroup>
        <div className='attachments-list'>
            {attachments.map((file, i) => 
            <div style={{display:"inline-block"}} key={i}>
            <AiFillFileImage color='#1475cf'/>
            <span>{file.name}</span>
            <MdDelete onClick={() => removeFileFromAttachmentsByIndex(i)}/>
            </div>)}
        </div>
        <div className='chat-send-error' color="red">
            {sendErrorMessage}
        </div>
    </Form>
}

export default SendMessageForm;
