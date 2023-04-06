import React, { useEffect } from 'react';
import Button from '@mui/material/Button';

import InputBase from '@mui/material/InputBase';
import MenuItem from '@mui/material/MenuItem';

import '../../styles/car-page.css';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';
import { useState } from 'react';
import axiosInstance from '../../httpclient/axios_client';



var handleSubmit = (e) => {
    var inputs = e.target.parentNode.getElementsByTagName('input')
    var result = {};

    Array.from(inputs).forEach(element => {
        var name = element?.name ?? "not exist";
        result[name] = element?.value ?? "not exist";
    });

    return (result);
}


export function CarForm({carModel}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [tarrifs, setTarrifs] = useState([]);
    
    const getTarrifs = async () => {
        axiosInstance.get(`tariff/tariffs`)
        .then((response) => response.data)
        .then((response) => { 
            setTarrifs(response);
            console.log(response);
        })
        .catch(err => console.log("Error ocured ehn recived tariff list for carform"));
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
                    label="Производитель"
                    name='brand'
                    type={'text'}
                    value={carModel?.brand.trimEnd().trimStart()}
                >
                </StyledTextField>

                <StyledTextField
                    placeholder={'Модель'}
                    variant="outlined"
                    size='small'
                    label="Модель"
                    border="white"
                    name='model'
                    type={'text'}
                    value={carModel?.number.trimEnd().trimStart()}
                >
                </StyledTextField>

                <StyledTextField
                    id="Тариф"
                    select
                    type={'number'}
                    label="Тариф"
                    helperText=""
                    name='tariffId'
                    defaultValue={carModel?.tariff_id}
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

                <InputBase placeholder='Фото машины' name='img' label='Фото машины' type='file'></InputBase>

            </div>
        </>
    )
};

export const CarFormTitle = ({ title = 'Добавить объект' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const CarFormSubmit = ({ handler, title = 'Создать' }) => {
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
