import React, { useState } from 'react';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
import { TextField, Input } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import InputBase from '@mui/material/InputBase';

import style from '../../styles/car-page.css';
import { ColorModeContext, tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { useContext } from 'react';
import { styleTextField } from '../../styleComponents';
import TableCars from './TableCars';



function createData(name, number, id) {
    return { name, number, id };
}

const rows = [
    createData('merseders', 159, 6),
    createData('lexus', 237, 9),
    createData('toyota', 262, 16),
    createData('kalina', 305, 3),
    createData('porsha', 356, 19),
];

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
            <div className='inputsCar' style={{ display: 'flex', flexDirection: 'column', maxWidth: '650px', flexWrap: 'wrap', justifyContent: 'center' }}>
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

                <TableCars rows={rows}></TableCars>
            </div>


            <div className="floating-form-wrapper" style={{visibility:'hidden'}}>
                <div className="form" style={{ backgroundColor: color.grey[900], color: color.primary[900]  }}>
                <span class="close" >×</span>
                    <form>
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
                            border="white">
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
                        <div className="button-container" >
                            <input type="submit" style={{ backgroundColor: color.greenAccent[600] }}/>
                        </div>
                    </form>
                </div>
            </div>
        </>
    )
}

export default RemvoeCar;