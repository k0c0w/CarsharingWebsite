import { tokens } from '../../theme';
import { useTheme } from '@emotion/react';


export function PostViewInfo({ postModel }) {
    const theme = useTheme();
    const style = { display: 'flex', flexDirection: 'column', flexWrap: 'wrap', justifyContent: 'center', color: tokens(theme.palette.mode).grey[100] }



    return (
        <>
            <div style={Object.assign({}, style, {flexDirection: 'row', gap:"30px"})}>
                <div style={style}>
                    <h4>Id:</h4>
                    <div>{postModel.id}</div>
                    <h4>Заголовок:</h4>
                    <div>{postModel.title}</div>
                    <h4>Новость:</h4>
                    <div>{postModel.body}</div>
                    <h4>Дата создания:</h4>
                    <div>{postModel.createdAt}</div>
                </div>
            </div>
        </>
    )
}
export function PostViewInfoTitle({title="Информация"}) {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);


    return (
        <h3 style={{ color: color.grey[100] }}>{title}</h3>
    )
}