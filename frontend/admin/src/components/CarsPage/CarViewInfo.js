import React, { useEffect, useState } from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import API from "../../httpclient/axios_client";


export function CarViewInfo({ carModel }) {
    const theme = useTheme();
    const [tariff, setTariff] = useState({});
    const color = tokens(theme.palette.mode);

    

    useEffect(() => {
        (async () => {
            const result = await API.getTariffById(carModel.tariff_id);
            if (result.successed !== true)
                alert(result.error);
            setTariff(result.data);
        })();
    }, []);

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{carModel.id}</div>
                <h4>Бренд:</h4>
                <div>{carModel.brand}</div>
                <h4>Модель:</h4>
                <div>{carModel.model}</div>
                <h4>Описание:</h4>
                <div>{carModel.description}</div>
                <h4>Тариф:</h4>
                <div>{tariff?.name}</div>
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
