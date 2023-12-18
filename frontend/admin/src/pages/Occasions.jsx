import React, { useEffect, useState } from "react";
import API from "../httpclient/axios_client"
import '../styles/chats-container.css';
import { OccasionChat } from "./Chat";
import OccasionsList from "../components/OccasionsPage/OccasionsList";


export default function Occasions () {

    const [uncompletedOccasions, setUncompletedOccasions] = useState([]);
    const [activeOccasion, setActiveOccasion] = useState(null);
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

    return (
        <>
            {!activeOccasion && <OccasionsList onlineOccasions={uncompletedOccasions} joinOccasion={setActiveOccasion} closeOccasion={completeOccasion}/>}
            {errorMessage && <div style='color:"red"'>{errorMessage}</div>}
            {activeOccasion && <OccasionChat occasionId={activeOccasion} setErrorMessage={setErrorMessage} onLeaveRoom={() => setActiveOccasion(null)}/>}
        </>
    )
}