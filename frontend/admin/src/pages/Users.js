import React, { useState, useEffect } from 'react';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import UserTable from '../components/UsersPage/UserTableManagement';
import API from '../httpclient/axios_client'
import { TableSearchField } from '../components/TableCommon';


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

    const onVerified = async (id) => {
        const index = usersData.findIndex(x => x.id == id);
        if (index !== -1) {
            const data = [...usersData]
            data[index].personal_info.is_info_verified = true;
            setUsersData(data)
        }
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
                <UserTable usersData={usersData} refreshRows={()=>loadData()} onVerified={onVerified}/>
            </div>
        </>
    )
}

export default UserMngmt;