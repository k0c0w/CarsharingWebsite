import '../styles/popup.css'
import { useTheme } from '@emotion/react';
import { tokens } from '../theme';
import { useRef } from 'react';
import { Button } from '@mui/material';
import API from '../httpclient/axios_client';

function getFormSettings ({form, additionalData}) {
    // const finalFormEndpoint = endpointUrl || form.action;
    // const finalMethod = method || form.method;
    // let data = new FormData();
    let file = {}
    var data = Array.from(form.elements)
        .filter( element  => { 
            if (element.type === "file"){
                file = { value: element?.files[0], name: element.name }
                return false
            }
            return true
        })
        .filter((element) => element.name)
        .reduce((obj, input) => Object.assign(obj, { [input.name]: input.value }),{});
    
    // Object.keys(fields).forEach(function(key) {
    //     data.append(key, fields[key]);
    // });

    // data.append(file.name, file)
    data[file.name] = file.value

    console.log(data)
    debugger;

    console.log(data);
    debugger;

    if (additionalData) 
        Object.assign(data, additionalData);
    console.log(data)
    return data;
}

async function sendForm(formRef, axiosRequest) {
    if(formRef){
        const data = getFormSettings({form: formRef.current});
        var result = await axiosRequest(data)
        if (result.successed !== true){
            alert(result.error);
        }
        else {
            // console.log("truee!!!!")
            alert("Saved!");
        }
        // .then(x => alert("Successfuly saved"))
        // .catch(err => alert(err.error))
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