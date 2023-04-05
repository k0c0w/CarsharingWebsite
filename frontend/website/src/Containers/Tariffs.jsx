import { useParams, Navigate } from "react-router-dom";
import { CardHolder } from "../Components/Card";
import Container from "../Components/Container";
import Section, { GreetingSection, SectionTitle } from "../Components/Sections";

const style = {     display: "flex",
    justifyContent: "space-evenly",
    alignItems: "center",
    minHeight: "fit-content", flexWrap: "wrap", rowGap: "50px", columnGap: "15px"}

export default function BeforeTariffs ({tariffsData}) {
    const {tariffName} = useParams();
    const tariff = tariffsData.find(x => x.name == tariffName);
    return tariff ? <Tariffs title={tariff.name} subtitle={tariff.description} backgroundImage={tariff.image_url}/> 
    : <Navigate to="/notFound"/>;
}

export function CarListSection () {
    return (
    <Section  style={{backgroundColor:"#DEF0F0"}}>
        <Container>
            <SectionTitle style={{paddingBottom: "2vh"}}>Выбирайте то, что подойдет именно Вам.</SectionTitle>
            <div style={style}>
                <CardHolder/>
                <CardHolder/>
                <CardHolder/>
            </div>
        </Container>
    </Section>);
}

function Tariffs ({title, subtitle, backgroundImage}) {
    return (<>
        <GreetingSection title={title} subtitle={subtitle}
         style={{backgroundImage:`url(${backgroundImage})`}}/>
        <CarListSection/>
    </>
    );
}