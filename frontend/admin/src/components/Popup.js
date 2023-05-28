import '../styles/popup.css'
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useRef } from 'react';
import { Button } from '@mui/material';
import API from '../httpclient/axios_client';

function getFormSettings ({form, additionalData}) {
    let file = {}
    const data = Array.from(form.elements)
        .filter( element  => { 
            if (element.type === "file"){
                file = { value: element?.files[0], name: element.name }
                return false
            }
            return true
        })
        .filter((element) => element.name)
        .reduce((obj, input) => Object.assign(obj, { [input.name]: input.value }),{});
   
    data[file.name] = file.value

    if (additionalData) 
        Object.assign(data, additionalData);
    return data;
}

async function sendForm(formRef, axiosRequest) {
    if(formRef){
        const data = getFormSettings({form: formRef.current});
        const result = await axiosRequest(data)
        console.log(result)
        if (result.successed !== true){
            alert(result.error);
        }
        else {
            alert("Saved!");
        }
    }
}

export const Popup = ({ close, inputsModel = "", 
    title = "Без названия", buttonTitle="Создать", submit, axiosRequest }) => {
    const theme = useTheme();
    const color = tokens(theme.palette.mode);
    const colorAccent = color.grey[900];
    const colorAlt = color.grey[100];
    const ref = useRef(null);


    return (
        <div className="wrapper" style={{ backgroundColor: colorAccent }}>
            <div className="popup" style={{ position: 'fixed', backgroundColor: colorAccent }}>
                <span>{title}</span>
                <div style={{ display:'flex', justifyContent:'center', justifyItems:'center' }}>
                    <form ref={ref}>{inputsModel}</form>
                </div>
                { submit && <Button disableFocusRipple className="submit" type="submit"
                    style={{ backgroundColor: color.grey[100], color: color.grey[900] }}
                    onClick={(e) => {
                        e.preventDefault(); 
                        sendForm(ref, (e)=> axiosRequest(e));
                        }}>
                        {buttonTitle}
                </Button> }
                <label className="close" htmlFor="callback" style={{ color: colorAlt }} onClick={()=>close()}>+</label>
            </div>
        </div>
    )
};