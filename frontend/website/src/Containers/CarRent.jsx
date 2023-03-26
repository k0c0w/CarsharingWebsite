import Section from "../Components/Sections";
import { Dim } from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import MyMap from "../Components/Map";
import "../css/car-rent.css";

export default function CarRent() {
    return <>
    <DocumentTitle>Sonata</DocumentTitle>
    <div style={{height:"96px"}}></div>
    <Section className="renting">
        <div className="renting-car-info flex-container">
            <div className="renting-car-info__tariff"><span style={{marginLeft:0}}>Travel</span></div>
            <div className="renting-car-info__img"><img src="https://mobility.hyundai.ru/dist/images/cars/sonata2.png" alt="" /></div>
            <div style={{width:"100%", textAlign: "center", padding: "0 15px"}}>
                <div>
                <h1 className="renting-car-info__car">Sonata</h1>
                <div className="renting-car-info__car-tarrif"><span>1050 р./мес.</span><span>500 км/день</span></div>
                <Dim style={{textAlign: "left", paddingTop:"20px"}}>
                    Таким образом укрепление и развитие структуры требуют от нас анализа систем массового участия. Товарищи! постоянное информационно-пропагандистское обеспечение нашей деятельности способствует подготовки и реализации форм развития. Не следует, однако забывать, что укрепление и развитие структуры в значительной степени обуславливает создание систем массового участия.
                </Dim>
                </div>
            </div>
        </div>
        <div className="renting-sidebar">
            <MyMap className="renting-sidebar-map"/>
            <div className="renting-sidebar-period">
                <div className="flex-container flex-column">
                    <div className="renting-sidebar-period__holder">
                        <span>Преиод:</span>
                        <span>с</span>
                        <input type="date"/>
                        <span>по</span>
                        <input type="date"/>
                    </div>
                    <div className="renting-sidebar-period__error">Неверная дата!</div>
                    <Dim>Расчетная стоимость: 10000р</Dim>
                    <button className="button">Аренда</button>
                </div>
            </div>
        </div>
    </Section>
    </>;
}