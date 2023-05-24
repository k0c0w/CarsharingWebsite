import React, { useEffect,useState } from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import API from "../../httpclient/axios_client";


export function UserViewInfo({ userModel }) {
    const theme = useTheme();
    const [tariff, setTariff] = useState({});
    const color = tokens(theme.palette.mode);

    

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{userModel.id}</div>
                <h4>Почта:</h4>
                <div>{userModel.email}</div>
                <h4>Имя:</h4>
                <div>{userModel.user_name}</div>
                <h4>Фамилия:</h4>
                <div>{userModel.surname}</div>
            </div>
        </>
    )
}
export function UserViewInfoTitle({title="Информация"}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    return (
        <h3 style={{ color: color.grey[100] }}>{title}</h3>
    )
}
