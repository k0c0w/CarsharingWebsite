import React, { useState } from 'react';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';

import { styleTextField } from '../styleComponents';
import TarrifTable from '../components/TarrifsPage/TarrifTableManagement';
import TarrifsGrid from '../components/TarrifsPage/TarrifsGrid';


const attrs = [
    {
        value: 'Name',
        label: 'Имя',
        labelHelper: "Имени"
    },
    {
        value: 'Period',
        label: 'Длительность',
        labelHelper: "Длительности"
    },
    {
        value: 'Price',
        label: 'Цена (p.)',
        labelHelper: "Ценe"
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



function TarrifMngmt() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    // Аттрибут для поиска 
    const [attr, setAttr] = useState("Period");

    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <h1>
                Tarrifs Management
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
                <TarrifTable />
            </div>
        </>
    )
}

export default TarrifMngmt;