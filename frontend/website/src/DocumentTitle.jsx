import { Outlet } from "react-router-dom";
export const DocumentTitle = (title) => {document.title = title.children; return <Outlet/>};
export default DocumentTitle;