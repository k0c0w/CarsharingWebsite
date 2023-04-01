import React, { useState, useContext, useEffect } from 'react';
import { useTheme } from '@emotion/react';
import { Box } from '@mui/system';
import { Button } from '@mui/material';

import '../../styles/car-page.css';
import '../../styles/popup.css'
import { ColorModeContext, tokens } from '../../theme';
import TarrifsGrid from './TarrifsGrid';
import { Popup } from '../Popup';
import { CarForm, CarFormTitle, CarFormSubmit } from './TarrifForm';
import { TarrifViewInfo, TarrifViewInfoTitle } from './TarrifViewInfo';
import { getElementsByTagNames } from '../../functions/getElementsByTags';
import { axiosInstance } from '../../httpclient/axios_client';

function send() {
    const elements = getElementsByTagNames("input,textarea", document.getElementById("form"));
    const obj = Object.values(elements).reduce((obj, field) => { obj[field.name] = field.value; return obj }, {});

    var body = JSON.stringify(obj);
    console.log(body);

    axiosInstance.post(`/tariff/create`, body)
    .then((response) => console.log(response))
}




function TarrifTable({ tariffsData, refreshRows }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    // selected from data grid of cars
    const [selected, setSelected] = useState([]);
    // Закрытие popup'a
    const [display, setD] = useState('none');
    // Модель формы для popup'а
    const [popupInput, setPopup] = useState(
        {
            title: <CarFormTitle></CarFormTitle>,
            submit: <CarFormSubmit></CarFormSubmit>,
            close: () => setD('none'),
            inputsModel: <CarForm handler={console.log}></CarForm>,
        }
    );

    const handleClickInfo = (model) => {
        var popup = {
            title: <TarrifViewInfoTitle></TarrifViewInfoTitle>,
            close: () => setD('none'),
            inputsModel: <TarrifViewInfo tarrifModel={model}></TarrifViewInfo>
        };
        setPopup(popup);
        setD('block');
    }


    const handleClickAdd = () => {
        var popup = {
            title: <CarFormTitle title='Добавить'></CarFormTitle>,
            close: () => setD('none'),
            submit: <CarFormSubmit handler={send}></CarFormSubmit>,
            inputsModel: <CarForm></CarForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }
    const handleClickChange = () => {
        var popup = {
            title: <CarFormTitle title='Изменить'></CarFormTitle>,
            close: () => setD('none'),
            submit: <CarFormSubmit></CarFormSubmit>,
            inputsModel: <CarForm carModel={selected[0]}></CarForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }

    return (
        <>
            <Button
                style={{ backgroundColor: color.greenAccent[300], color: color.primary[900], marginRight: '20px' }}
                onClick={(e)=> handleClickAdd()}>Добавить</Button>
            <Button style={{ backgroundColor: color.greenAccent[300], color: color.primary[900], marginRight: '20px' }} 
                onClick={(e)=> refreshRows()}>Обновить</Button>
            <TarrifsGrid handleClickInfo={handleClickInfo} handleSelect={(list)=>setSelected(list)} rows={tariffsData}></TarrifsGrid>

            <Box position="fixed" left={'0%'} top={'0%'} width={'100%'} >
                <footer style={{ opacity: (selected.length === 0 ? 0 : 1) }}>
                    <div style={{ position: 'absolute', left: '80%' }}>
                        <Button
                            disabled={selected.length !== 1}
                            variant={'contained'}
                            style={{ backgroundColor: (selected.length !== 1 ? color.grey[500] : color.primary[100]), color: color.primary[900], marginRight: '20px' }}
                            onClick={()=>handleClickChange()}
                            >
                            Изменить
                        </Button>
                        <Button
                            variant={'contained'}
                            disabled={selected.length === 0}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Посмотреть данные</Button>
                    </div>
                </footer>
            </Box>

            <Box style={{ display:display, color: color.grey[100] }}>
                <Popup {...popupInput} > </Popup>
            </Box>
        </>
    )
}

export default TarrifTable;