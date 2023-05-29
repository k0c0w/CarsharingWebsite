import { useEffect, useState } from "react";
import API from "../httpclient/axios_client"
import '../styles/chats-container.css';
import { useNavigate } from "react-router-dom";


export default function Chats () {
    const [chats, setChats] = useState([]);
    var navigate = useNavigate();

    async function getChats () {
        var _chats = await API.getOpenChats();
        debugger
        setChats(_chats.data)
    }

    useEffect(() => {
        getChats();
        
    }, [])
    return (
        <div className="chats">
            {chats.map((item, index) => ( 
                <div className="chat-wrapper" key={index} onClick={()=>navigate(`chat?connection_id=${item.connection_id}`)}> 
                        <div className="name"> {item.first_name} {item.last_name} </div>
                        <div className="text-message"> {item.connection_id} </div>  
                </div>
            ))}
        </div>
    )
}