import { useRef } from "react";
import IndexAbout from "../Components/Index/IndexAbout";
import IndexChat from "../Components/Index/IndexChat";
import IndexTariffs from "../Components/Index/IndexTariffs";
import { GreetingSection} from "../Components/Sections"
import {DocumentTitle} from "../DocumentTitle"
import "../css/common.css";

function scrollTo(ref) {
    if(ref){
        ref?.current.scrollIntoView();
    }
}

export default function Index() {
    const tariffs = useRef(null);
    const chat = useRef(null);

    return <>
        <DocumentTitle>Drive</DocumentTitle>
        <GreetingSection title="Drive" subtitle="Онлайн аренда автомобиля"
            backgroundImageSource="../sources/greeting/main.jpg">
                <div className="greeting-buttons">
                    <button className="button" onClick={() => scrollTo(tariffs)}>Аренда</button>
                    <button className="button" onClick={() => scrollTo(chat)}>Задать вопрос</button>          
                </div>
        </GreetingSection>
        <IndexAbout/>
        <IndexTariffs ref={tariffs}/>
        <IndexChat ref = {chat}/>
        </>;
}