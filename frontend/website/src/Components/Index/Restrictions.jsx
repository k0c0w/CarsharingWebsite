import Bold, {Dim} from "../TextTags";

const style = {
    justifyContent: "space-between",
    columnGap: "10vw",
}

export const Restrictions = (props) => (
    <div className="flex-container" style={style}>
        <Bold>{props?.title}</Bold>
        <Dim>{props?.children}</Dim>
    </div>
);

export default Restrictions;