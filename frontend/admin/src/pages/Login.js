import React, { useEffect, useState } from 'react';
import InputBase from '@mui/material/InputBase';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useRef } from 'react';
import { Button } from '@mui/material';
import { styleTextField } from '../styleComponents';
import API from '../httpclient/axios_client';
import useAuth from '../hooks/useAuth';


export default function Login() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const { setAuth } = useAuth();
    
    var takeInputsValues = async (e) => {
        var inputs = e.target.parentNode.getElementsByTagName('input')
        var result = {};

        Array.from(inputs).forEach(element => {
            var name = element?.name ?? "not exist";
            result[name] = element?.value ?? "not exist";
        });
        
        return result;
    }

   
    var isAuth = () => setAuth.isAuthorized; 

    // setAuthState(setAuth.isAuthorized)

    const handleSubmit = async (e) => {
        e.preventDefault();

        var body = await takeInputsValues(e);
        console.log(body);
        try {
            const response = await API.login(body);
            const roles = response?.data?.roles;
            var isAuthorized = true 
            setAuth({ roles, isAuthorized });
            debugger
        } catch (err) {
            if (!err?.response) {
                alert('Сервер не отвечает');
            } else if (err.response?.status === 400) {
                alert('Почта или пароль неверен');
            } else if (err.response?.status === 401) {
                alert('Не авторизован');
            } else {
                alert('Вход не удался');
            }
        }
    }

    const StyledTextField = styleTextField(color.primary[100]);

    return (
            
         
        <div>
            { !isAuth() && 
            <div>
            <div className='form' style={{ display:'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', marginTop:"120px" }}>
                <StyledTextField
                    placeholder='Почта'
                    name='Email'
                    ></StyledTextField>
                <StyledTextField
                    placeholder='Почта'
                    name='Password'
                    ></StyledTextField>
                <Button
                    onClick={(e)=>handleSubmit(e)}
                    style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
                    >
                    Войти
                </Button>
            </div>
            <Button
                onClick={()=>API.become()}
                style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
                >
                 Зарегаться
            </Button>
            </div>
            }
            { isAuth() &&
            <Button
                onClick={()=>
                    {
                        setAuth( { roles:[], isAuthorized:false  } )
                        API.logout() 
                        console.log(setAuth)
                    }}
                style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
            >
                Выйти
            </Button>
            }
        </div>
        
    );
}