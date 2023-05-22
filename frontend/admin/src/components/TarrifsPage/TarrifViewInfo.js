import React from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';


export function TarrifViewInfo({ tarrifModel }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{tarrifModel?.id}</div>
                <h4>Название:</h4>
                <div>{tarrifModel?.name}</div>
                <h4>Длительность:</h4>
                <div>{tarrifModel?.period}</div>
                <h4>Стоимость:</h4>
                <div>{tarrifModel?.price}</div>
                <h4>Статус:</h4>
                <div>{tarrifModel.is_active ? "Активен" : "Отключен"}</div>
            </div>
        </>
    )
}
export function TarrifViewInfoTitle({title="Информация"}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    return (
        <h3 style={{ color: color.grey[100] }}>{title}</h3>
    )
}
