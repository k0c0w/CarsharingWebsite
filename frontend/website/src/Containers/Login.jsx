import Section from "../Components/Sections";
import Container from "../Components/Container";
import { useEffect } from "react";
import Form, { Input } from "../Components/formTools";
import Bold from "../Components/TextTags";
import { NavLink } from "react-router-dom";

function fixHeader() {
    const header = document.getElementsByTagName("header")[0];

    if(header && !header.classList.contains("fixed")) {
        header.classList.add("fixed");
    }
}

export default function Login() {

    useEffect(()=> {fixHeader()}, []);

    return (
    <Section>
        <Container className="flex-container">
            <Form className="center flex-column">
                <Bold className="form-header">Войти</Bold>
                <Input placeholder="Почта"/>
                <Input placeholder="Пароль"/>
                <div className="form-filed">
                    <button className="button">Login</button>
                    <NavLink>Регистрация</NavLink>
                </div>
            </Form>
        </Container>
    </Section>);
}