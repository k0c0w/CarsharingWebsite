import React, { useContext } from 'react';
import Button from '@mui/material/Button';
import { TextField, Input, colors } from '@mui/material';
import { maxWidth } from '@mui/system';
import InputBase from '@mui/material/InputBase';
import MenuItem from '@mui/material/MenuItem';

import style from '../../styles/car-page.css';
import '../../styles/floating-form-dashboard.css'
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

// InputLabelProps={{
//     style: { color: colors.grey[100] },
// }}


function AddCar() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const colorMode = useContext(ColorModeContext);

    var re = new RegExp("^[A-Z]{1}[0-9]{3}[A-Z]{2}-[0-9]{2,3}$")

    var validate = (text) => {
        text.replace("/\s+/g", "")
        text.replace('/\B(?=([0-9]{3})+(?![0-9]))/g', " ")
        console.log(`${text} - ${re.test(text)} `)
    }

    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>

            <div className='inputsCar' style={{ display: 'flex', flexDirection: 'column', maxWidth: '650px', flexWrap: 'wrap', justifyContent: 'center' }}>
                <h3>
                    Добавить машину
                </h3>
                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Имя"
                >
                </StyledTextField>

                <StyledTextField
                    placeholder={'"Серия и номер"-"регион"'}
                    variant="outlined"
                    size='small'
                    label="Номер"
                    border="white"
                    onChange={(e) => validate(e.target.value.toString())}>
                </StyledTextField>

                <StyledTextField
                    id="Тариф"
                    select
                    label="Тариф"
                    defaultValue="5H"
                    helperText=""
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
                    minRows={2} maxRows={5}
                    multiline={true}>

                </StyledTextField>

                <InputBase placeholder='Фото машины' name='Фото машины' label='Фото машины' type='file'></InputBase>
            </div>
        </>
    )
}

export default AddCar;