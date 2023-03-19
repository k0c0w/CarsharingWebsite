import { useEffect } from "react";
import Section from "../Components/Sections";
import Container from "../Components/Container";
import Bold from "../Components/TextTags";
import {Form, Input} from "../Components/formTools"
import { NavLink } from "react-router-dom";

function fixHeader() {
    const header = document.getElementsByTagName("header")[0];

    if(header && !header.classList.contains("fixed")) {
        header.classList.add("fixed");
    }
}

const gap = { columnGap: "100px"};

export function Registration  ()  {
    useEffect(()=> {fixHeader()}, []);

    return (
    <Section>
        <Container className="flex-container">
            <Form className="center flex-column">
                <Bold className="form-header">Регистрация</Bold>
                <div className="flex-container" style={gap}>
                    <div className="flex-container flex-column" style={{order: 0}}>
                        <Input name="email" placeholder="Почта"/>
                        <Input name="password" placeholder="Пароль"/>
                        <Input placeholder="Повторите пароль"/>
                    </div>
                    <div className="flex-container flex-column" style={{order: 1}}>
                        <Input name="name" placeholder="Имя"/>
                        <Input name="surname" placeholder="Фамилия"/>
                        <Input name="age" placeholder="Возраст"/>
                    </div>
                </div>
                <div className="form-filed flex-container" style={gap}>
                    <button className="button">Регистрация</button>
                    <label>
                        <input type="checkbox"/>
                        <div>Согласие на обработку персональнвх данных</div>
                    </label>
                </div>  
            </Form>
        </Container>
    </Section>);
};