import Section from '../Components/Sections'
import Container from '../Components/Container'
import React from 'react'
import Form, { Input } from '../Components/formTools'
import Bold from '../Components/TextTags'
import { Navigate, NavLink, useLocation, useNavigate } from 'react-router-dom'
import { useState, useRef } from 'react'
import API from '../httpclient/axios_client'
import { areValidLoginFields } from '../js/form-validators'
import GoogleSignIn from '../Components/SignInButtons'
import "../css/form.css";


export default function Login ({setUser, user}) {
  const [errors, setErrors] = useState({})
  const [formSummary, setFormSummary] = useState();
  const [requestSent, setRequestSent] = useState(false);
  const formRef = useRef(null);
  const loginRef = useRef(null)
  const passwordRef = useRef(null)
  const location = useLocation();
  const navigator = useNavigate();

   async function handleLogin (event) {
    event.preventDefault();
    if(requestSent || !areValidLoginFields(loginRef.current, passwordRef.current, setErrors)) return;

    setFormSummary("");
    setRequestSent(true);
    const response = await API.login(formRef.current);
    setRequestSent(false);
    if (response.status === 401) {
      const error = response.error;
      if(error.code === 2)
        setFormSummary(error.messages)
    }
    else if(response.status === 400){
      const error = response.error;
      if(error.code === 1)
        setErrors({login: error.errors?.Email[0], password: error.errors?.Password[0]})
    }
    else{
      localStorage.setItem('user', true);
      setUser(true);
      const urlParams = new URLSearchParams(location.search);
      const returnUri = urlParams.get('return_uri');
      debugger;
      if(returnUri)
        navigator(returnUri);
      else
        navigator('/');
    }
  }


  return (
    <Section>
      <Container className='flex-container'>
        <Form ref={formRef} className='center flex-column'>
          <Bold id='loginHeader' className='form-header'>
            Вход
          </Bold>
          <div className='form-error-summary'>{formSummary}</div>
          <Input
            ref={loginRef}
            name="email"
            type="email"
            placeholder='Почта'
            inputErrorMessage={errors?.login}/>
          <Input
            ref={passwordRef}
            name="password"
            type='password'
            placeholder='Пароль'
            inputErrorMessage={errors?.password}/>

          <GoogleSignIn/>
          <div id='formButton' className='form-filed'style={{ marginTop: '15px' }}>
            <button className='button form-button' onClick={handleLogin}>
              Войти
            </button>
            <NavLink className='softblue-regular' to='/registration'>
              Регистрация
            </NavLink>
          </div>
        </Form>
      </Container>
    </Section>
  )
}
