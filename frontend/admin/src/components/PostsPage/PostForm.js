import React from 'react';
import '../../styles/car-page.css';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';
import Button from "@mui/material/Button";


let handleSubmit = (e) => {
    let inputs = e.target.parentNode.getElementsByTagName('input')
    let result = {};

    Array.from(inputs).forEach(element => {
        let name = element?.name ?? "not exist";
        result[name] = element?.value ?? "not exist";
    });

    return (result);
}

export function PostForm({ postModel, isEdit }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs' id='form'>
                { isEdit && <input hidden name="id" defaultValue={postModel?.id}></input> }
                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Заголовок"
                    name='title'
                    defaultValue={postModel?.title}
                >
                </StyledTextField>

                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Новость"
                    border="white"
                    name='body'
                    defaultValue={postModel?.body}
                >
                </StyledTextField>
                
            </div>
        </>
    )
}

export const PostFormTitle = ({title='Добавить объект'}) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const PostFormSubmit = ({ handler, title='Сделать запрос' }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    return (
        <Button disableFocusRipple className="submit" type="submit"
                style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
                onClick={(e) => handler(handleSubmit(e))}>
            {title}
        </Button>
    );
}
