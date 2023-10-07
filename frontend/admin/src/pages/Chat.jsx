import React, { useEffect, useState } from "react";
import '../styles/chat.css';
import { Button } from '@mui/material';

import MessageContainer from "../components/Chat/MessageContainer";
import SendMessageForm from "../components/Chat/SendMessageForm";


export default function Chat ({roomId, sendMessage, messages, leaveRoom, setActiveRoomId}) {

    const leave = () => {
      setActiveRoomId("");
      leaveRoom(roomId);
    }

    return (
      <div className='app'>
        <div className='leave-room'>
          <Button variant='danger' onClick={() => leave()}>
            Leave Room
          </Button>
        </div>
  
        <div className='chat' >
          <MessageContainer messages={messages} />
          <SendMessageForm sendMessage={sendMessage} />
        </div>
      </div>
    )
}