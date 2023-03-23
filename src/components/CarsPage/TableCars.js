import React, { useState } from 'react';
import Button from '@mui/material/Button';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

import { useTheme } from '@emotion/react';

import {  tokens } from '../../theme';


function TableCars({ rows }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Id</TableCell>
                        <TableCell align="right">Машины</TableCell>
                        <TableCell align="right">Номер&nbsp;</TableCell>
                        <TableCell align='center'>-</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.map((row) => (
                        <TableRow
                            key={row.name}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">{row.id}</TableCell>
                            <TableCell align="right">{row.name}</TableCell>
                            <TableCell align="right">{row.number}</TableCell>
                            <TableCell align="center" style={{ paddingRight:"0px" }}>
                                <Button style={{ color: color.primary[100], height: '30px', width: '10px', marginRight:'20px' }}>Изменить</Button>
                                <Button style={{ color: color.primary[100], height: '30px', width: '10px'  }}>Удалить</Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}

export default TableCars;