import React from "react";
import '../styles/chat.css';
import { Button } from '@mui/material';

import MessageContainer from "../components/Chat/MessageContainer";
import SendMessageForm from "../components/Chat/SendMessageForm";

export default function Chat ({sendMessage, messages, leaveRoom, isDocumentsEnabled=true}) {

    return (
      <div className='app'>
        <div className='leave-room'>
          <Button variant='danger' onClick={leaveRoom}>
            Leave Room
          </Button>
        </div>
  
        <div className='chat' >
          <MessageContainer messages={messages} />
          { isDocumentsEnabled && <DocumentsPlaceHolder /> }
          <SendMessageForm sendMessage={sendMessage} />
        </div>
      </div>
    )
}

function DocumentsPlaceHolder () {
    return (
        <Button
            variant="contained"
            component="label"
        >
            Загрузить файл
            <input
                type="file"
                hidden
            />
        </Button>
    )
}