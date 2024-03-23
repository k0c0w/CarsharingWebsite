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



function DocumentsGrid({handleSwitch, handleSelect, rows}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const [, setSelected] = useState([]);

    const columns = [
        { field: 'id', headerName: '№', flex:1 },
        { field: 'name', headerName: 'Имя файла', flex:2 },
        { field: 'annotation', headerName: 'Аннотация', flex:3 },
        {
            field: 'isPublic',
            headerName: 'Публичный',
            flex:4,
            menu:false,
            sortable: false,
            renderCell: (params) => {
                const is_public = params.row.isPublic;
                return (
                    <Box
                        width="20%"
                        borderRadius={"5px"}
                        sx= {{ height: '30px', width: '10px',  }}
                    >
                        {is_public && <Button
                            variant={'contained'}
                            style={{ backgroundColor: "green", color: color.primary[900], marginLeft: 'auto' }}
                            onClick={() => handleSwitch(params.row.guid, false)}
                        >
                            Public
                        </Button>}
                        {!is_public &&<Button
                            variant={'contained'}
                            style={{ backgroundColor: "red", color: color.primary[900], marginRight: 'auto' }}
                            onClick={() => handleSwitch(params.row.guid, true)}
                        >
                            Private
                        </Button>}
                    </Box>
                )
            }
        },
        { field: 'creation', headerName: 'Дата создания', flex:5 },
    ];


    //  ----- Оптимизировать -----  //
    let _handleSelect = async (listId) => {
        let result = []
        listId.forEach(id => {
            rows.forEach( row => {
                if (row.id === id) {
                    result.push(row);
                }
            })
        })
        setSelected(result);
        console.log(result);
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
                style={{marginTop:"20px"}}
                autoHeight
                rows={rows}
                columns={columns}
                initialState={{
                    pagination: {
                        paginationModel: {
                            pageSize: 20,
                        },
                    },
                }}
                pageSizeOptions={[10, 25]}
                checkboxSelection
                onRowSelectionModelChange={(e) => _handleSelect(e)}
            />
        </Box>
    )
}

export default DocumentsGrid;


