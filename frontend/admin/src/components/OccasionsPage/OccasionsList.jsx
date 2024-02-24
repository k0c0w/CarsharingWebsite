import React from "react"

export default function OccasionsList({onlineOccasions, joinOccasion, closeOccasion}) {

    return (<div className="chats">
        {onlineOccasions.map((occasion, index) => (
            <div className="chat-wrapper" key={index}>
                <div className="name"> {occasion.type} </div>
                <div className="text-message"> {occasion.topic} </div>
                <button onClick={() => joinOccasion(occasion.id)}>Присоединиться</button>
                <button onClick={() => closeOccasion(occasion.id)}>Закрыть обращение</button>
            </div>
        ))}
    </div>);
}