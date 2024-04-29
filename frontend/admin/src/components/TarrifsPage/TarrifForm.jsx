import React from 'react';
import '../../styles/car-page.css';
import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';
import { styleTextField } from '../../styleComponents';

export function TarrifForm({ carModel: tarrifModel, isEdit }) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const StyledTextField = styleTextField(color.primary[100]);

    return (
        <>
            <div className='inputs' id='form'>
                { isEdit && <input hidden name="id" defaultValue={tarrifModel?.id}></input> }
                <StyledTextField
                    variant="outlined"
                    size='small'
                    label="Название"
                    name='name'
                    defaultValue={tarrifModel?.name}
                >
                </StyledTextField>

                <StyledTextField
                    placeholder={'Цена'}
                    variant="outlined"
                    size='small'
                    label="Цена"
                    border="white"
                    name='price'
                    defaultValue={tarrifModel?.price}
                >
                </StyledTextField>

                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Описание'}
                    label="Описание"
                    fullWidth={true}
                    variant="outlined"
                    name='description'
                    minRows={2} maxRows={10}
                    multiline={true}
                    defaultValue={tarrifModel?.description}
                    >
                </StyledTextField>
                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Мин. время аренды'}
                    label="Минимальное время аренды (минуты)"
                    fullWidth={true}
                    variant="outlined"
                    name='min_rent_minutes'
                    itemType='number'
                    multiline={true}
                    defaultValue={tarrifModel?.min_rent_minutes}
                    >
                </StyledTextField>
                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Макс. время аренды'}
                    label="Максимальное время аренды (минуты)"
                    fullWidth={true}
                    variant="outlined"
                    name='max_rent_minutes'
                    itemType='number'
                    multiline={true}
                    defaultValue={tarrifModel?.max_rent_minutes}
                    >
                </StyledTextField>
                <StyledTextField
                    style={{ border: '25px' }}
                    placeholder={'Макс. пробег'}
                    label="Макс пробег"
                    fullWidth={true}
                    variant="outlined"
                    name='max_millage'
                    itemType='number'
                    multiline={true}
                    defaultValue={tarrifModel?.max_millage}
                    >
                </StyledTextField>
            </div>
        </>
    )
}

export const TarrifFormTitle = ({title='Добавить объект'}) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    return (<h3 style={{ color: color.grey[100] }}>{title}</h3 >);
}

export const TarrifFormSubmit = () => {
}