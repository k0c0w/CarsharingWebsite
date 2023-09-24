import React from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';


export function UserViewInfo({ userModel }) {
    const theme = useTheme();
    const style = { display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: tokens(theme.palette.mode).grey[100] }

    return (
        <>
            <div style={Object.assign({}, style, {flexDirection: 'row', gap:"30px"})}>
                <div style={style}>
                    <h4>Id:</h4>
                    <div>{userModel.id}</div>
                    <h4>Почта:</h4>
                    <div>{userModel.email}</div>
                    <h4>Имя:</h4>
                    <div>{userModel.name}</div>
                    <h4>Фамилия:</h4>
                    <div>{userModel.surname}</div>
                </div>
                <div style={style}>
                    <h4>Данные подтверждены: </h4>
                    <div>{userModel.personal_info.is_info_verified ? "Подтверждены" : "Не подтверждены"}</div>
                    <h4>Паспорт</h4>
                    <div>{userModel.personal_info.passport}</div>
                    <h4>Права:</h4>
                    <div>{userModel.personal_info.license}</div>
                    <h4>Дата рождения: </h4>
                    <div>{new Date(userModel.personal_info.birthdate).toDateString()}</div>
                    <h4>Баланс</h4>
                    <div>{userModel.personal_info.account_balance}</div>
                </div>
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
