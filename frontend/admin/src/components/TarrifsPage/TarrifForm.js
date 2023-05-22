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


const tarrifs = [
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


export function TarrifForm({ carModel: tarrifModel, isEdit }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs' id='form'>
                { isEdit && <input hidden name="id" defaultValue={tarrifModel?.id}></input> }
                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Имя"
                    name='name'
                    defaultValue={tarrifModel?.name}
                >
                </StyledTextField>

                <StyledTextField
                    placeholder={'Цена'}
                    variant="outlined"
                    size='small'
                    label="Цена"
                    border="white"
                    name='price'
                    defaultValue={tarrifModel?.price}
                >
                </StyledTextField>

                <StyledTextField
                    id="Длительность"
                    label="Длительность"
                    helperText=""
                    name='period'
                    border="white"
                    // defaultValue={tarrifModel?.period ?? ""} 
                >
                </StyledTextField>

                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Описание'}
                    fullWidth={true}
                    variant="outlined"
                    name='description'
                    minRows={2} maxRows={10}
                    multiline={true}
                    defaultValue={tarrifModel?.description}
                    >
                </StyledTextField>
                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Макс. пробег'}
                    fullWidth={true}
                    variant="outlined"
                    name='max_mileage'
                    itemType='number'
                    type='number'
                    multiline={true}
                    defaultValue={tarrifModel?.max_mileage}
                    >
                </StyledTextField>
            </div>
        </>
    )
}

export const TarrifFormTitle = ({title='Добавить объект'}) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const TarrifFormSubmit = ({ handler, title='Сделать запрос' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    // return (
    //     <Button disableFocusRipple className="submit" type="submit"
    //         style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
    //         onClick={(e) => handler(handleSubmit(e))}>
    //         {title}
    //     </Button>
    // );
}
