import Section from "../Components/Sections";
import Container from "../Components/Container";
import Bold from "../Components/TextTags";
import {Form, MyFormProfileInput, Input } from "../Components/formTools"
import "../css/form.css";
import "../css/text.css";
import API from "../httpclient/axios_client"
import { areValidRegistrationFields } from "../js/form-validators";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import {getVMErrors} from "../js/common_functions.js";


const gap = { columnGap: "100px"};

export function Registration  ()  {
    const formRef = useRef(null);
    const navigator = useNavigate();
    const [errors, setErrors] = useState({});
    const [errorSummary, setErrorSummary] = useState([]);
    const [requestSent, setRequestSent] = useState(false);

    async function handleSend(event) {
        event.preventDefault();
        if(!areValidRegistrationFields(formRef.current) || requestSent) return;
        
        setErrorSummary([]);
        setErrors({});
        setRequestSent(true);
        const result = await API.register(formRef.current);
        setRequestSent(false);
        if(result?.status === 400 && result?.error)
        {
            if(result.error.code === 1)
                setErrors(getVMErrors(result.error.errors));
            else if(result.error.code === 2)
                setErrorSummary(result.error.messages)
        }
        else if (result.status === 500)
            alert("Ошибка сервера.");
        if(result.successed)
            navigator("/login");
    }

    return (
    <Section className="margin-header">
        <Container className="flex-container">
            <Form ref={formRef} className="center flex-column">
                <Bold className="form-header">Регистрация</Bold>
                <div className='form-error-summary'>{errorSummary?.map((x,i) => <text key={i}>{x}<br/></text>)}</div>
                <MyFormProfileInput
                    leftBlock={<RegistrationLeftInputs errors={errors}/>}
                    rightBlock={<RegistrationRightInputs errors={errors}/>}/>
                <div id="formButton" className="form-filed flex-container" style={gap}>
                    <button onClick={handleSend} className="button form-button">Регистрация</button>
                    <label className="form-accept">
                        <input id="data-processing-agreement" name="accept" type="checkbox"/>
                        <p className="form-error">{errors.accept}</p>
                        <div className="form-accept_description">Согласие на обработку персональных данных</div>
                    </label>
                </div>  
            </Form>
        </Container>
    </Section>);
};

const RegistrationLeftInputs = ({errors}) => {

    return <>
        <Input inputErrorMessage={errors?.email} required type="email" id="email" name="email" placeholder="Почта"/>
        <Input inputErrorMessage={errors?.password} required type="password" id="password" name="password" placeholder="Пароль"/>
        <Input inputErrorMessage={errors?.password_repeat} required type="password" id="password_repeat" placeholder="Повторите пароль"/>
    </>
}

const RegistrationRightInputs = ({errors}) => (
    <>
        <Input inputErrorMessage={errors?.name} required type="text" id="name" name="name" placeholder="Имя"/>
        <Input inputErrorMessage={errors?.surname} required type="text" id="surname" name="surname" placeholder="Фамилия"/>
        <Input inputErrorMessage={errors?.birthdate} required type="date" id="birthdate" name="birthdate" placeholder="Дата рождения"/>
    </>
)
