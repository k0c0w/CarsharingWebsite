import React, { useState } from 'react';
import Button from '@mui/material/Button';
// import Table from '@mui/material/Table';
// import TableBody from '@mui/material/TableBody';
// import TableCell from '@mui/material/TableCell';
// import TableContainer from '@mui/material/TableContainer';
// import TableHead from '@mui/material/TableHead';
// import TableRow from '@mui/material/TableRow';
// import Paper from '@mui/material/Paper';
import { Typography, useTheme, Box } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { rows } from '../../data/carsRows';
import { tokens } from '../../theme';



export const columnsFabric = (color) => [
    {
        field: 'id',
        headerName: 'Id'
    },
    {
        field: 'name',
        headerName: 'Name',
        flex: 1,
        // cellClassName: 'name-column-cell'
    },
    {
        field: 'number',
        headerName: 'Number',
        flex: 1,
        // cellClassName: 'number-column-cell'
    },
    {
        field: 'func',
        headerName: 'Func',
        flex: 1,
        renderCell: ({ row: { access } }) => {
            return (
                <Box
                    width="60%"
                    borderRadius={"5px"}
                    sx= {{ height: '30px', width: '10px',  }}
                >
                    <Button variant={'contained'} style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px' }}>Изменить</Button>
                    <Button variant={'contained'} style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                </Box>
            )
        }
    }
]



function TableCars() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const columns = columnsFabric(color)

    return (
        <Box
            width="100%"
            m="0 auto"
            p="5px"
            display="flex"
            justifyContent="center"
            sx={{
                "& .MuiDataGrid-root": {
                    border: "none",
                },
                "& .MuiDataGrid-cell": {
                    borderBottom: "none",
                },
                "& .name-column-cell": {
                    color: color.grey[600],
                },
                "& .MuiDataGrid-columnHeaders": {
                    backgroundColor: color.grey[800],
                    borderBottom: "none",
                },
                "& .MuiDataGrid-virtualScroller": {
                    backgroundColor: color.grey[900],
                },
                "& .MuiDataGrid-footerContainer": {
                    borderTop: "none",
                    backgroundColor: color.grey[800],
                },
                "& .MuiCheckbox-root": {
                    color: `${color.grey[500]} !important`,
                },
            }}
        >
            <DataGrid
                autoHeight
                checkboxSelection disableRowSelectionOnClick
                columns={columns}
                rows={rows}
            />
        </Box>
    )
}

export default TableCars;


