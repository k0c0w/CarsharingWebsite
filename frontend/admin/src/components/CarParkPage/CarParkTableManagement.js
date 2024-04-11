import React, { useState } from 'react';
import { useTheme } from '@emotion/react';
import { Box } from '@mui/system';
import { Button } from '@mui/material';
import { TableAddRefreshButtons } from '../TableCommon';
import '../../styles/car-page.css';
import '../../styles/popup.css'
import { tokens } from '../../theme';
import CarParkGrid from './CarParkGrid';
import { Popup } from '../Popup';
import { CarForm, CarFormTitle, CarFormSubmit } from './CarForm';
import { CarParkViewInfo, CarParkViewInfoTitle } from './CarParkViewInfo';
import API from '../../httpclient/axios_client';


function CarParkTable({ refreshRows, carParkData }) {
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
            axiosRequest: () => {},
            inputsModel: <CarForm handler={console.log}></CarForm>,
        }
    );

    //открывают попап с нужным действием
    let handleClickInfo = (model) => {
         const popup = {
             title: <CarParkViewInfoTitle></CarParkViewInfoTitle>,
             close: () => setD('none'),
             inputsModel: <CarParkViewInfo carPark={model}></CarParkViewInfo>
         };
        setPopup(popup);
        setD('block');
    }

    const handleClickAdd = () => {
        const popup = {
            title: <CarFormTitle title='Добавить'></CarFormTitle>,
            close: () => setD('none'),
            inputsModel: <CarForm></CarForm>,
            axiosRequest: (e) => API.createCarModel(e),
            submit: true
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }

    const handleClickChange = () => {
        const popup = {
            title: <CarFormTitle title='Изменить'></CarFormTitle>,
            close: () => setD('none'),
            axiosRequest: (e) => API.createCarModel(e),
            submit: <CarFormSubmit></CarFormSubmit>,
            inputsModel: <CarForm carModel={selected[0]}></CarForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }

    return (
        <>
            <TableAddRefreshButtons addHandler = {handleClickAdd} refreshHandler={refreshRows}/>

            <CarParkGrid handleClickInfo={handleClickInfo} handleSelect={(list) => setSelected(list)} rows={carParkData}/>

            <Box position="fixed" left={'0%'} top={'0%'} width={'100%'} >
                <footer style={{ opacity: (selected.length === 0 ? 0 : 1) }}>
                    <div style={{ position: 'absolute', left: '80%' }}>
                        {<Button
                            disabled={selected.length !== 1}
                            variant={'contained'}
                            style={{ backgroundColor: (selected.length !== 1 ? color.grey[500] : color.primary[100]), color: color.primary[900], marginRight: '20px' }}
                            onClick={() => handleClickChange()}
                        >
                            Изменить
                        </Button>}
                        <Button
                            variant={'contained'}
                            disabled={selected.length === 0}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                    </div>
                </footer>
            </Box>

            {<Box style={{ display: display, color: color.grey[100] }}>
                <Popup {...popupInput} > </Popup>
            </Box>}
        </>
    )
}

export default CarParkTable;