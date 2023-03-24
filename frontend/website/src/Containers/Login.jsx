import Section from "../Components/Sections";
import Container from "../Components/Container";
import Form, { Input } from "../Components/formTools";
import Bold from "../Components/TextTags";
import { NavLink } from "react-router-dom";

export default function Login() {

    return (
    <Section>
        <Container className="flex-container">
            <Form className="center flex-column">
                <Bold id="loginHeader" className="form-header">Войти</Bold>
                <Input placeholder="Почта"/>
                <Input placeholder="Пароль"/>
                <div id="formButton" className="form-filed">
                    <button className="button form-button">Login</button>
                    <NavLink className="softblue-regular" to="/registration">Регистрация</NavLink>
                </div>
            </Form>
        </Container>
    </Section>);
}