import React from "react"

export default function RoomList({onlineRooms, joinRoom}) {
    
    return (<div className="chats">
            {onlineRooms.map((room, index) => ( 
                <div className="chat-wrapper" style={{backgroundColor: room.pending ? 'orange' : ''}} key={index}> 
                        <div className="name"> {room.roomName} </div>
                        <div className="text-message"> {room.roomId} </div>
                        <button onClick={() => joinRoom(room.roomId)}>Присоединиться</button>
                </div>
            ))}
        </div>);
}