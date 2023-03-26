import React, { useContext } from 'react';
import Button from '@mui/material/Button';
import { TextField, Input, colors } from '@mui/material';
import { maxWidth } from '@mui/system';
import InputBase from '@mui/material/InputBase';
import MenuItem from '@mui/material/MenuItem';

import '../../styles/car-page.css';
import { ColorModeContext, tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';

const tariffs = [
    {
        value: '5H',
        label: '5H',
    },
    {
        value: '1D',
        label: '1D',
    },
    {
        value: '1W',
        label: '1W',
    },
    {
        value: '7W',
        label: '7W',
    },
];



var handleSubmit = (e) => {
    var inputs = e.target.parentNode.getElementsByTagName('input')
    var result = {};

    Array.from(inputs).forEach(element => {
        var name = element?.name ?? "not exist";
        result[name] = element?.value ?? "not exist";
    });

    return (result);
}


export function CarForm({ carModel: tarrifModel }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs'>
                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Имя"
                    name='name'
                    value={tarrifModel?.name}
                >
                </StyledTextField>

                <StyledTextField
                    placeholder={'Цена'}
                    variant="outlined"
                    size='small'
                    label="Цена"
                    border="white"
                    name='number'
                    value={tarrifModel?.price}
                >
                </StyledTextField>

                <StyledTextField
                    id="Длительность"
                    select
                    label="Длительность"
                    helperText=""
                    name='period'
                    defaultValue={tarrifModel?.period ?? '5H'} 
                >
                    {tariffs.map((option) => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
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
                    value={tarrifModel?.description}
                    >
                </StyledTextField>


            </div>
        </>
    )
}

export const CarFormTitle = ({title='Добавить объект'}) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const CarFormSubmit = ({ handler, title='Сделать запрос' }) => {
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
