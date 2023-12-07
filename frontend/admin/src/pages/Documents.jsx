import TextField from '@mui/material/TextField';
import {Box, Button} from '@mui/material';
import { FormControl, FormLabel } from '@mui/material';
import {useTheme} from "@emotion/react";
import {tokens} from "../theme";
import { DataGrid } from '@mui/x-data-grid';
import {useEffect, useState} from "react";
import API from "../httpclient/axios_client";
import { TableAddRefreshButtons } from '../components/TableCommon';
import { Popup } from '../components/Popup';
import { styleTextField } from "../styleComponents";


async function send() {
    const elements = getElementsByTagNames("input,textarea", document.getElementById("form"));
    const obj = Object.values(elements).reduce((obj, field) => { obj[field.name] = field.value; return obj }, {});

    var body = JSON.stringify(obj);
    API.getCars(body);
}


export default function Documents () {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const [errorMessage, setErrorMessage] = useState(null);
    const [documents, setDocuments] = useState([]);

    const [popup, setPopup] = useState(
        {
            title: <DocumentFormTitle/>,
            submit: <DocumentFormSubmit/>,
            close: () => setD('none'),
            axiosRequest: () => {},
            inputsModel: <DocumentForm handler={console.log}></DocumentForm>,
        }
    );
    // Закрытие popup'a
    const [display, setD] = useState('none');
    const [selected, setSelected] = useState([]);

    
    useEffect(() => {
        loadDocumentData();
    }, [])

    const onDelete = (guid) => {
        const index = documents.findIndex(x => x.guid == guid);
        if (index !== -1) {
            const data = [...documents]
            data.splice(index, 1);
            setTariffsData(data)
        }
    }

    const handleClickAdd = () => {
        const popup = {
            title: <DocumentFormTitle title='Добавить'/>,
            close: () => setD('none'),
            axiosRequest: (data) => API.sendDocument(data),
            submit: <DocumentFormSubmit handler={send}/>,
            inputsModel: <DocumentForm/>,
        };
        setPopup(popup);
        setD('block');
    }

    const handleDelete = async (id) => {
        const response = await API.deleteDocument(id);
        if(response.successed){
            onDelete(id);
        }
        else alert("Ошибка удаления.");
    }

    const handleClickEdit = () => {
        const popup = {
            title: <DocumentFormTitle title={'Изменить'}/>,
            close: () => setD('none'),
            submit: <DocumentFormSubmit></DocumentFormSubmit>,
            axiosRequest: (body) => alert("Not implemented!"),
            inputsModel: <DocumentForm isEdit={true} document={selected[0]}></DocumentForm>,
        };
        setPopup(popup);
        setD('block');
    }

    const onStateUpdate = (guid, state) => {
        const index = documents.findIndex(x => x.guid == guid);
        if (index !== -1) {
            const data = [...documents]
            data[index] = Object.assign({}, data[index], { isPublic: state });
            setDocuments(data)
        }
    } 

    const loadDocumentData = async () => {
        const response = await API.getAllDocuments();
        if (response.successed){
            setDocuments(response.data.map((x,i) => {return { id:i+1, name: x.file_name, annotation: x.annotation ?? "", isPublic: !x.isPrivate, creation: new Date(x.date), guid: x.id }}));
        }
        else{
            setErrorMessage(response.errorMessage);
        }
    };

    const handleSwitch = async (documentId, state) => {
        const result = await API.makeDocumentPublic(documentId, state);
        if(result.successed){
            onStateUpdate(documentId, state);
        }
    }

    const _handleSelect = async (listId) => {
        const result = []
        listId.forEach(id => {
            rows.forEach( row => {
                if (row.guid == id) {
                    result.push(row);
                }
            })
        })

        handleSelect(result);
    }

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
                            onClick={()=>handleSwitch(params.row.guid, false)}
                            >
                            Public
                        </Button>}
                        {!is_public &&<Button 
                            variant={'contained'} 
                            style={{ backgroundColor: "red", color: color.primary[900], marginRight: 'auto' }}
                            onClick={()=>handleSwitch(params.row.guid, true)}
                            >
                            Private
                        </Button>}
                    </Box>
                )
            }
        },
        { field: 'creation', headerName: 'Дата создания', flex:5 },
    ];


    return (
        <>
            <h1>
                Documents
            </h1>
            {errorMessage && <h3 style={{color: 'red'}}>{errorMessage}</h3>}
            <FormControl>
                
            </FormControl>
            <TableAddRefreshButtons addHandler = {handleClickAdd} refreshHandler={() => loadDocumentData()} color={color}/>

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
                    rows={documents}
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
                    onRowSelectionModelChange={(e)=>_handleSelect(e)}
                />
            </Box>

            <Box position="fixed" left={'0%'} top={'0%'} width={'100%'} >
                <footer style={{ opacity: (selected.length === 0 ? 0 : 1) }}>
                    <div style={{ position: 'absolute', left: '80%' }}>
                        <Button
                            disabled={selected.length !== 1}
                            variant={'contained'}
                            style={{ backgroundColor: (selected.length !== 1 ? color.grey[500] : color.primary[100]), color: color.primary[900], marginRight: '20px' }}
                            onClick={()=>handleClickEdit()}
                            >
                            Изменить
                        </Button>
                        <Button
                            variant={'contained'}
                            disabled={selected.length === 0} onClick={() => handleDelete(selected[0].guid)}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                    </div>
                </footer>
            </Box>


            <Box style={{ display:display, color: color.grey[100] }}>
                <Popup {...popup} />
            </Box>
        </>
    )
};

function DocumentForm() {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs' id='documentForm'>
                <StyledTextField placeholder={'Аннотация'}
                    variant="outlined"
                    size='small'
                    label="Аннотация"
                    border="white"
                    name='annotation'/>
                <select name="isPublic">
                    <option value={true}>Public</option>
                    <option value={false}>Private</option>
                </select>
                <FormLabel>Выберите файл</FormLabel>
                <TextField name={"Document"} type={"file"} variant={'outlined'}/>
            </div>
        </>
    )
}

const DocumentFormTitle = ({title='Загрузить документ'}) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

const DocumentFormSubmit = () => {
}

export function TarrifViewInfo({ tarrifModel }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (
        <>
            <div style={{ display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: color.grey[100] }}>
                <h4>Id:</h4>
                <div>{tarrifModel?.id}</div>
                <hr style={hrStyle}/>
                <h4>Название:</h4>
                <div>{tarrifModel?.name}</div>
                <hr  style={hrStyle}/>
                <h4>Макс. Пробег:</h4>
                <div>{tarrifModel?.max_millage}</div>
                <hr  style={hrStyle}/>
                <h4>Стоимость:</h4>
                <div>{tarrifModel?.price}</div>
                <hr style={hrStyle}/>
                <h4>Статус:</h4>
                <div>{tarrifModel?.is_active ? "Активен" : "Отключен"}</div>
                <hr style={hrStyle}/>
                <h4>Описание:</h4>
                <div>{tarrifModel?.description}</div>
            </div>
        </>
    )
}