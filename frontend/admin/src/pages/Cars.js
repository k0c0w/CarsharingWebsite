import React, { useState } from 'react';
import CarTable from '../components/CarsPage/CarTableManagement';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';

import '../styles/car-page.css';
import { styleTextField } from '../styleComponents';


const attrs = [
    {
        value: 'Name',
        label: 'Имя',
        labelHelper: "Имени"
    },
    {
        value: 'Number',
        label: 'Номер',
        labelHelper: "Номеру"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    }
];

var getAttr = (value) => {
    var result = null;
    attrs.forEach(attr => {
        if (attr.value === value)
            result = attr;
    })
    return result;
}



function CarsMngmt() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    // Аттрибут для поиска 
    const [attr, setAttr] = useState("Number");

    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <h1>
                Car Management
            </h1>
            <div style={{ marginTop: '70px' }}>
                <StyledTextField
                    style={{ width: '250px' }}
                    type={'search'}
                    helperText={`Найти по ${getAttr(attr)?.labelHelper?.toLowerCase() ?? attr}`}>

                </StyledTextField>

                <StyledTextField
                    style={{ width: '170px' }}
                    select
                    label="Аттрибут"
                    defaultValue={attr}
                    helperText=""
                    onChange={(e) => setAttr(e.target.value)}
                >
                    {attrs.map((option) => (
                        <MenuItem key={option.value} value={option.value}>
                            {option.label}
                        </MenuItem>
                    ))}
                </StyledTextField>
                <Button
                    style={{ marginTop: '10px', marginLeft: '20px', backgroundColor: color.primary[100], color: color.primary[900] }}
                    variant={'contained'}
                >
                    Поиск
                </Button>
            </div>
            <div className='commandsList'>
                <CarTable></CarTable>
            </div>
        </>
    )
}

export default CarsMngmt;