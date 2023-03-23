import React from "react";
import Section, { SectionTitle } from "../Sections";
import Container from "../Container";
import Figure from "../../Components/Figure";

import "../../css/rectangle-link.css";
import "../../css/index-tariffs.css";
import HorizontalArrow from "../HorizontalArrow";
import { NavLink } from "react-router-dom";


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
            <ul className="tariff-list">
              <li className="rectangle-container-item" color-type="1"><TarrifHolder name="Everyday"/></li>
              <li className="rectangle-container-item" color-type="2"><TarrifHolder name="Everyday"/></li>
              <li className="rectangle-container-item" color-type="3"><TarrifHolder name="Everyday"/></li>
            </ul>
        </div>
      </Container>
    </Section>
));
export default IndexTariffs;