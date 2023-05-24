import React, { useState } from 'react';
import Button from '@mui/material/Button';
// import Table from '@mui/material/Table';
// import TableBody from '@mui/material/TableBody';
// import TableCell from '@mui/material/TableCell';
// import TableContainer from '@mui/material/TableContainer';
// import TableHead from '@mui/material/TableHead';
// import TableRow from '@mui/material/TableRow';
// import Paper from '@mui/material/Paper';
import { useTheme, Box } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import { tokens } from '../../theme';
import useAuth from '../../hooks/useAuth';
import API from '../../httpclient/axios_client';





function UserGrid({handleClickInfo, handleSelect, handleMakeAdmin, rows}) {
    const { auth } = useAuth();
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    var isAdmin = () => auth?.roles?.find(role => role == "Admin");

    

    const [selected, setSelected] = useState([]);
    
    debugger;
    const columns = [
        {
            field: 'id',
            headerName: 'Id',
            type: 'string'
        },
        {
            field: 'email',
            headerName: 'Почта',
            type: 'string',
        },
        {
            field: 'name',
            headerName: 'Имя',
        },
        {
            field: 'surname',
            headerName: 'Фамилия',
        },
        {
            field: 'func',
            headerName: 'Func',
            flex: 3,
            menu:false,
            sortable: false,
            renderCell: (params) => {
                return (
                    <>
                    <Box
                    width="15px"
                    borderRadius={"1px"}
                    sx= {{ height: '30px', width: '5px'  }}>
                        <Button 
                            variant={'contained'} 
                            style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px'}}
                            onClick={(e)=>handleClickInfo(params.row)}
                            >
                            Посмотреть данные
                        </Button>
                        { isAdmin && <>
                        <Button 
                            variant={'contained'} 
                            style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px',  }}
                            onClick={(e)=>API.makePersonManager(params.row.id)}
                            >
                            Роль - менеджер
                        </Button>
                        <Button 
                            variant={'contained'} 
                            style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px' }}
                            onClick={(e)=>API.makePersonAdmin(params.row.id)}
                            >
                            Роль - админ
                        </Button>
                        <Button 
                            variant={'contained'} 
                            style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px'}}
                            onClick={(e)=>API.makePersonUser(params.row.id)}
                            >
                            Убрать роль
                        </Button>
                        </>}
                    </Box>
                    
                    </>
                )
            }
        }
    ]
    
    //  ----- Оптимизировать -----  //
    const _handleSelect = async (listId) => {
        const result = []
        listId.forEach(id => {
            rows.forEach( row => {
                if (row.id == id) {
                    result.push(row);
                }
            })
        })
        setSelected(result);
        handleSelect(result);
    }
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
                onRowSelectionModelChange={(e)=>_handleSelect(e)}
            />
        </Box>
    )
}

export default UserGrid;


