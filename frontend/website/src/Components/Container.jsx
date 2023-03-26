export const Container = (props) => (<div style={props?.style} className={`container container--full ${props.className?props.className:""}`}>{props.children}</div>);
export default Container;