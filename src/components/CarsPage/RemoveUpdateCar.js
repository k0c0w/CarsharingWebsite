import React, { useState, useContext } from 'react';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
import InputBase from '@mui/material/InputBase';
import { useTheme } from '@emotion/react';

import style from '../../styles/car-page.css';
import { ColorModeContext, tokens } from '../../theme';
import { styleTextField } from '../../styleComponents';
import TableCars from './TableCars';
import { tariffs } from '../../data/tariffs';



function createData(name, number, id) {
    return { name, number, id };
}





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



// A component is changing the default value state of an uncontrolled Select after being initialized. To suppress this warning opt to use a controlled Select. ??????

var getAttr = (value) => {
    var result = null;
    attrs.forEach(attr => {
        if (attr.value === value)
            result = attr;
    })
    return result;
}

function RemvoeCar() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const colorMode = useContext(ColorModeContext);

    const [attrToDelete, setAttrD] = useState("Number");


    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputsCar' style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center' }}>
                <h3>
                    Удалить машины
                </h3>
                <div>
                    <StyledTextField
                        style={{ width: '250px' }}
                        type={'search'}
                        helperText={`Удалить по ${getAttr(attrToDelete)?.labelHelper?.toLowerCase() ?? attrToDelete}`}>

                    </StyledTextField>

                    <StyledTextField
                        style={{ width: '170px' }}
                        select
                        label="Аттрибут"
                        defaultValue={attrToDelete}
                        helperText=""
                        onChange={(e) => setAttrD(e.target.value)}
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


                
                <TableCars></TableCars>
            </div>

        </>
    )
}

export default RemvoeCar;