import TextField from '@mui/material/TextField';
import {Box, Button} from '@mui/material';
import { FormControl, FormLabel } from '@mui/material';
import {useTheme} from "@emotion/react";
import {tokens} from "../theme";
import { DataGrid } from '@mui/x-data-grid';
import {useEffect} from "react";
import API from "../httpclient/axios_client";


export default function Documents () {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    async function handleForm (e) {
        e.preventDefault();
        debugger;
        let result = await API.sendDocument(e.target.value);
    };

    var loadDocumentData = () => {
        return 0;
    };

    let rows = [
        { id: 1, name: 'Hello.png' },
        { id: 2, name: 'DataGridPro.docx' },
        { id: 3, name: 'MUI.mp4' },
    ];

    let columns = [
        { field: 'id', headerName: 'Id', flex:1 },
        { field: 'name', headerName: 'Имя файла', flex:2 },
    ];

    // Загружаю данные о документах: загружен/загружается/отменено
    useEffect(() => {
        loadDocumentData()
    }, [])

    return (
        <>
            <h1>
                Documents
            </h1>
            <FormControl>
                <FormLabel>Выберите файл</FormLabel>
                <TextField name={"Document"} type={"file"} hidden variant={'outlined'}></TextField>
                <button title={"Загрузить"} onClick={e => { handleForm(e) }}>Загрузить</button>
            </FormControl>

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
                    disableRowSelectionOnClick
                />
            </Box>
        </>
    )
};

function DocumentButton({ title }) {
    const theme = useTheme();
    const color = tokens(theme?.palette?.mode);

    return (
        <Button disableFocusRipple className="submit" type="submit"
                style={{ backgroundColor: color.grey[100], color: color.grey[900] }}>
            {title}
        </Button>
    )
}