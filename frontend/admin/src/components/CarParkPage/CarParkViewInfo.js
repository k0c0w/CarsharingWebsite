import React, { useEffect,useState } from "react";
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import API from "../../httpclient/axios_client";


export function CarParkViewInfo({ carPark }) {
    const theme = useTheme();
    // const [carModel, setCarModel] = useState({});
    const color = tokens(theme.palette.mode);

    // var getCars = async () => {
    //     var result = await API.getCars();
    //     if (result.successed !== true)
    //         alert(result.error);
    //     setCarModel(result.data);
    // } 

    //getTariff();

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{carPark.id}</div>
                <h4>Гос. Номер:</h4>
                <div>{carPark.license_plate}</div>
                <h4>Состояние:</h4>
                <div>{carPark.is_open}</div>
                <h4>Активность:</h4>
                <div>{carPark.is_taken}</div>
            </div>
        </>
    )
}
export function CarParkViewInfoTitle({title="Информация"}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    return (
        <h3 style={{ color: color.grey[100] }}>{title}</h3>
    )
}
