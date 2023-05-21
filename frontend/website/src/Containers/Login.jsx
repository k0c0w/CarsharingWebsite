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
  const navigator = useNavigate();
  const location = useLocation();

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
      if(returnUri)
        navigator(returnUri);
      else
        navigator('/');
    }
  }


  return user ? (<Navigate to ='/'/>) : (
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

          <GoogleSignIn
            redirect_uri='https://localhost:7129/api/account/google-external-auth-callback/'
            scope='https://www.googleapis.com/auth/userinfo.email'
            client_id='930943899094-n86i2ipn8jb3j51aj9d8k2tcojd89ilb.apps.googleusercontent.com'
          />
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
