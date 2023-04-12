import Section from "../Components/Sections";
import Container from "../Components/Container";
import Form, { Input } from "../Components/formTools";
import Bold from "../Components/TextTags";
import { NavLink } from "react-router-dom";
import { useRef, useState } from "react";
import { areValidLoginFields } from "../js/form-validators";
import axiosInstance from "../httpclient/axios_client";

function sendFields(formRef){
    if(formRef){
        const data = Array.from(formRef.current.elements)
        .filter((element) => element.name)
        .reduce(
          (obj, input) => Object.assign(obj, { [input.name]: input.value }),
          {}
        );
        axiosInstance.post('/login', data);
    }
}


export default function Login() {
    const [errors, setErrors] = useState({});
    const loginRef = useRef(null);
    const passwordRef = useRef(null);
    const formRef = useRef(null);

    function handleLogin(event) {
        event.preventDefault();
        if(!areValidLoginFields(loginRef.current, passwordRef.current, setErrors)) return;
        sendFields(formRef);
    }

    return (
    <Section>
        <Container className="flex-container">
            <Form ref={formRef} className="center flex-column">
                <Bold id="loginHeader" className="form-header">Войти</Bold>
                <Input ref={loginRef} type="email" placeholder="Почта" required={true}
                    name="login" inputErrorMessage={errors["login"]} className="loginField"/>
                <Input ref={passwordRef} type="password" placeholder="Пароль" required={true}
                    name="password" inputErrorMessage={errors["password"]}/>
                <div id="formButton">
                    <button className="button form-button" onClick={handleLogin}>Login</button>
                    <NavLink className="softblue-regular" to="/registration">Регистрация</NavLink>
                </div>
            </Form>
        </Container>
    </Section>);
}