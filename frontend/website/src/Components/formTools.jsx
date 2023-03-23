import React from "react";
import "../css/form.css";

export const Input = (props) => (
    <div className="form-field">
        <input className="form-input" name={props.name} type={props.type} placeholder={props.placeholder} value={props.value}/>
        {props.inputErrorMessage && <p className="form-error">{props.inputErrorMessage}</p>}
    </div>
);


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