import '../styles/popup.css'
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';


export const Popup = ({ close, colorAccent, colorAlt, inputsModel = "", title = "Без названия", submit="" }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    var colorAccent = color.grey[900];
    var colorAlt = color.grey[100];

    return (
        <div class="wrapper" style={{ backgroundColor: colorAccent }}>
            <div className="popup" style={{ position: 'fixed', backgroundColor: colorAccent }}>
                <span>{title}</span>
                <div style={{ display:'flex', justifyContent:'center', justifyItems:'center' }}>
                        {inputsModel}
                </div>
                {submit}
                <label className="close" htmlFor="callback" style={{ color: colorAlt }} onClick={()=>close()}>+</label>
            </div>
        </div>
    )
};