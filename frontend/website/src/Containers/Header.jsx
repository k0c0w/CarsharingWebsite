import DesktopHeaderContent from "../Components/Header/DesktopHeaderContent";
import MobileHeaderContent from "../Components/Header/MobileHeaderContent";

export default function Header() {
    return (
        <header>
            <DesktopHeaderContent/>
            <MobileHeaderContent/>
        </header>)
    ;
}