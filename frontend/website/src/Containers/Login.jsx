import Section from '../Components/Sections'
import Container from '../Components/Container'
import React from 'react'
import Form, { Input } from '../Components/formTools'
import Bold from '../Components/TextTags'
import { NavLink, useNavigate } from 'react-router-dom'
import { useState, useRef } from 'react'
import API from '../httpclient/axios_client'
import { areValidLoginFields } from '../js/form-validators'
import GoogleSignIn from '../Components/SignInButtons'
import "../css/form.css";

const getMessages = (errors, property) => errors.find(x => x.key==property)?.messages;

export default function Login () {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [errors, setErrors] = useState({})
  const [formSummary, setFormSummary] = useState();
  const [requestSent, setRequestSent] = useState(false);
  const loginRef = useRef(null)
  const passwordRef = useRef(null)
  const navigator = useNavigate();

   async function handleLogin (event) {
    event.preventDefault();
    if(requestSent || !areValidLoginFields(loginRef.current, passwordRef.current, setErrors)) return;

    setFormSummary("");
    setRequestSent(true);
    const response = await API.login({ email: loginRef?.current?.value, password: passwordRef?.current?.value})
    setRequestSent(false);
    if (response.status === 401) {
      const error = response.error;
      if(error.code === 2)
        setFormSummary(error.messages)
    }
    else if(response.status === 400){
      const error = response.error;
      debugger;
      if(error.code === 1)
        setErrors({login: getMessages(error.errors, "Email")[0], password: getMessages(error.errors, "Password")[0]})
    }
    else{
      navigator('/');
    }
  }

  return (
    <Section>
      <Container className='flex-container'>
        <Form className='center flex-column'>
          <Bold id='loginHeader' className='form-header'>
            Вход
          </Bold>
          <div className='form-error-summary'>{formSummary}</div>
          <Input
            ref={loginRef}
            type="email"
            placeholder='Почта'
            set={e => setEmail(e)}
            value={email}
            inputErrorMessage={errors?.login}/>
          <Input
            ref={passwordRef}
            type='password'
            placeholder='Пароль'
            set={e => setPassword(e)}
            value={password}
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
