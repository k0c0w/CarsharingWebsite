import React from "react";
import "../css/form.css";

export const Input = React.forwardRef((props, ref) => (
    <div className="form-field">
        <input ref={ref} id={props.id} className={`form-input${props.inputErrorMessage?' error':''}`}
             name={props.name} type={props.type} placeholder={props.placeholder} defaultValue={props.value}
             required={props.required}/>
        {props.inputErrorMessage && <p className="form-error">{props.inputErrorMessage}</p>}
        {props.children}
    </div>
));

export const Form = React.forwardRef((props, ref) => {
    const className = `flex-container ${props.className? props.className : ''}`;
    return (
    <form action={props?.action} method={props?.method} ref={ref} className={className}>
        {props.children}
    </form>)
});



export const MyFormProfileInput = (props) => {
    const gap = { columnGap: "100px"};

    return (
    <div id="myFormInput" className="flex-container" style={gap}>
        <div className="flex-container flex-column" style={{order: 0}}>
            {props.leftBlock}
        </div>
        <div className="flex-container flex-column" style={{order: 1}}>
            {props.rightBlock}
        </div>
    </div>);
};

export default Form;