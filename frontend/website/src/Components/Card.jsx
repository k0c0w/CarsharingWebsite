import { NavLink } from "react-router-dom";
import "../css/card.css";
import Figure from "./Figure";
import { Dim } from "./TextTags";

export const CardHolder = ({brand, model, price, description, modelId, img}) => (
    <Figure figureName="card">
        <div className="image-holder"><img src={img} alt={model}/></div>
        <div className="card-content">
            <div>
                <h1>{model}</h1>
                <h3 style={{margin: 0}}>{brand}</h3>
                <p>{price} р/мес</p>
            </div>
            <Dim>{description}</Dim>
        </div>
        <NavLink to={`rent/${modelId}`}>Аренда</NavLink>
    </Figure>
) 