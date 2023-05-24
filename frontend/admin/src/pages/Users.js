import React, { useState, useEffect } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import UserTable from '../components/UsersPage/UserTableManagement';
import API from '../httpclient/axios_client'
import { TableSearchField } from '../components/TableCommon';
import { styleTextField } from '../styleComponents';
import { getElementsByTagNames } from '../functions/getElementsByTags';


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

    

    const [usersData, setUsersData] = useState([]);
    
    var loadData = async () => {
        const result = await API.getUsers();

        if (result.successed)
            setUsersData(result.data);
    }

    useEffect(()=>{ 
        loadData()
    }, []);

    return (
        <>
            <h1>
                User Models
            </h1>
            <TableSearchField data={usersData} attrs={attrs} defaultAttrName="UserName" setData={setUsersData}/>
            <div className='commandsList'>
                <UserTable usersData={usersData} refreshRows={()=>loadData()}/>
            </div>
        </>
    )
}

export default UserMngmt;