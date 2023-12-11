import React, { useEffect, useState } from "react";
import API from "../httpclient/axios_client"
import '../styles/chats-container.css';
import RoomList from "../components/Chat/RoomList";
import Chat from "./Chat";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import OccasionsList from "../components/OccasionsPage/OccasionsList";
import {Button} from "@mui/material";


export default function Occasions () {
    const [connection, setConnection] = useState(null);

    const [uncompletedOccasions, setUncompletedOccasions] = useState([]);
    const [activeOccasion, setActiveOccasion] = useState();

    const [activeRoomMessages, setActiveRoomMessages] = useState([])

    const [errorMessage, setErrorMessage] = useState();

    const completeOccasion = async (id) => {
        var result = await API.completeOccasion(id);
        if (result.successed){
            var _uncompletedOccasions = uncompletedOccasions.filter(function(item) {
                            return item.id !== id;
                        })
            setUncompletedOccasions([..._uncompletedOccasions]);
        }
        else {
            alert(result.errorMessage);
        }
    }

    const onJoinRoomResultRecieved = async (update) => {
        if (update.success){
            const history = await API.getOccasionChatHistory(update.roomId);
            setActiveRoomMessages(history);
            const room = uncompletedOccasions.find(elem => elem.roomId === update.roomId);
            setActiveOccasion(room);
        }
        else{
            setErrorMessage("Не возможно присоедениться к комнате");
        }
    }

    const onLeaveRoomResultRecieved = (update) => {
        if (update.success){
            setActiveOccasion();
            setActiveRoomMessages([]);
        }
        else{
            setErrorMessage("Не возможно поикнуть комнату");
        }
    }
    useEffect(() => {
        async function retrieveOccasions() {
            let result = await API.getUncompletedOccasions();
            if (result.successed !== true)
            {
                console.log(result.errorMessage);
                return;
            }
            const mappedOccasions = result?.occasions.map(occasion => {
                return { id: occasion?.id, type: occasion?.occasionType, topic: occasion?.topic}});
            setUncompletedOccasions([...uncompletedOccasions, ...mappedOccasions]);
        }
        retrieveOccasions();
    }, []);

    useEffect( () => {
            const startConnection = async () => {
                try {
                    const localConnection = new HubConnectionBuilder()
                        .withUrl(process.env.REACT_APP_WEBSITE_OCCASION_CHAT_URL, { accessTokenFactory: () => localStorage.getItem("token") })
                        .configureLogging(LogLevel.Information)
                        .build();

                    localConnection.on('RecieveMessage', (message) => {
                        setActiveRoomMessages(messages => [...messages, message])
                        //todo: should trigger only on message
                    });
                    localConnection.on('JoinRoomResult', update => onJoinRoomResultRecieved(update));
                    localConnection.on('LeaveRoomResult', update => onLeaveRoomResultRecieved(update));
                    localConnection.on('ChatRoomUpdate', update => {
                        console.debug(update);
                        switch(update.event) {
                            // Created
                            case 1:
                            {
                                const newRooms = [{roomName: update.roomName, roomId: update.roomId, pending: true}, ...uncompletedOccasions];
                                setUncompletedOccasions(newRooms);
                                break;
                            }
                            // Deleted
                            case 2:
                            {
                                const elemIndex = uncompletedOccasions.map(room => room.roomId).indexOf(update.roomId);
                                if (elemIndex > -1) {
                                    setUncompletedOccasions(uncompletedOccasions.splice(elemIndex, 1));
                                }

                                if (activeOccasion && activeOccasion.roomId === update.roomId){
                                    setErrorMessage("Комната была закрыта");
                                    setActiveRoomMessages([]);
                                    setActiveOccasion();
                                }

                                break;
                            }
                            // Manager joined
                            case 3: {
                                const joinedRoomIndex = uncompletedOccasions.map(room => room.roomId).indexOf(update.roomId);
                                if (joinedRoomIndex > -1) {
                                    const oldRoom = uncompletedOccasions[joinedRoomIndex];
                                    oldRoom.pending = false;
                                    uncompletedOccasions[joinedRoomIndex] = oldRoom;
                                    setUncompletedOccasions(uncompletedOccasions);
                                }
                                break;
                            }

                            // manager left
                            case 4: {
                                const joinedRoomIndex = uncompletedOccasions.map(room => room.roomId).indexOf(update.roomId);
                                const leftRoomIndex = uncompletedOccasions.map(room => room.roomId).indexOf(update.roomId);
                                if (joinedRoomIndex > -1) {
                                    const oldRoom = uncompletedOccasions[leftRoomIndex];
                                    oldRoom.pending = true;
                                    uncompletedOccasions[leftRoomIndex] = oldRoom;
                                    setUncompletedOccasions(uncompletedOccasions);
                                }
                                break;
                            }
                            default: {
                                console.log(update);
                                break;
                            }

                        }
                    });

                    localConnection.onclose(() => {
                        setConnection();
                        setActiveRoomMessages([]);
                        setActiveOccasion();
                    });
                    debugger
                    setConnection(localConnection);
                } catch (e) {
                    console.log(e);
                }
            }
            startConnection()
            // todo: causes multiple reconnections but without it onlinerooms are not rendered
        }, []
    );

    useEffect(() => {
        const startConnection = async () => {
            debugger;
            if (connection){
                await connection.start();
            }
        }

        startConnection();
    }, [connection])


    const joinRoom = async (roomId) => {
        debugger
        try {
            await connection.invoke('JoinRoom', roomId);
        } catch (e) {
            console.log(e)
        }
    }

    const leaveRoom = async (roomId) => {
        try {
            await connection.invoke('LeaveRoom', roomId);
        } catch (e) {
            console.log(e)
        }
    }

    const sendMessage = async (message) => {
        try {
            const messageModel = {
                Text: message,
                Time: new Date().toJSON(),
                RoomId: activeOccasion.roomId,
            };

            await connection.invoke('SendMessage', messageModel);
        } catch (e) {
            console.log(e)
        }
    }
    // <Button onClick={()=>alert("")}>Нажми на меня</Button>
    return (
        <>
            {!activeOccasion && <OccasionsList onlineOccasions={uncompletedOccasions} joinOccasion={joinRoom} closeOccasion={completeOccasion}/>}
            {errorMessage && <div style='color:"red"'>{errorMessage}</div>}
            {activeOccasion && <Chat leaveRoom={() => leaveRoom(activeOccasion?.roomId)} messages = {activeRoomMessages} sendMessage = {sendMessage}/>}
        </>
    )
}