import React from "react";
import Section, { SectionTitle } from "../Sections";
import Container from "../Container";
import "../../css/rectangle-link.css";
import "../../css/index-tariffs.css";
import HorizontalArrow from "../HorizontalArrow";
import { NavLink } from "react-router-dom";
import Bold from "../TextTags";


const TarrifHolder = (props) => (
  <NavLink to={props.to} className="flex-container rectangle-container-item_link tariff-card">
      <div className="flex-container" style={{overflow:"hidden"}}>
          <h3 className="rectangle-container-item_head">{props.name}</h3>
      </div>
      <HorizontalArrow  className="arrow"/>
  </NavLink>
);

export const IndexTariffs =  React.forwardRef((props, ref) => (
  <Section ref={ref}>
      <Container>
        <SectionTitle subtitle="Ваш личный автомобиль">Тарифы</SectionTitle>
        <div className="rectangle-container">
            {props.tariffs.length == 0 && <Bold>Нет доступных тарифов</Bold>}
            <ul className="tariff-list">
              {props.tariffs.map((tariff, i) => 
              <li className="rectangle-container-item" color-type={i % 3 + 1} key={i}>
                <TarrifHolder to={`/tariffs/${tariff.id}`} name={tariff.name}/></li>)}
            </ul>
        </div>
      </Container>
  </Section>
));
IndexTariffs.displayName = "Tariffs"
export default IndexTariffs;
