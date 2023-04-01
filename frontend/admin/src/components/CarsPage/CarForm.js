import React, { useContext, useEffect } from 'react';
import Button from '@mui/material/Button';
import { TextField, Input, colors } from '@mui/material';
import { maxWidth } from '@mui/system';
import InputBase from '@mui/material/InputBase';
import MenuItem from '@mui/material/MenuItem';

import '../../styles/car-page.css';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';
import { useState } from 'react';
import axiosInstance from '../../httpclient/axios_client';




// var re = new RegExp("^[A-Z]{1}[0-9]{3}[A-Z]{2}-[0-9]{2,3}$")

// var validate = (text) => {
//     text.replace("/\s+/g", "")
//     text.replace('/\B(?=([0-9]{3})+(?![0-9]))/g', " ")
//     console.log(`${text} - ${re.test(text)} `)
// }


var handleSubmit = (e) => {
    var inputs = e.target.parentNode.getElementsByTagName('input')
    var result = {};

    Array.from(inputs).forEach(element => {
        var name = element?.name ?? "not exist";
        result[name] = element?.value ?? "not exist";
    });

    return (result);
}


export function CarForm({ carModel }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [tarrifs, setTarrifs] = useState([]);
    
    var getTarrifs = async () => {
        let settings = require('../../appsettings.json');

        await axiosInstance.get(`http://${settings.appUrl}/api/tariff/tariffs`)
        .then((response) => response.json())
        .then((response) => { 
            setTarrifs(response);
            console.log(response);
        })
    }
    useEffect(() => {
        getTarrifs();
    },[])
    
    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs' id='form'>
                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Имя"
                    name='name'
                    type={'text'}
                    value={carModel?.name}
                >
                </StyledTextField>

                <StyledTextField
                    placeholder={'"Серия и номер"-"регион"'}
                    variant="outlined"
                    size='small'
                    label="Номер"
                    border="white"
                    name='number'
                    type={'text'}
                    value={carModel?.number}
                >
                </StyledTextField>

                <StyledTextField
                    id="Тариф"
                    select
                    type={'number'}
                    label="Тариф"
                    helperText=""
                    name='tariffId'        /////// --------------- поменять название
                    defaultValue={carModel?.tarrif ?? ''}
                >
                    {tarrifs.map((option) => (
                        <MenuItem key={option.id} value={option.id}>
                            {option.name}
                        </MenuItem>
                    ))}
                </StyledTextField>

                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Описание'}
                    fullWidth={true}
                    variant="outlined"
                    name='description'
                    minRows={2} maxRows={10}
                    multiline={true}
                    value={carModel?.description}
                    type={'text'}
                >
                </StyledTextField>

                <InputBase placeholder='Фото машины' name='sourceImg' label='Фото машины' type='file'></InputBase>

            </div>
        </>
    )
}

export const CarFormTitle = ({ title = 'Добавить объект' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const CarFormSubmit = ({ handler, title = 'Сделать запрос' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (
        <Button disableFocusRipple className="submit" type="submit"
            style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
            onClick={(e) => handler(handleSubmit(e))}>
            {title}
        </Button>
    );
}
