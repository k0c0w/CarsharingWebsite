import Section from "../Components/Sections";
import Container from "../Components/Container";
import Form, { Input } from "../Components/formTools";
import Bold from "../Components/TextTags";
import { NavLink } from "react-router-dom";
import { useState, useRef } from "react";
import login from "../axios-request/login"
import { areValidLoginFields } from "../js/form-validators";


export default function Login() {
    const [errors, setErrors] = useState({});
    const loginRef = useRef(null);
    const passwordRef = useRef(null);
    const formRef = useRef(null);

    // window.fbAsyncInit = function() {
    //     FB.init({
    //       appId      : '{your-app-id}',
    //       cookie     : true,
    //       xfbml      : true,
    //       version    : '{api-version}'
    //     });
          
    //     FB.AppEvents.logPageView();   
          
    //   };
    
    // (function(d, s, id)
    // {
    //     var js, fjs = d.getElementsByTagName(s)[0];
    //     if (d.getElementById(id)) {return;}
    //     js = d.createElement(s); js.id = id;
    //     js.src = "https://connect.facebook.net/en_US/sdk.js";
    //     fjs.parentNode.insertBefore(js, fjs);
    // } (document, 'script', 'facebook-jssdk') );

    // FB.getLoginStatus(function(response) {
    //     statusChangeCallback(response);
    // });

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    function handleLogin (event) {
        event.preventDefault();
        if(!areValidLoginFields(loginRef.current, passwordRef.current, setErrors)) return;
        const body = {
            email: email,
            password: password
        };
        login(body);
    }

    return (
    <Section>
        <Container className="flex-container">
            <Form ref={formRef} className="center flex-column">
                <Bold id="loginHeader" className="form-header">Войти</Bold>
                <Input ref={loginRef} name="Email" placeholder="Почта" set={(e)=>setEmail(e)} value={email} />
                <Input ref={passwordRef} name="Password" placeholder="Пароль" set={(e)=>setPassword(e)} value={password} />
                <div id="formButton" className="form-filed" >
                    <a className="button form-button" onClick={ (e) => { handleLogin(e) } }>Login</a>
                    <NavLink className="softblue-regular" to="/registration">Регистрация</NavLink>
                </div>
            </Form>
        </Container>
    </Section>);
}