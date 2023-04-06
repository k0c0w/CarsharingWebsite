import '../styles/popup.css'
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useRef } from 'react';
import { Button } from '@mui/material';
import axiosInstance from '../httpclient/axios_client';

function getFormSettings ({form, endpointUrl, method, additionalData}) {
    debugger
    const finalFormEndpoint = endpointUrl || form.action;
    const finalMethod = method || form.method;
    const data = Array.from(form.elements)
        .filter((element) => element.name)
        .reduce((obj, input) => Object.assign(obj, { [input.name]: input.value }),{});

    if (additionalData) 
        Object.assign(data, additionalData);
    debugger;
    return { finalFormEndpoint, data, finalMethod};
}

function sendForm(formRef, settings) {
    if(formRef){
        const info = getFormSettings({form: formRef.current, method: settings?.method, endpointUrl:settings?.endpointUrl});
        axiosInstance[info.finalMethod](info.finalFormEndpoint, info.data)
        .then(x => alert("Successfuly saved"))
        .catch(err => alert('error'))
    }
}

export const Popup = ({ close, settings, inputsModel = "", 
    title = "Без названия", buttonTitle="Создать" }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);

    const colorAccent = color.grey[900];
    const colorAlt = color.grey[100];
debugger;
    const ref = useRef(null);

    return (
        <div className="wrapper" style={{ backgroundColor: colorAccent }}>
            <div className="popup" style={{ position: 'fixed', backgroundColor: colorAccent }}>
                <span>{title}</span>
                <div style={{ display:'flex', justifyContent:'center', justifyItems:'center' }}>
                    <form ref={ref}>{inputsModel}</form>
                </div>
                <Button disableFocusRipple className="submit" type="submit"
                    style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
                    onClick={(e) => {e.preventDefault(); sendForm(ref, settings);}}>
                        {buttonTitle}
                </Button>
                <label className="close" htmlFor="callback" style={{ color: colorAlt }} onClick={()=>close()}>+</label>
            </div>
        </div>
    )
};