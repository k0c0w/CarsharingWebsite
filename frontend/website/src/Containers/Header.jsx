import DesktopHeaderContent from "../Components/DesktopHeaderContent";
import MobileHeaderContent from "../Components/MobileHeaderContent";

export default function Header() {
    return (
        <header>
            <DesktopHeaderContent/>
            <MobileHeaderContent/>
        </header>)
    ;
}