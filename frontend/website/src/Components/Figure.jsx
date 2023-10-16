import "../css/common.css";

export const Figure = (props) => {
    return (
    <div /*figure={props?.figureName}*/ className={`flex-container ${props.className?props.className:""}`} style={props?.style}>
        {props?.children}
    </div>)
};

export default Figure;