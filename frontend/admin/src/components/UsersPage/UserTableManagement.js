import React, { useState } from 'react';
import { useTheme } from '@emotion/react';
import { Box } from '@mui/system';
import { Button } from '@mui/material';
import { TableAddRefreshButtons } from '../TableCommon';
import '../../styles/car-page.css';
import '../../styles/popup.css'
import { tokens } from '../../theme';
import UserGrid from './UserGrid';
import { Popup } from '../Popup';
import { UserForm, UserFormTitle, UserFormSubmit } from './UserForm';
import { UserViewInfo } from './UserViewInfo';
import { getElementsByTagNames } from '../../functions/getElementsByTags';
import API from '../../httpclient/axios_client';





// A component is changing the default value state of an uncontrolled Select after being initialized. To suppress this warning opt to use a controlled Select. ??????


function UserTable({ refreshRows, usersData }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    // selected from data grid of cars
    const [selected, setSelected] = useState([]);
    // Закрытие popup'a
    const [display, setD] = useState('none');
    // Модель формы для popup'а
    const [popupInput, setPopup] = useState(
        {
            title: <UserFormTitle></UserFormTitle>,
            submit: <UserFormSubmit></UserFormSubmit>,
            close: () => setD('none'),
            axiosRequest: () => {},
            inputsModel: <UserForm handler={console.log}></UserForm>,
        }
    );

        const handleVerify = (id) => {
            const response = API.verify_profile(c);
            if(response.successed){
                
            }
        }

    // открывают попап с нужным действием
    const handleClickInfo = (model) => {
        const popup = {
            title: <UserFormTitle></UserFormTitle>,
            close: () => setD('none'),
            inputsModel: <UserViewInfo userModel={model}></UserViewInfo>
        };
        setPopup(popup);
        setD('block');
    }
    const handleClickAdd = () => {
        const popup = {
            title: <UserFormTitle title='Добавить'></UserFormTitle>,
            close: () => setD('none'),
            inputsModel: <UserForm></UserForm>,
            axiosRequest: (e) => API.createUser(e),
            submit: true
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }
    const handleClickChange = () => {
        const popup = {
            title: <UserFormTitle title='Изменить'></UserFormTitle>,
            close: () => setD('none'),
            axiosRequest: (e) => API.createCarModel(e),
            submit: <UserFormSubmit/>,
            inputsModel: <UserForm carModel={selected[0]}></UserForm>,
        };
        setPopup(popup);
        console.log(selected[0]);
        setD('block');
    }

    return (
        <>
            <TableAddRefreshButtons addHandler = {handleClickAdd} refreshHandler={refreshRows}/>

            <UserGrid handleVerify={handleVerify} handleClickInfo={handleClickInfo} handleSelect={(list) => setSelected(list)} rows={usersData}/>

            <Box position="fixed" left={'0%'} top={'0%'} width={'100%'} >
                <footer style={{ opacity: (selected.length === 0 ? 0 : 1) }}>
                    <div style={{ position: 'absolute', left: '80%' }}>
                        <Button
                            disabled={selected.length !== 1}
                            variant={'contained'}
                            style={{ backgroundColor: (selected.length !== 1 ? color.grey[500] : color.primary[100]), color: color.primary[900], marginRight: '20px' }}
                            onClick={() => handleClickChange()}
                        >
                            Изменить
                        </Button>
                        <Button
                            variant={'contained'}
                            disabled={selected.length === 0}
                            style={{ backgroundColor: color.redAccent[200], color: color.primary[900], marginRight: '20px' }}>Удалить</Button>
                    </div>
                </footer>
            </Box>

            <Box style={{ display: display, color: color.grey[100] }}>
                <Popup {...popupInput} > </Popup>
            </Box>
        </>
    )
}

export default UserTable;