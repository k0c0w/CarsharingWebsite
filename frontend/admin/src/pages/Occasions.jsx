import TextField from '@mui/material/TextField';
import {Box, Button} from '@mui/material';
import { FormControl, FormLabel } from '@mui/material';
import {useTheme} from "@emotion/react";
import {tokens} from "../theme";
import { DataGrid } from '@mui/x-data-grid';
import React, {useEffect, useState} from "react";
import API from "../httpclient/axios_client";
import { TableAddRefreshButtons } from '../components/TableCommon';
import { Popup } from '../components/Popup';
import { styleTextField } from "../styleComponents";
import DocumentsGrid from "../components/DocumentsPage/DocumentsGrid";

export default Occasions() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
}


function OpenOccasions() {
    const [occasions, setOccasions] = useTheme([]);

    return (
    <>
        <div className="chats">
            {occasions.map((occasion, index) => ( 
                <div className="chat-wrapper" style={{backgroundColor: room.pending ? 'orange' : ''}} key={index}> 
                        <div className="name"> {room.roomName} </div>
                        <div className="text-message"> {room.roomId} </div>
                        <button onClick={() => joinRoom(room.roomId)}>Присоединиться</button>
                </div>
            ))}
        </div>
    </>
    );
}