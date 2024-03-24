import React, { useState } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { Button } from '@mui/material';
import { styleTextField } from '../styleComponents';
import API from '../httpclient/axios_client';
import useAuth from '../hooks/useAuth';

const takeInputsValues = (e) => {
    const inputs = e.target.parentNode.getElementsByTagName('input')
    const result = {};

    Array.from(inputs).forEach(element => {
        let name = element?.name ?? "not exist";
        result[name] = element?.value ?? "not exist";
    });
    
    return result;
}

export default function Login() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [summary, setSummary] = useState(null);
    const { auth, setAuth } = useAuth();
    const isAuth = () => auth.isAuthorized; 

    const handleSubmit = async (e) => {
        e.preventDefault();

        const body = takeInputsValues(e);
        setSummary(null);
        const response = await API.login(body);
        if(response.successed) {
            const roles = response.data?.roles;
            const isAuthorized = true 
            setAuth({ roles, isAuthorized });
        }
        else if(response.status){
            if(response.status === 401 && response.error) {
                const error = response.error;
                if(error.code === 1){
                    setSummary("Некоторые поля не заполнены или заполнены не верно.")
                }
                else if (error.code === 2){
                    setSummary(error.messages.join(', '));
                }
            }
        }
        else{
            alert('Сервер не отвечает');
        }
    }

    const StyledTextField = styleTextField(color.primary[100]);

    return (
            
         
        <div>
            { !isAuth() && 
            <div>
                <div style={{color:"red"}}>{summary}</div>
            <div className='form' style={{ display:'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', marginTop:"120px" }}>
                <StyledTextField
                    placeholder='Почта'
                    name='email'
                    type="email"
                    ></StyledTextField>
                <StyledTextField
                    placeholder='Пароль'
                    name='password'
                    type="password"
                    ></StyledTextField>
                <Button
                    onClick={(e) => handleSubmit(e)}
                    style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
                    >
                    Войти
                </Button>
            </div>
            </div>
            }
            { isAuth() &&
            <Button
                onClick={() =>
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