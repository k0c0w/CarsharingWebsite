import { NavLink, useLocation } from "react-router-dom";
import {Form, MyFormProfileInput, Input} from "../Components/formTools";
import Bold, {Dim} from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import Section from "../Components/Sections";
import Container from "../Components/Container";

import "../css/profile.css";
import Figure from "../Components/Figure";
import { useEffect, useRef, useState } from "react";
import { areValidProfileEdit, isValidPasswordChange } from "../js/form-validators";
import { sendForm } from "../js/common-functions";
import axiosInstance from "../httpclient/axios_client";

const gap = { columnGap: "100px"};

function axiosData(endpoint, setter) {
    axiosInstance.get(endpoint)
        .then(r => setter(r.data))
        .catch(err => alert("Ошибка при загрузке профиля. Проверьте интернет соединение."))
}

export function ProfileChangePassword () {
    const formRef = useRef(null);
    const location = useLocation();
    function handleSend(event) {
        event.preventDefault();
        if(isValidPasswordChange(formRef.current))
            sendForm(formRef.current, location.pathname);
    }

    return <>
        <DocumentTitle>Смена пароля</DocumentTitle>
        <Section>
            <Form ref={formRef} className="center flex-column">
                <Bold className="form-header">Смена пароля</Bold>
                <Input required id="password" name="password" placeholder="новый пароль"/>
                <Input required id="password_repeat" placeholder="повторите пароль"/>
                <button className="button form-button" onClick={handleSend}>Сохранить</button>
            </Form>
        </Section>
    </>
};

const LeftProfileEdit = ({personalInfo}) => (<>
    <Input id="email" name="email" placeholder="Почта" value={personalInfo?.email}/>
    <Input id="passport" name="passport" placeholder="Паспорт" value={personalInfo?.passport}/>
    <Input id="license" placeholder="Водительское удостоверение" value={personalInfo.driver_license} /></>);

const RightProfileEdit = ({personalInfo}) => (<>
    <Input id="name" name="name" placeholder="Имя" value={personalInfo?.name}/>
    <Input id="surname" name="surname" placeholder="Фамилия" value={personalInfo?.surname}/>
    <Input id="age" name="age" placeholder="Возраст" value={personalInfo?.age}/></>);

export function ProfileEdit () {
    const formRef = useRef(null);
    const location = useLocation();
    const [personalInfo, setPersonalInfo] = useState({});

    useEffect(() => {
        axiosData("/profile/personalInfo", setPersonalInfo);
    },[]);

    function handleSend(event) {
        event.preventDefault();
        if(areValidProfileEdit(formRef.current))
            sendForm(formRef.current, location.pathname);
    }

    return <>
    <DocumentTitle>Редактирование профиля</DocumentTitle>
    <Section className="margin-header">
        <Container className="flex-container">
            <Form ref={formRef} className="center flex-column">
                <div className="flex-container profile-edit-form-header">
                    <Bold className="form-header">{`${personalInfo?.name} ${personalInfo?.surname}`}</Bold>
                    <NavLink to="password" className="change-password">[сменить пароль]</NavLink>
                </div>
                <MyFormProfileInput
                    leftBlock={<LeftProfileEdit personalInfo={personalInfo}/>}
                    rightBlock={<RightProfileEdit personalInfo={personalInfo}/>}/>
                <div id="formButton" className="form-filed flex-container" style={gap}>
                    <button className="button form-button delete">Удалить аккаунт</button>
                    <button onClick={handleSend} className="button form-button">Сохранить</button>
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

const CarList = ({cars}) => (
    <>
        <Bold>Мои машины:</Bold>
        <ul className="profile-carlist-list">
            {cars.map(car => 
            <li>
                <Figure figureName="rented-car" className="border-solid">
                    <img className="rented-car-image" src="https://mobility.hyundai.ru/dist/images/cars/sonata2.png"/>
                    <div className="rented-car-info">
                        <Bold>{car.model}</Bold>
                        <div className="rented-car-info__sign">{car.license_plate}</div>
                    </div>
                    <button id="2" className="open-button open-button-nonactive" onClick={openButtonPressed}>Открыть</button>
                </Figure>
            </li>
            )}
        </ul>
    </>
);

const ProfileInfo = ({userInfo}) => (
    <>
        <NavLink to="edit" className="flex-container">
            <Bold>{userInfo?.full_name}</Bold>
            <svg className="profile-pen" viewBox="0 0 32 32" xmlns="http://www.w3.org/2000/svg"><defs><style>.cls-1{"fill:#101820;"}</style></defs><title/><g data-name="Layer 42" id="Layer_42"><path d="M2,29a1,1,0,0,1-1-1.11l.77-7a1,1,0,0,1,.29-.59L18.42,3.94a3.2,3.2,0,0,1,4.53,0l3.11,3.11a3.2,3.2,0,0,1,0,4.53L9.71,27.93a1,1,0,0,1-.59.29l-7,.77Zm7-1.78H9ZM3.73,21.45l-.6,5.42,5.42-.6,16.1-16.1a1.2,1.2,0,0,0,0-1.7L21.53,5.35a1.2,1.2,0,0,0-1.7,0Z"/><path d="M23,14.21a1,1,0,0,1-.71-.29L16.08,7.69A1,1,0,0,1,17.5,6.27l6.23,6.23a1,1,0,0,1,0,1.42A1,1,0,0,1,23,14.21Z"/><rect height="2" transform="translate(-8.31 14.13) rotate(-45)" width="11.01" x="7.39" y="16.1"/><path d="M30,29H14a1,1,0,0,1,0-2H30a1,1,0,0,1,0,2Z"/></g></svg>
        </NavLink>
        <Dim className="profile-account-element">{userInfo?.email}</Dim>
        <div className="profile-account border-solid">
            <Bold className="profile-account-element">Остаток:</Bold>
            <div className="profile-account-element balance">{userInfo?.balance}</div>
            <NavLink className="profile-account-element"><Dim style={{color:"#D9D9D9"}}>Пополнить</Dim></NavLink>
        </div>
    </>
);


export default function Profile() {
    const [profileInfo, setProfileInfo] = useState({rented_cars:[], user_info: {}});

    useEffect(() => {
        axiosData("/profile", setProfileInfo);
    }, []);

    return <>
    <Section style={{backgroundColor:"#DEF0F0"}}>
       <Container className="profileContainer-padding">
            <div className="profile-holder">
                <Figure figureName="info-holder" className="profile-carlist-figure">
                    <CarList cars={profileInfo.rented_cars}/>
                </Figure>
                <Figure figureName="info-holder" className="profile-info-figure">
                    <ProfileInfo userInfo={profileInfo.user_info}/>
                </Figure>
            </div>
        </Container>
    </Section>
    </>
}