import "../css/text.css";

export default function Bold (props) {
    const style = {
        fontSize: props?.fontSize,
        color: props?.color,
        fontWeight : props?.fontWeight,
        fontFamily: props?.fontFamily
    }

    return (
    <div style={style} className={`bold-text ${props.className ? props.className : ""}`}>{props.children}</div>);
}

export const Dim = (props) => (<div className={`dim ${props.className ? props.className : ""}`}>{props.children}</div>);