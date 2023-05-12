import { useState } from "react";
import { useParams, Navigate, useOutletContext, useNavigate } from "react-router-dom";
import { CardHolder } from "../Components/Card";
import Container from "../Components/Container";
import Section, { GreetingSection, SectionTitle } from "../Components/Sections";
import { useEffect } from "react";
import { axiosInstance } from "../httpclient/axios_client";

const style = {     display: "flex",
    justifyContent: "space-evenly",
    alignItems: "center",
    minHeight: "fit-content", flexWrap: "wrap", rowGap: "50px", columnGap: "15px"}

export default function BeforeTariffs () {
    const info = useOutletContext();
    const [tariffs, setTariffs] = useState(info);
    const {tariffName} = useParams();
    /* todo: запрос если нет инфы
    
    const navigator = useNavigate();
    useEffect(() => {
      if(!tariffs) {
        axiosInstance.get(`/tariffs?${tariffName}`)
        .then(r => setTariffs(r.data))
        .catch(err => {});
      }
    }, []);*/

    const tariff = tariffs.find(x => x.name == tariffName);
    return tariff ? <Tariffs title={tariff.name} subtitle={tariff.description} backgroundImage={tariff.image_url}/> 
    : <Navigate to="/notFound"/>;
}

const t = () => {<><CardHolder/>
<CardHolder/>
<CardHolder/></>};


export function CarListSection () {
    return (
    <Section  style={{backgroundColor:"#DEF0F0"}}>
        <Container>
            <SectionTitle style={{paddingBottom: "2vh"}}>Выбирайте то, что подойдет именно Вам.</SectionTitle>
            <div style={style}>
            {t()}
            </div>
        </Container>
    </Section>);
}

function Tariffs ({title, subtitle, backgroundImage}) {
    //todo: вычленить нужную инфу от тарифов

    return (<>
        <GreetingSection title={title} subtitle={subtitle}
         style={{backgroundImage:`url(${backgroundImage})`}}/>
        <CarListSection/>
    </>
    );
}