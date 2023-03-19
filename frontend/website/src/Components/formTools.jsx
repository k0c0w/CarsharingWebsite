import React from "react";
import "../css/form.css";

export const Input = (props) => (
    <div className="form-field">
        <input className="form-input" name={props.name} type={props.type} placeholder={props.placeholder}/>
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

export default Form;