import { NavLink } from "react-router-dom";
import "../css/card.css";
import Figure from "./Figure";
import { Dim } from "./TextTags";

export const CardHolder = (props) => (
    <Figure figureName="card">
        <div className="image-holder"><img src="https://mobility.hyundai.ru/dist/images/cars/sonata2.png" alt="car"/></div>
        <div className="card-content">
            <div>
                <h1>SONATA</h1>
                <p>1050р/мес</p>
            </div>
            <Dim>Some description of car in few words. features.</Dim>
        </div>
        <NavLink to="rent/sonata">Аренда</NavLink>
    </Figure>
) 