import React, { useState, useEffect } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import UserTable from '../components/UsersPage/UserTableManagement';
import API from '../httpclient/axios_client'
import { TableSearchField } from '../components/TableCommon';
import { styleTextField } from '../styleComponents';
import { getElementsByTagNames } from '../functions/getElementsByTags';
import axiosInstance from '../httpclient/axios_client';


const attrs = [
    {
        value: 'User_name',
        label: 'Имя',
        labelHelper: "Имени"
    },
    {
        value: 'Email',
        label: 'Почта',
        labelHelper: "Почта"
    },
    {
        value: 'Surname',
        label: 'Фамилия',
        labelHelper: "Фамилия"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    }
];




function UserMngmt() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    // Аттрибут для поиска 

    function send() {
        const elements = getElementsByTagNames("input,textarea", document.getElementById("form"));
        const obj = Object.values(elements).reduce((obj, field) => { obj[field.name] = field.value; return obj }, {});
        obj.retryPassword = obj.password;

        var body = JSON.stringify(obj);
        console.log(body);
    }

    const [usersData, setUsersData] = useState([]);
    
    var loadData = async () => {
        var result = await API.getUsers();
        console.log(result);

        if (result.error !== null)
        setUsersData(result.data);
    }

    useEffect(()=>{ 
        loadData()
    }, []);

    const TextField = styleTextField(color.primary[100]);

    return (
        <>
            <h1>
                User Models
            </h1>
            <TableSearchField data={usersData} attrs={attrs} defaultAttrName="UserName" setData={setUsersData}/>
            <div className='commandsList'>
                <UserTable usersData={usersData} refreshRows={()=>loadData()}/>
            </div>
            <div>
                <div className='inputs' id='form'>
                    <TextField
                        variant="outlined"
                        size='small'
                        label="имя пользователя"
                        name='user_name'
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