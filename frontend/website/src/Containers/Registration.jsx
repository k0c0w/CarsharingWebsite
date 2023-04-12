import Section from "../Components/Sections";
import Container from "../Components/Container";
import Bold from "../Components/TextTags";
import {Form, MyFormProfileInput, Input } from "../Components/formTools"


import "../css/text.css";
import { areValidRegistrationFields } from "../js/form-validators";
import { useRef } from "react";
import axiosInstance from "../httpclient/axios_client";




//todo: send form вынести в отдельную общедоступную функцию
function sendForm(form, endpointUrl, additionalData) {
    const finalFormEndpoint = endpointUrl || form.action;
    const data = Array.from(form.elements)
        .filter((element) => element.name)
        .reduce(
          (obj, input) => Object.assign(obj, { [input.name]: input.value }),
          {}
        );

    if (additionalData) {
        Object.assign(data, additionalData);
    }
    console.log(data);
    return axiosInstance.post(finalFormEndpoint, data)
}


const gap = { columnGap: "100px"};

export function Registration  ()  {
    const formRef = useRef(null);

    function handleSend(event) {
        event.preventDefault();
        if(areValidRegistrationFields(formRef.current))
            sendForm(formRef.current, "/register")
            .then(r => alert("done"))
            .catch(err => alert("error post"));
    }

    return (
    <Section className="margin-header">
        <Container className="flex-container">
            <Form ref={formRef} className="center flex-column">
                <Bold className="form-header">Регистрация</Bold>
                <MyFormProfileInput
                    leftBlock={<RegistrationLeftInputs/>}
                    rightBlock={<RegistrationRightInputs/>}/>
                <div id="formButton" className="form-filed flex-container" style={gap}>
                    <button onClick={handleSend} className="button form-button">Регистрация</button>
                    <label className="form-accept">
                        <input id="data-processing-agreement" name="accept" type="checkbox"/>
                        <div className="form-accept_description">Согласие на обработку персональных данных</div>
                    </label>
                </div>  
            </Form>
        </Container>
    </Section>);
};

const RegistrationLeftInputs = () => ( <>
        <Input required id="email" name="email" placeholder="Почта"/>
        <Input required id="password" name="password" placeholder="Пароль"/>
        <Input required id="password_repeat" placeholder="Повторите пароль"/></>
)

const RegistrationRightInputs = () => (
    <><Input required id="name" name="name" placeholder="Имя"/>
    <Input required id="surname" name="surname" placeholder="Фамилия"/>
    <Input required id="age" type="number" name="age" placeholder="Возраст"/></>
)

