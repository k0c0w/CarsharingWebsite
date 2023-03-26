import React from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';


export function CarViewInfo({ carModel }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{carModel.id}</div>
                <h4>Название:</h4>
                <div>{carModel.name}</div>
                <h4>Номер:</h4>
                <div>{carModel.number}</div>
            </div>
        </>
    )
}
export function CarViewInfoTitle({title="Информация"}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    return (
        <h3 style={{ color: color.grey[100] }}>{title}</h3>
    )
}
