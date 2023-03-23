import { useEffect } from "react";
import { NavLink } from "react-router-dom";
import {Form, MyFormProfileInput, Input} from "../Components/formTools";
import Bold from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import { fixHeader } from "../hooks/common_functions";

const gap = { columnGap: "100px"};

export default function ProfileEdit () {
    useEffect(() => {fixHeader()}, []);
    return <>
    <DocumentTitle>Редактирование профиля</DocumentTitle>
    <Form className="center flex-column">
        <div className="flex-container profile-edit-form-header">
            <Bold className="form-header">Василий Пупкин</Bold>
            <NavLink to="password">[сменить пароль]</NavLink>
        </div>
        <MyFormProfileInput
            leftBlock={<><Input name="email" placeholder="Почта" value="example@mail.ru"/><Input name="passport" placeholder="Паспорт"/><Input placeholder="Водительское удостоверение"/></>}
            rightBlock={<><Input name="name" placeholder="Имя" value="Василий"/><Input name="surname" placeholder="Фамилия" value="Пупкин"/><Input name="age" placeholder="Возраст" value="25"/></>}/>
        <div id="formButton" className="form-filed flex-container" style={gap}>
            <button className="button form-button" style={{backgroundColor:"#F85B5B"}}>Удалить аккаунт</button>
            <button className="button form-button">Регистрация</button>
        </div>  
    </Form></>;
}