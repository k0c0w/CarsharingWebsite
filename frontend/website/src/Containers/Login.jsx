import Section from '../Components/Sections'
import Container from '../Components/Container'
import React from 'react'
import Form, { Input } from '../Components/formTools'
import Bold from '../Components/TextTags'
import { NavLink } from 'react-router-dom'
import { useState, useRef } from 'react'
import API from '../httpclient/axios_client'
import { areValidLoginFields } from '../js/form-validators'
import GoogleSignIn from '../Components/SignInButtons'

const loginVM = {
    Email: [""],
    Password: [""]
}

export default function Login () {
  var api = new API()
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [errors, setErrors] = useState({})

  const [errorsAfter, setErrorsAfter] = useState(loginVM)
  const loginRef = useRef(null)
  const passwordRef = useRef(null)

   async function handleLogin (event) {

    event.preventDefault();
        if(!areValidLoginFields(loginRef.current, passwordRef.current, setErrors)) return;

    var body = {
      email: email,
      password: password
    }

    var response = await api.login(body)
    console.log(response)
    if (response.isError === true) {
      setErrorsAfter(response.message)
    }
  }

  return (
    <Section>
      <Container className='flex-container'>
        <Form className='center flex-column'>
          <Bold id='loginHeader' className='form-header'>
            Войти
          </Bold>
          <h1 color='red'></h1>
          <Input
            ref={loginRef}
            name='Email'
            placeholder='Почта'
            set={e => setEmail(e)}
            value={email}
            inputErrorMessage={errors['login']}
            ><div color='red'>{errorsAfter?.Email[0] ?? ""}</div></Input>
          <Input
            ref={passwordRef}
            name='Password'
            placeholder='Пароль'
            set={e => setPassword(e)}
            value={password}
            inputErrorMessage={errors['password']}
            ><div color='red'>{errorsAfter?.Password[0] ?? ""}</div></Input>
          
          <GoogleSignIn
            redirect_uri='https://localhost:7129/api/account/google-external-auth-callback/'
            scope='https://www.googleapis.com/auth/userinfo.email'
            client_id='930943899094-n86i2ipn8jb3j51aj9d8k2tcojd89ilb.apps.googleusercontent.com'
          />
          <div
            id='formButton'
            className='form-filed'
            style={{ marginTop: '15px' }}
          >
            <a
              className='button form-button'
              onClick={(event) => {
                handleLogin(event)
              }}
            >
              Login
            </a>
            <NavLink className='softblue-regular' to='/registration'>
              Регистрация
            </NavLink>
          </div>
        </Form>
      </Container>
    </Section>
  )
}
