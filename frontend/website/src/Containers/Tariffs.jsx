import { CardHolder } from "../Components/Card";
import Container from "../Components/Container";
import Section, { GreetingSection, SectionTitle } from "../Components/Sections";

const style = {     display: "flex",
    justifyContent: "space-evenly",
    alignItems: "center",
    minHeight: "fit-content", flexWrap: "wrap", rowGap: "50px", columnGap: "15px"}

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

export default function Tariffs () {
    return (<>
        <GreetingSection title="Everyday" subtitle="Дай деняк сюда"
         backgroundImageSource="../sources/greeting/tariff_name.jpg"/>
        <CarListSection/>
    </>
    );
}