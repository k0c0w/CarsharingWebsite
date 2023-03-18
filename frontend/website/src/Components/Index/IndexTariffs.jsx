import React from "react";
import Section, { SectionTitle } from "../Sections";
import Container from "../Container";
import Figure from "../../Components/Figure";

import "../../css/index-tariffs.css";

const TariffHolder = (props) => (<Figure className="tariff-holder"><p>{props.children}</p></Figure>);

const tariffsHolder = {
    justifyContent: "space-around",
    flexWrap:"wrap",
}

export const IndexTariffs =  React.forwardRef((props, ref) => (
    <Section ref={ref}>
      <Container>
        <SectionTitle subtitle="Ваш личный автомобиль">Тарифы</SectionTitle>
        <div className="tariffs-list-holder" id="tariffs">
            <li style={tariffsHolder} className="flex-container">
                <TariffHolder>Freedom</TariffHolder>
                <TariffHolder>Travel</TariffHolder>
                <TariffHolder>Everyday</TariffHolder>
            </li>
        </div>
      </Container>
    </Section>
));
export default IndexTariffs;