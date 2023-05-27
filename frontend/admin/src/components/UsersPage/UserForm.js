import React, { useEffect } from 'react';
import Button from '@mui/material/Button';

import InputBase from '@mui/material/InputBase';
import MenuItem from '@mui/material/MenuItem';

import '../../styles/car-page.css';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';
import { useState } from 'react';
import API from '../../httpclient/axios_client';



var handleSubmit = (e) => {
    var inputs = e.target.parentNode.getElementsByTagName('input')
    var result = {};

    Array.from(inputs).forEach(element => {
        var name = element?.name ?? "not exist";
        result[name] = element?.value ?? "not exist";
    });

    return (result);
}

const commonStyle = {
    variant:"outlined",
    size:'small',
    style: { border: '25px' },
    border:"white"
}

export function UserForm({user}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    
    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs' id='form'>
                <StyledTextField {...commonStyle} 
                    label="Имя" name='name' type={'text'} value={user?.name.trimEnd().trimStart()}/>

                <StyledTextField {...commonStyle} placeholder={'Фамилия'}label="Фамилия"name='surname'type={'text'}
                    value={user?.surname.trimEnd().trimStart()}/>
                <StyledTextField {...commonStyle} placeholder={'Почта'} name='email' value={user?.email} type={'email'}/>
                <StyledTextField {...commonStyle}
                    placeholder={'Дата рождения'} name='birthdate'value={user?.birthdate} type={'date'}/>
                <StyledTextField {...commonStyle} placeholder={'Пароль'} name='password' value={user?.password} type={'password'}/>
                <input name='accept' value='on' hidden/>

            </div>
        </>
    )
};

export const UserFormTitle = ({ title = 'Добавить объект' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const UserFormSubmit = ({ handler, title = 'Создать' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (
        <Button disableFocusRipple className="submit" type="submit"
            style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
            onClick={(e) => handler(handleSubmit(e))}>
            {title}
        </Button>
    );
}
