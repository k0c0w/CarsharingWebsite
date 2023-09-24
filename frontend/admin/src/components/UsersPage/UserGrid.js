import React from 'react';
import Button from '@mui/material/Button';
import { useTheme, Box } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import { tokens } from '../../theme';
import useAuth from '../../hooks/useAuth';
import API from '../../httpclient/axios_client';

function getRoleFromSelect(id){
    const select = document.getElementById(`${id}_select`);
    return select.value;
}

async function revokeRole(id){
    const response = await API.grantRole(id, getRoleFromSelect(id));
    if(!response.successed){
        alert("Ошибка revoke");
    }
    else{ alert("Роль забрана")}
}

async function grantRole(id) {
    const response = await API.revokeRole(id, getRoleFromSelect(id));
    if(!response.successed){
        alert("Ошибка Grant");
    } else {alert("Роль выдана")}
}

function UserGrid({handleClickInfo, handleSelect, handleVerify, rows}) {
    const { auth } = useAuth();
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const isAdmin = () => auth?.roles?.find(role => role == "Admin");

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
            flex: 2,
            menu:false,
            sortable: false,
            renderCell: (params) => {
                return (
                    <>
                        <Button 
                            variant={'contained'} 
                            style={{ backgroundColor: color.primary[100], color: color.primary[900], marginRight: '20px'}}
                            onClick={()=>handleClickInfo(params.row)}
                            >
                            Посмотреть данные
                        </Button>                             
                    </>
                )
            }
        },
        {
            field: 'roles',
            headerName: 'Роль менеджмент',
            sortable: false,
            flex:3,
            renderCell: (params) => {
                const id = params.row.id;
                return (
                    <Box>
                        { isAdmin && <>
                        <select id={`${id}_select`} style={{ backgroundColor: color.primary[900], color: color.primary[100], marginRight: '10px',  }}
                            name="role">
                            <option value="Admin">Admin</option>
                            <option value="Manager">Manager</option>
                        </select>
                        <Button id={`${id}_grant`}
                            variant={'contained'} style={{ backgroundColor:"#228b22", marginRight: '10px'}}
                            onClick={()=>grantRole(params.row.id)}>
                            Выдать
                        </Button>
                        <Button 
                            variant={'contained'} id={`${id}_revoke`}
                            style={{ backgroundColor: "#FF4500", color: color.primary[900]}}
                            onClick={()=>revokeRole(params.row.id)}>
                            Убрать
                        </Button>
                        </>}
                    </Box>
                )
            }
        },
        {
            field: 'verify',
            headerName: 'Подтвердить',
            menu:false,
            sortable: false,
            flex:4,
            renderCell: (params) => {
                const is_verified = params.row.personal_info.is_info_verified;
                return (
                    <Box
                    width="20%"
                    borderRadius={"5px"}
                    sx= {{ height: '30px', width: '10px',  }}
                    >
                        {!is_verified && <Button 
                            variant={'contained'} 
                            style={{ backgroundColor: "orange", color: color.primary[900], marginLeft: 'auto' }}
                            onClick={()=>handleVerify(params.row.id, false)}
                            >
                            Verify info
                        </Button>}
                    </Box>
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


