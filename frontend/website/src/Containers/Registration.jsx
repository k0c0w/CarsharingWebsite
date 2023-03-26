import Section from "../Components/Sections";
import Container from "../Components/Container";
import Bold from "../Components/TextTags";
import {Form, MyFormProfileInput, Input } from "../Components/formTools"


import "../css/text.css";


const gap = { columnGap: "100px"};

export function Registration  ()  {

    return (
    <Section className="margin-header">
        <Container className="flex-container">
            <Form className="center flex-column">
                <Bold className="form-header">Регистрация</Bold>
                <MyFormProfileInput
                    leftBlock={<><Input name="email" placeholder="Почта"/><Input name="password" placeholder="Пароль"/><Input placeholder="Повторите пароль"/></>}
                    rightBlock={<><Input name="name" placeholder="Имя"/><Input name="surname" placeholder="Фамилия"/><Input name="age" placeholder="Возраст"/></>}/>
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