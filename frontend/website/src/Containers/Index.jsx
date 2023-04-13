import { useEffect, useRef, useState } from "react";
import IndexAbout from "../Components/Index/IndexAbout";
import IndexChat from "../Components/Index/IndexChat";
import IndexTariffs from "../Components/Index/IndexTariffs";
import { GreetingSection} from "../Components/Sections"
import {DocumentTitle} from "../DocumentTitle"
import "../css/common.css";
import axiosInstance, {getDataFromEndpoint} from "../httpclient/axios_client";
import { useOutletContext } from "react-router-dom";

function scrollTo({ref, hash}) {
    const options = {behavior: 'smooth'};
    if(ref){
        ref?.current.scrollIntoView(options);
    }
    if(hash && hash?.length > 0)
        document.getElementById(hash)?.scrollIntoView(options);
}


export default function Index() {
    const tariffs = useRef(null);
    const chat = useRef(null);
    const info = useOutletContext();
    //const [tariffsData, setTariffsData] = useState([]);
    //useEffect(()=> {getDataFromEndpoint("tariff/tariffs", setTariffsData)}, []);


    return <>
        <DocumentTitle>Drive</DocumentTitle>
        <GreetingSection title="Drive" subtitle="Онлайн аренда автомобиля" backgroundImageClass="index-greeting">
            <div className="greeting-buttons">
                <button className="button" onClick={() => scrollTo({ref:tariffs})}>Аренда</button>
                <button className="button" onClick={() => scrollTo({ref:chat})}>Задать вопрос</button>          
            </div>
        </GreetingSection>
        <IndexAbout/>
        <IndexTariffs ref={tariffs} tariffs={info}/>
        <IndexChat ref = {chat}/>
        </>;
}