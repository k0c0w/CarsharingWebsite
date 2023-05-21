import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { CardHolder } from "../Components/Card";
import Container from "../Components/Container";
import Section, { GreetingSection, SectionTitle } from "../Components/Sections";
import { useEffect } from "react";
import  API  from "../httpclient/axios_client";
import Bold from "../Components/TextTags";

const style = {     display: "flex",
    justifyContent: "space-evenly",
    alignItems: "center",
    minHeight: "fit-content", flexWrap: "wrap", rowGap: "50px", columnGap: "15px"}

export default function BeforeTariffs () {
    const [tariff, setTariff] = useState(null);
    const {tariffId} = useParams();
    const navigator = useNavigate();

    useEffect(() => {
        async function fetchData() {
            const response = await API.tariffs(tariffId);
            if(response.successed)
                setTariff(response.data);
            else if(response.status === 404)
                navigator('/notFound');
        }

        fetchData();
      }, []);

    return <Tariffs id={tariffId} title={tariff?.name} price={tariff?.price}
    subtitle={tariff?.description} backgroundImage={tariff?.image_url}/>;
}

export function CarListSection ({tariffId, price}) {
    const [cars, setCars] = useState([]);
    const [loaded, setLoaded] = useState(false);

    useEffect(() => {
        async function fetchData(){
            const response = await API.car_prototypes(tariffId);
            if(response.successed) {
                setCars(response.data);
                setLoaded(true);
            }
        }
        fetchData();
    }, []);

    return (
    <Section  style={{backgroundColor:"#DEF0F0"}}>
        <Container>
            <SectionTitle style={{paddingBottom: "2vh"}}>Выбирайте то, что подойдет именно Вам.</SectionTitle>
            <div style={style}>
            {!loaded && <Bold>Загрузка...</Bold>}
            {loaded && cars.length == 0 && <Bold>К сожалению, нет доступных машин.</Bold>}
            {cars.map((x, i) =>{

                let description = x.description?.substring(0, 64);
                if(x?.description.length > 64)
                    description = `${description}...`;
                return <CardHolder key={i} brand={x.brand} model={x.model} price={price}
                description={description} modelId={x.id}
                img={x.image_url}/>;
            })}
            </div>
        </Container>
    </Section>);
}

function Tariffs ({id, title, subtitle, backgroundImage, price}) {
    return (<>
        <GreetingSection title={title} subtitle={subtitle}
         style={{backgroundImage:`url(${backgroundImage})`}}/>
        <CarListSection tariffId={id} price={price}/>
    </>
    );
}