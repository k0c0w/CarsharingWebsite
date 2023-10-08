import { Form, InputGroup } from 'react-bootstrap';
import { OutlinedInput, Button } from '@mui/material';
import { useState } from 'react';


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

export default SendMessageForm;
