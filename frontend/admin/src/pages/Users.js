import React, { useState } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';

import { styleTextField } from '../styleComponents';
import { getElementsByTagNames } from '../functions/getElementsByTags';
import axiosInstance from '../httpclient/axios_client';


const attrs = [
    {
        value: 'Name',
        label: 'Имя',
        labelHelper: "Имени"
    },
    {
        value: 'Period',
        label: 'Длительность',
        labelHelper: "Длительности"
    },
    {
        value: 'Price',
        label: 'Цена (p.)',
        labelHelper: "Ценe"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    }
];

var getAttr = (value) => {
    var result = null;
    attrs.forEach(attr => {
        if (attr.value === value)
            result = attr;
    })
    return result;
}



function UserMngmt() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    // Аттрибут для поиска 
    const [attr, setAttr] = useState("Period");

    function send() {
        const elements = getElementsByTagNames("input,textarea", document.getElementById("form"));
        const obj = Object.values(elements).reduce((obj, field) => { obj[field.name] = field.value; return obj }, {});
        obj.retryPassword = obj.password;

        var body = JSON.stringify(obj);
        console.log(body);

        //body = '{"email":"string","password":"string","retryPassword":"string","name":"string","surname":"string","age":0}'

        axiosInstance.post(`/client/register`,body)
            .then((response) => console.log(response));
    }


    const TextField = styleTextField(color.primary[100]);

    return (
        <>
            <div>
                <div className='inputs' id='form'>
                    <TextField
                        variant="outlined"
                        size='small'
                        label="имя пользователя"
                        name='name'
                        type={'text'}
                    >
                    </TextField>

                    <TextField
                        variant="outlined"
                        size='small'
                        label="Фамилия"
                        border="white"
                        name="surname"
                        type={'text'}
                    >
                    </TextField>

                    <TextField
                        label="Почта"
                        helperText=""
                        name='email'
                        type={'email'}
                    >
                    </TextField>

                    <TextField
                        label="пароль"
                        helperText=""
                        name={'password'}
                        type={'password'}
                    >
                    </TextField>

                    <TextField
                        helperText=""
                        label="возраст"
                        name="age" 
                        type={'number'}
                    >
                    </TextField>

                </div>
                <button onClick={() => send()}>Создать</button>
            </div>
        </>
    )
}

export default UserMngmt;