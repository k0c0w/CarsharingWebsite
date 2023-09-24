import { useEffect, useState } from "react";
import API from "../httpclient/axios_client"
import '../styles/chats-container.css';
import { useNavigate } from "react-router-dom";


export default function Chats ({ savedConnectionId, setSavedConnectionId, savedConnection }) {
    const [chats, setChats] = useState([]);
    var navigate = useNavigate();

    async function getChats () {
        var _chats = await API.getOpenChats();
        setChats(_chats.data);
    }

    useEffect(() => {
        if (savedConnection === undefined){
            getChats();
        }
        else {
            return navigate(`chat?connection_id=${savedConnectionId}`)
        } 
    }, [])

    return (
        <div className="chats">
            {chats.map((item, index) => ( 
                <div className="chat-wrapper" key={index} onClick={()=>
                { setSavedConnectionId(item.connection_id); return navigate(`chat?connection_id=${item.connection_id}`);} }> 
                        <div className="name"> {item.first_name} {item.last_name} </div>
                        <div className="text-message"> {item.connection_id} </div>  
                </div>
            ))}
        </div>
    )
}