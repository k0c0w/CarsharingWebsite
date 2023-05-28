import { TextField, Button } from '@mui/material';
import { useState } from 'react';

const SendMessageForm = ({ sendMessage }) => {
    const [message, setMessage] = useState('');
    

    return <form
        onSubmit={e => {
            e.preventDefault();
            debugger
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

export default SendMessageForm;
