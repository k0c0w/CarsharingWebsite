import React from "react"

export default function RoomList({onlineRooms, joinRoom}) {
    
    return (<div className="chats">
            {onlineRooms.map((room, index) => ( 
                <div className="chat-wrapper" style={{backgroundColor: 'orange'}} key={index}> 
                        <div className="name"> {room.topic} </div>
                        <button onClick={() => joinRoom(room.topic)}>Присоединиться</button>
                </div>
            ))}
        </div>);
}