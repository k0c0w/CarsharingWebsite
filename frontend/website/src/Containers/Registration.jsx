import { useEffect } from "react";
import Section from "../Components/Sections";
import Container from "../Components/Container";
import Bold from "../Components/TextTags";
import {Form, MyFormProfileInput} from "../Components/formTools"
import { fixHeader } from "../hooks/common_functions";

import "../css/text.css";


const gap = { columnGap: "100px"};

export function Registration  ()  {
    useEffect(()=> {fixHeader()}, []);

    return (
    <Section>
        <Container className="flex-container">
            <Form className="center flex-column">
                <Bold className="form-header">Регистрация</Bold>
                <MyFormProfileInput/>
                <div id="formButton" className="form-filed flex-container" style={gap}>
                    <button className="button form-button">Регистрация</button>
                    <label className="form-accept">
                        <input name="accept" type="checkbox"/>
                        <div className="form-accept_description">Согласие на обработку персональных данных</div>
                    </label>
                </div>  
            </Form>
        </Container>
    </Section>);
};