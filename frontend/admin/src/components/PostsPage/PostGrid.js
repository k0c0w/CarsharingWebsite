import React, { useState } from 'react';
import Button from '@mui/material/Button';
import { useTheme, Box } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { tokens } from '../../theme';





export function PostGrid({handleClickInfo, handleSelect, rows=[]}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [, setSelected] = useState([]);

    const columns = [
        {
            field: 'id',
            headerName: 'Id',
            align:'right',
            type: 'number'
        },
        {
            field: 'title',
            headerName: 'Title',
            flex: 1,
            align:'left',
            type:'string',
            // cellClassName: 'name-column-cell'
        },
        {
            field: 'body',
            headerName: 'Body',
            flex: 1,
            type:'string',
            // cellClassName: 'number-column-cell'
        },
        {
            field: 'func',
            headerName: '',
            flex: 1,
            menu:false,
            sortable: false,
            renderCell: (params) => {
                return (
                    <Box
                        width="60%"
                        borderRadius={"5px"}
                        sx= {{ height: '30px', width: '10px',  }}
                    >
                        <Button
                            variant={'contained'}
                            style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px' }}
                            onClick={(e)=>handleClickInfo(params.row)} //e.target.parentNode.parentNode.parentNode.childNodes
                        >
                            Посмотреть данные
                        </Button>
                    </Box>
                )
            }
        }
    ]

    //  ----- Оптимизировать -----  //
    const _handleSelect = async (listId) => {
        // const rows = await API.getTariff();
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


