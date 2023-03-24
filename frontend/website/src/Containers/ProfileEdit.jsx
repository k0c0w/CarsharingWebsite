import { NavLink } from "react-router-dom";
import {Form, MyFormProfileInput, Input} from "../Components/formTools";
import Bold from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import Section from "../Components/Sections";
import Container from "../Components/Container";

import "../css/profile.css";

const gap = { columnGap: "100px"};

export function ProfileChangePassword () {
return <>
<DocumentTitle>Смена пароля</DocumentTitle>
<Section>
    <Form className="center flex-column">
        <Bold className="form-header">Смена пароля</Bold>
        <Input name="password" placeholder="новый пароль"/>
        <Input placeholder="повторите пароль"/>
        <button className="button form-button">Сохранить</button>
    </Form>
</Section>
</>
};

export default function ProfileEdit () {
    return <>
    <DocumentTitle>Редактирование профиля</DocumentTitle>
    <Section className="margin-header">
        <Container className="flex-container">
            <Form className="center flex-column">
                <div className="flex-container profile-edit-form-header">
                    <Bold className="form-header">Василий Пупкин</Bold>
                    <NavLink to="password" className="change-password">[сменить пароль]</NavLink>
                </div>
                <MyFormProfileInput
                    leftBlock={<><Input name="email" placeholder="Почта" value="example@mail.ru"/><Input name="passport" placeholder="Паспорт"/><Input placeholder="Водительское удостоверение"/></>}
                    rightBlock={<><Input name="name" placeholder="Имя" value="Василий"/><Input name="surname" placeholder="Фамилия" value="Пупкин"/><Input name="age" placeholder="Возраст" value="25"/></>}/>
                <div id="formButton" className="form-filed flex-container" style={gap}>
                    <button className="button form-button delete">Удалить аккаунт</button>
                    <button className="button form-button">Сохранить</button>
                </div>  
            </Form>
        </Container>
    </Section>
    </>;
}