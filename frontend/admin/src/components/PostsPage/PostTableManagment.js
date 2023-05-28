import React, { useState } from 'react';
import { useTheme } from '@emotion/react';
import { Box } from '@mui/system';
import { Button } from '@mui/material';

import '../../styles/car-page.css';
import '../../styles/popup.css'
import { tokens } from '../../theme';
import { PostGrid } from './PostGrid';
import { Popup } from '../Popup';
import { PostForm,PostFormTitle,PostFormSubmit } from './PostForm';
import { PostViewInfo,PostViewInfoTitle } from './PostViewInfo';
import { getElementsByTagNames } from '../../functions/getElementsByTags';
import API from '../../httpclient/axios_client';
import { TableAddRefreshButtons } from '../TableCommon';


async function send() {
    const elements = getElementsByTagNames("input,textarea", document.getElementById("form"));
    const obj = Object.values(elements).reduce((obj, field) => { obj[field.name] = field.value; return obj }, {});

    var body = JSON.stringify(obj);
    var result = API.getCars(body);
}

export function PostTable({ postData, refreshRows, onDelete }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    // selected from data grid of cars
    const [selected, setSelected] = useState([]);
    // Закрытие popup'a
    const [display, setD] = useState('none');
    // Модель формы для popup'а
    const [popupInput, setPopup] = useState(
        {
            title: <PostFormTitle></PostFormTitle>,
            submit: <PostFormSubmit></PostFormSubmit>,
            close: () => setD('none'),
            axiosRequest: () => {},
            inputsModel: <PostForm handler={console.log}></PostForm>,
        }
    );

    const handleClickInfo = (model) => {
        const popup = {
            title: <PostViewInfoTitle></PostViewInfoTitle>,
            close: () => setD('none'),
            axiosRequest: (data) => API.createPost(data),
            inputsModel: <PostViewInfo postModel={model}></PostViewInfo>,
            submit: false
        };
        setPopup(popup);
        setD('block');
    }

    const handleDelete = async (id) => {
        //todo: modal view to confirm deletion
        const response = await API.deletePost(id);
        if(response.successed){
            onDelete(id);
        }
        else alert("Ошибка удаления.");
    }

    const handleClickAdd = () => {
        const popup = {
            title: <PostViewInfoTitle title='Добавить'></PostViewInfoTitle>,
            close: () => setD('none'),
            axiosRequest: (data) => API.createPost(data),
            submit: <PostFormSubmit handler={send}></PostFormSubmit>,
            inputsModel: <PostForm></PostForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }
    const handleClickEdit = () => {
        const popup = {
            title: <PostFormTitle title='Изменить'></PostFormTitle>,
            close: () => setD('none'),
            submit: <PostFormSubmit></PostFormSubmit>,
            axiosRequest: (body) => API.updatePost(body),
            inputsModel: <PostForm isEdit={true} postModel={selected[0]}></PostForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }
    return (
        <>
            
            <TableAddRefreshButtons addHandler = {handleClickAdd} refreshHandler={refreshRows} color={color}/>
            <PostGrid handleClickInfo={handleClickInfo} handleSelect={(list)=>setSelected(list)} rows={postData}></PostGrid>

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
                            disabled={selected.length === 0} onClick={() => handleDelete(selected[0].id)}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                    </div>
                </footer>
            </Box>

            <Box style={{ display:display, color: color.grey[100] }}>
                <Popup {...popupInput} > </Popup>
            </Box>
        </>
    )
}