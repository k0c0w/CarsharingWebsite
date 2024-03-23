import { useEffect, useRef, useState } from "react";
import IndexAbout from "../Components/Index/IndexAbout";
import IndexChat from "../Components/Index/IndexChat";
import IndexTariffs from "../Components/Index/IndexTariffs";
import { GreetingSection} from "../Components/Sections"
import {DocumentTitle} from "../DocumentTitle"
import  API  from "../httpclient/axios_client";
import "../css/common.css";

function scrollTo({ref, hash}) {
    const options = {behavior: 'smooth'};
    if(ref){
        ref?.current.scrollIntoView(options);
    }
    if(hash && hash?.length > 0)
        document.getElementById(hash)?.scrollIntoView(options);
}


export default function Index({user}) {
    const tariffs = useRef(null);
    const chat = useRef(null);
    const [tariffsData, setTariffsData] = useState([]);
    useEffect(() => {
        async function fetchData() {
            const response = await API.tariffs();
            if(response.successed)
                setTariffsData(response.data);
        }
        fetchData();
    }, []);

    return <>
        <DocumentTitle>Drive</DocumentTitle>
        <GreetingSection title="Drive" subtitle="Онлайн аренда автомобиля" backgroundImageClass="index-greeting">
            <div className="greeting-buttons">
                <button className="button" onClick={() => scrollTo({ref:tariffs})}>Аренда</button>
                <button className="button" onClick={() => scrollTo({ref:chat})}>Задать вопрос</button>          
            </div>
        </GreetingSection>
        <IndexAbout/>
        <IndexTariffs ref={tariffs} tariffs={tariffsData}/>
        <IndexChat user={user} ref = {chat}/>
        </>;
}