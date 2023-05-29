import DesktopHeaderContent from "../Components/Header/DesktopHeaderContent";
import MobileHeaderContent from "../Components/Header/MobileHeaderContent";

export default function Header(props) {
    return (
        <header>
            <DesktopHeaderContent {...props}/>
            <MobileHeaderContent {...props}/>
        </header>)
    ;
}