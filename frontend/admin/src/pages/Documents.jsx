import TextField from '@mui/material/TextField';
import {Box, Button} from '@mui/material';
import { FormControl, FormLabel } from '@mui/material';
import {useTheme} from "@emotion/react";
import {tokens} from "../theme";
import { DataGrid } from '@mui/x-data-grid';
import React, {useEffect, useState} from "react";
import API from "../httpclient/axios_client";
import { TableAddRefreshButtons } from '../components/TableCommon';
import { Popup } from '../components/Popup';
import { styleTextField } from "../styleComponents";
import DocumentsGrid from "../components/DocumentsPage/DocumentsGrid";


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
    const [documents, setDocuments] = useState([{ id:"1", name: "lol", annotation: "__lol__", creation:"" }]);
    const [selectedDocuments, setSelectedDocuments] = useState([]);

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
            setDocuments(data)
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
        debugger;
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
        const index = documents.findIndex(x => x.guid === guid);
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

    return (
        <>
            <h1>
                Documents
            </h1>
            {errorMessage && <h3 style={{color: 'red'}}>{errorMessage}</h3>}
            <FormControl>
                
            </FormControl>
            <TableAddRefreshButtons addHandler = {handleClickAdd} refreshHandler={() => loadDocumentData()} color={color}/>

            <DocumentsGrid handleSelect={(selectedDocuments)=>setSelectedDocuments(selectedDocuments)} rows={documents} ></DocumentsGrid>

            <Box style={{ display:display, color: color.grey[100] }}>
                <Popup {...popup} />
            </Box>

            <Box position="fixed" left={'0%'} top={'0%'} width={'100%'} >
                <footer style={{ opacity: (selectedDocuments.length === 0 ? 0 : 1) }}>
                    <div style={{ position: 'absolute', left: '80%' }}>
                        <Button
                            disabled={selectedDocuments.length !== 1}
                            variant={'contained'}
                            style={{ backgroundColor: (selectedDocuments.length !== 1 ? color.grey[500] : color.primary[100]), color: color.primary[900], marginRight: '20px' }}
                            onClick={()=>handleClickEdit()}
                        >
                            Изменить
                        </Button>
                        <Button
                            variant={'contained'}
                            disabled={selectedDocuments.length === 0} onClick={() => handleDelete(selectedDocuments[0].id)}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                    </div>
                </footer>
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


// Footer для удаления и изменения данных из таблицы

