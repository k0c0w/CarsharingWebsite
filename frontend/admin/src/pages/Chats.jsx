import { useEffect, useState } from "react";
import '../styles/chats-container.css';
import RoomList from "../components/Chat/RoomList";
import Chat from "./Chat";
import { ChatClient } from "../httpclient/grpc_clients.ts";

export default function Chats () {
    const [onlineRooms, setOnlineRooms] = useState([]);
    const [activeRoom, setActiveRoom] = useState(null);
    const [client] = useState(new ChatClient());

    useEffect(() => {
      (async () => {
        const topics = await client.getActiveRooms();
        const mappedRooms = topics.map(topic => { return { topic }; });
        setOnlineRooms([...onlineRooms, ...mappedRooms]);
      })();

    }, []);

    return (
        <>
            {!activeRoom && <RoomList joinRoom={(topic) => setActiveRoom(topic)} onlineRooms={onlineRooms}/>}
            {activeRoom && <Chat topic = {activeRoom} leaveRoom = {() => setActiveRoom(null)}/>}
        </>
    )
}