import Section from "../Components/Sections";
import Container from "../Components/Container";
import Form, { Input } from "../Components/formTools";
import Bold from "../Components/TextTags";
import { NavLink } from "react-router-dom";
import { useState } from "react";
import login from "../axios-request/login"

var props = {
    placeholder:"Почта",
    value:""
}


export default function Login() {
    const [errors, setErrors] = useState({});
    const loginRef = useRef(null);
    const passwordRef = useRef(null);
    const formRef = useRef(null);
    const location = useLocation();

    function handleLogin(event) {
        event.preventDefault();
        if(!areValidLoginFields(loginRef.current, passwordRef.current, setErrors)) return;
        sendForm(formRef.current, location.pathname);
    }

    window.fbAsyncInit = function() {
        FB.init({
          appId      : '{your-app-id}',
          cookie     : true,
          xfbml      : true,
          version    : '{api-version}'
        });
          
        FB.AppEvents.logPageView();   
          
      };
    
    (function(d, s, id)
    {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {return;}
        js = d.createElement(s); js.id = id;
        js.src = "https://connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    } (document, 'script', 'facebook-jssdk') );

    FB.getLoginStatus(function(response) {
        statusChangeCallback(response);
    });

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    function handleLogin () {
        var body = {
            email: email,
            password: password
        };
        console.log(email);
        console.log(password);
        login("https://localhost:7129", body);
    }

    return (
    <Section>
        <Container className="flex-container">
            <Form ref={formRef} className="center flex-column">
                <Bold id="loginHeader" className="form-header">Войти</Bold>
                <Input name="Email" placeholder="Почта" set={(e)=>setEmail(e)} value={email} />
                <Input name="Password" placeholder="Пароль" set={(e)=>setPassword(e)} value={password} />
                <div id="formButton" className="form-filed" >
                    <a className="button form-button" onClick={ () => { handleLogin() } }>Login</a>
                    <NavLink className="softblue-regular" to="/registration">Регистрация</NavLink>
                </div>
            </Form>
        </Container>
    </Section>);
}