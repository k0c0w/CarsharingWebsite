import React, { useEffect, useState } from 'react';
import InputBase from '@mui/material/InputBase';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useRef } from 'react';
import { Button } from '@mui/material';
import { styleTextField } from '../styleComponents';
import API from '../httpclient/axios_client';


export default function Login() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [tarrifs, setTarrifs] = useState([]);
    
    var handleSubmit = async (e) => {
        var inputs = e.target.parentNode.getElementsByTagName('input')
        var result = {};

        Array.from(inputs).forEach(element => {
            var name = element?.name ?? "not exist";
            result[name] = element?.value ?? "not exist";
        });
        
        console.log(result);

        result = await API.login(result);
        if (result.successed === true) {
            alert("logged in");
        }
        else {
            alert(result.error);
        }
    }


    const StyledTextField = styleTextField(color.primary[100]);

    return (
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
            {/* <Button
                onClick={()=>API.register()}
                style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
            >
                 Зарегаться
            </Button> */}
        </div>
    );
}