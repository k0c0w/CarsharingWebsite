import React, { useState, useContext } from 'react';
import { useTheme } from '@emotion/react';
import { Box } from '@mui/system';
import { Button } from '@mui/material';

import '../../styles/car-page.css';
import '../../styles/popup.css'
import { ColorModeContext, tokens } from '../../theme';
import { styleTextField } from '../../styleComponents';
import CarsGrid from './CarsGrid';
import { Popup } from '../Popup';
import { CarForm, CarFormTitle, CarFormSubmit } from './CarForm';
import { CarViewInfo, CarViewInfoTitle } from './CarViewInfo';


function createData(name, number, id) {
    return { name, number, id };
}



// A component is changing the default value state of an uncontrolled Select after being initialized. To suppress this warning opt to use a controlled Select. ??????


function CarTable({ handleSelect }) {
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

    var handleClickInfo = (model) => {
        var popup = {
            title: <CarViewInfoTitle></CarViewInfoTitle>,
            close: () => setD('none'),
            inputsModel: <CarViewInfo carModel={model}></CarViewInfo>
        };
        setPopup(popup);
        setD('block');
    }
    var handleClickAdd = () => {
        var popup = {
            title: <CarFormTitle title='Добавить'></CarFormTitle>,
            close: () => setD('none'),
            submit: <CarFormSubmit></CarFormSubmit>,
            inputsModel: <CarForm></CarForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }
    var handleClickChange = () => {
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

            <CarsGrid handleClickInfo={handleClickInfo} handleSelect={(list)=>setSelected(list)} ></CarsGrid>

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
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                    </div>
                </footer>
            </Box>

            <Box style={{ display: display, color: color.grey[100] }}>
                <Popup {...popupInput} > </Popup>
            </Box>
        </>
    )
}

export default CarTable;