import DesktopHeaderContent from "../Components/Header/DesktopHeaderContent";
import MobileHeaderContent from "../Components/Header/MobileHeaderContent";

export default function Header({user}) {
    return (
        <header>
            <DesktopHeaderContent user={user}/>
            <MobileHeaderContent user={user}/>
        </header>)
    ;
}