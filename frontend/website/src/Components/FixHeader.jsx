import { useEffect } from "react";
import { Outlet } from "react-router-dom";

function fixHeader() {
    const header = document.getElementsByTagName("header")[0];

    if(header && !header.classList.contains("fixed")) {
        header.classList.add("fixed");
    }
}

export default function FixHeader(){
    useEffect(() => {fixHeader()}, []);
    return <Outlet/>;
}