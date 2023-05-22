import React, { useState, useContext, useEffect } from 'react';
import { useTheme } from '@emotion/react';
import { Box } from '@mui/system';
import { Button } from '@mui/material';

import '../../styles/car-page.css';
import '../../styles/popup.css'
import { ColorModeContext, tokens } from '../../theme';
import TarrifsGrid from './TarrifsGrid';
import { Popup } from '../Popup';
import { TarrifForm, TarrifFormTitle, TarrifFormSubmit } from './TarrifForm';
import { TarrifViewInfo, TarrifViewInfoTitle } from './TarrifViewInfo';
import { getElementsByTagNames } from '../../functions/getElementsByTags';
import API from '../../httpclient/axios_client';
import { TableAddRefreshButtons } from '../TableCommon';


function send() {
    const elements = getElementsByTagNames("input,textarea", document.getElementById("form"));
    const obj = Object.values(elements).reduce((obj, field) => { obj[field.name] = field.value; return obj }, {});

    var body = JSON.stringify(obj);
    console.log(body);
    var result = API.getCars(body);
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
            title: <TarrifFormTitle></TarrifFormTitle>,
            submit: <TarrifFormSubmit></TarrifFormSubmit>,
            close: () => setD('none'),
            axiosRequest: () => {},
            inputsModel: <TarrifForm handler={console.log}></TarrifForm>,
        }
    );

    const handleClickInfo = (model) => {
        var popup = {
            title: <TarrifViewInfoTitle></TarrifViewInfoTitle>,
            close: () => setD('none'),
            axiosRequest: (data) => API.createTariff(data),
            inputsModel: <TarrifViewInfo tarrifModel={model}></TarrifViewInfo>,
            submit: false
        };
        console.log(model)
        setPopup(popup);
        setD('block');
    }


    const handleClickAdd = () => {
        var popup = {
            title: <TarrifViewInfoTitle title='Добавить'></TarrifViewInfoTitle>,
            close: () => setD('none'),
            axiosRequest: (data) => API.createTariff(data),
            submit: <TarrifFormSubmit handler={send}></TarrifFormSubmit>,
            inputsModel: <TarrifForm></TarrifForm>,
        };
        setPopup(popup);
        debugger
        console.log(selected[0]);
        setD('block');
    }
    const handleClickEdit = () => {
        var popup = {
            title: <TarrifFormTitle title='Изменить'></TarrifFormTitle>,
            close: () => setD('none'),
            submit: <TarrifFormSubmit></TarrifFormSubmit>,
            axiosRequest: (body) => API.updateTariff(body),
            inputsModel: <TarrifForm isEdit={true} carModel={selected[0]}></TarrifForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }
    return (
        <>
            <TableAddRefreshButtons addHandler = {handleClickAdd} refreshHandler={refreshRows} color={color}/>
            <TarrifsGrid handleClickInfo={handleClickInfo} handleSelect={(list)=>setSelected(list)} rows={tariffsData}></TarrifsGrid>

            <Box position="fixed" left={'0%'} top={'0%'} width={'100%'} >
                <footer style={{ opacity: (selected.length === 0 ? 0 : 1) }}>
                    <div style={{ position: 'absolute', left: '80%' }}>
                        <Button
                            disabled={selected.length !== 1}
                            variant={'contained'}
                            style={{ backgroundColor: (selected.length !== 1 ? color.grey[500] : color.primary[100]), color: color.primary[900], marginRight: '20px' }}
                            onClick={()=>handleClickEdit()}
                            >
                            Изменить
                        </Button>
                        <Button
                            variant={'contained'}
                            disabled={selected.length === 0}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
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