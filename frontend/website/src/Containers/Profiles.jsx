import { NavLink } from "react-router-dom";
import {Form, MyFormProfileInput, Input} from "../Components/formTools";
import Bold, {Dim} from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import Section from "../Components/Sections";
import Container from "../Components/Container";

import "../css/profile.css";
import Figure from "../Components/Figure";

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

export function ProfileEdit () {
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


function openButtonPressed(event){
    const classList = event.target.classList;
    event.target.innerText = event.target.innerText === "Открыть" ? "Закрыть" : "Открыть";
    classList.toggle("open-button-active");
    classList.toggle("open-button-nonactive");
}

const CarList = () => (
    <>
        <Bold>Мои машины:</Bold>
        <ul className="profile-carlist-list">
            <li>
                <Figure figureName="rented-car" className="border-solid">
                    <img className="rented-car-image" src="https://mobility.hyundai.ru/dist/images/cars/sonata2.png"/>
                    <div className="rented-car-info">
                        <Bold>Sonata</Bold>
                        <div className="rented-car-info__sign">H182OP 116</div>
                    </div>
                    <button id="2" className="open-button" onClick={openButtonPressed}>Открыть</button>
                </Figure>
            </li>
        </ul>
    </>
);

const ProfileInfo = () => (
    <>
        <NavLink to="edit" className="flex-container">
            <Bold>Василий Пупкин</Bold>
            <svg className="profile-pen" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><defs><style>.cls-1{"fill:#101820;"}</style></defs><title/><g data-name="Layer 42" id="Layer_42"><path class="cls-1" d="M2,29a1,1,0,0,1-1-1.11l.77-7a1,1,0,0,1,.29-.59L18.42,3.94a3.2,3.2,0,0,1,4.53,0l3.11,3.11a3.2,3.2,0,0,1,0,4.53L9.71,27.93a1,1,0,0,1-.59.29l-7,.77Zm7-1.78H9ZM3.73,21.45l-.6,5.42,5.42-.6,16.1-16.1a1.2,1.2,0,0,0,0-1.7L21.53,5.35a1.2,1.2,0,0,0-1.7,0Z"/><path class="cls-1" d="M23,14.21a1,1,0,0,1-.71-.29L16.08,7.69A1,1,0,0,1,17.5,6.27l6.23,6.23a1,1,0,0,1,0,1.42A1,1,0,0,1,23,14.21Z"/><rect class="cls-1" height="2" transform="translate(-8.31 14.13) rotate(-45)" width="11.01" x="7.39" y="16.1"/><path class="cls-1" d="M30,29H14a1,1,0,0,1,0-2H30a1,1,0,0,1,0,2Z"/></g></svg>
        </NavLink>
        <Dim className="profile-account-element">example1@mail.ru</Dim>
        <div className="profile-account border-solid">
            <Bold className="profile-account-element">Остаток:</Bold>
            <div className="profile-account-element balance">2889.65</div>
            <NavLink className="profile-account-element"><Dim style={{color:"#D9D9D9"}}>Пополнить</Dim></NavLink>
        </div>
    </>
);


export default function Profile() {
    return <>
    <Section style={{backgroundColor:"#DEF0F0"}}>
       <Container className="profileContainer-padding">
            <div className="profile-holder">
                <Figure figureName="info-holder" className="profile-carlist-figure">
                    <CarList/>
                </Figure>
                <Figure figureName="info-holder" className="profile-info-figure">
                    <ProfileInfo/>
                </Figure>
            </div>
        </Container>
    </Section>
    </>
}