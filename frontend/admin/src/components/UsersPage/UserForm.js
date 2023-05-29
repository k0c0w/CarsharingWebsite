import React, { useEffect, useRef } from 'react';
import Button from '@mui/material/Button';
import '../../styles/car-page.css';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';
import { useState } from 'react';
import API from '../../httpclient/axios_client';
import { Box, Select } from '@mui/material';



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

async function onMoneyButtonClick(id, selectRef, moneyRef, setMoneySent, setError, saveCallback){
    if(moneyRef && selectRef){
        setMoneySent(true);
        const money = parseFloat(moneyRef.current.value);
        if(!money || money && money < 0)
        {
            setError({moneyError: "Не корректная сумма"})
            return;
        }
        const select = selectRef.current.value;
        if(select == "add"){
            const response = await API.giveMoney(id, money);
            debugger;
            if(response.successed){
                saveCallback();
                setError({});
            }
            else
                setError({moneyError: "Не сохранено"})

        }
        else if(select == "subtract"){
            const response = await API.subtractMoney(id, money);
            if(response && response.successed){
                saveCallback();
                setError({});
            }
            else
                setError({moneyError: "Не сохранено"})
        }

        setMoneySent(false);
    }
}


async function onSaveButtonClick(id, formRef, setEditSent, setError, saveCallback){
    if(formRef){
        setEditSent(true);
        const data = Array.from(formRef.current.elements)
        .filter((element) => element.name)
        .reduce(
          (obj, input) => Object.assign(obj, { [input.name]: input.value.trimEnd().trimStart() }),
          {}
        );

        const response = await API.editUser(id, data);
        if(response && response.successed){
            saveCallback();
            setError({})
        }
        else
            setError({saveError: "Ошибка сохранения"})
        setEditSent(false);
    }
}

export function EditUserForm ({user, saveCallback}){
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const selectRef = useRef(null);
    const moneyRef = useRef(null);
    const formRef = useRef(null);
    const [moneySent, setMoneySent] = useState(false);
    const [editSent, setEditSent] = useState(false);
    const [error, setError] = useState({})
    const [balance, setBalance] = useState(parseFloat(user.personal_info.account_balance))

    const StyledTextField = styleTextField(color.primary[100]);

    function changeBalance() {
        if(selectRef?.current.value == "add" && moneyRef?.current)
            setBalance(balance + parseFloat(moneyRef.current.value))
        else if(selectRef?.current.value == "subtract" && moneyRef?.current)
            setBalance(balance - parseFloat(moneyRef.current.value))
    }

    return (
        <>
            <div style={{color: "red"}}>{error['moneyError']}</div>
            <div style={{color: "red"}}>{error['saveError']}</div>
            <form className='inputs' id='form' ref={formRef}>
                <input {...commonStyle} required
                    label="Имя" name='name' type={'text'} defaultValue={user?.name.trimEnd().trimStart()}/>
                <input {...commonStyle} placeholder={'Фамилия'}label="Фамилия"name='surname'type={'text'} required
                    defaultValue={user?.surname.trimEnd().trimStart()}/>
                <input {...commonStyle} placeholder={'Почта'} name='email' defaultValue={user?.email} type={'email'} required/>
                <input {...commonStyle} required
                    placeholder={'Дата рождения'} name='birthdate' defaultValue={user?.birthdate} type={'date'}/>
                {!editSent && <Button onClick={()=>onSaveButtonClick(user.id, formRef, setEditSent, setError, () => saveCallback(user.id))}>Сохранить</Button>}            
            </form>
            <Box>
                <label style={{color:color.primary[100] }}>
                    <div>Текущий баланс: {balance}</div>
                    <label>
                        <div>
                            Изменить:
                        </div>
                        <select {...commonStyle} style={{minWidth: "100px"}} ref={selectRef}>
                            <option value="add">Пополнить</option>
                            <option value="subtract">Убавить</option>
                        </select>
                    </label>
                    
                    <input ref={moneyRef} style={{width:"inherit"}}
                        label="Сумма в рублях" placeholder={'рубли'} type='number'
                    />
                    {!moneySent && <Button {...commonStyle} onClick={() => onMoneyButtonClick(user.id, selectRef, moneyRef, setMoneySent, setError, () =>{saveCallback(user.id); changeBalance();})}>
                        Выполнить запрос</Button>}
                </label>
            </Box>
        </>
    )
}

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
