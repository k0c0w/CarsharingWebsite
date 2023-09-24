import React from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';

const hrStyle = {
    color: "black",
    width: "600px"
}

export function TarrifViewInfo({ tarrifModel }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{tarrifModel?.id}</div>
                <hr style={hrStyle}/>
                <h4>Название:</h4>
                <div>{tarrifModel?.name}</div>
                <hr  style={hrStyle}/>
                <h4>Макс. Пробег:</h4>
                <div>{tarrifModel?.max_millage}</div>
                <hr  style={hrStyle}/>
                <h4>Стоимость:</h4>
                <div>{tarrifModel?.price}</div>
                <hr style={hrStyle}/>
                <h4>Статус:</h4>
                <div>{tarrifModel?.is_active ? "Активен" : "Отключен"}</div>
                <hr style={hrStyle}/>
                <h4>Описание:</h4>
                <div>{tarrifModel?.description}</div>
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
