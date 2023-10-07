import React from "react"

export default function RoomList({roomInfos, joinRoom}) {
    
    return (<div className="chats">
            {roomInfos.map((item, index) => ( 
                <div className="chat-wrapper" key={index}> 
                        <div className="name"> {item.name} </div>
                        <div className="text-message"> {item.roomId} </div>
                        <button onClick={() => joinRoom(item.roomId)}></button>
                </div>
            ))}
        </div>);
}