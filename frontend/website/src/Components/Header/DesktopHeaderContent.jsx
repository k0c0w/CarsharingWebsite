import { NavLink } from "react-router-dom";
import "../../css/common.css";
import Container from "../Container";
import { AuthData } from "../Auth/AuthWrapper";


export const DesktopHeaderContent = () => {
    const { user, logout } = AuthData();

    return (
        <Container className="">
            <div className="flex-container header-content">
                <div>
                    <NavLink to="/" className="logo-content">Drive</NavLink>           
                </div>
                <ul className="flex-container header-content-menu">
                    <li className="header-content-menu-item"><NavLink to="/#tariffs" className="header-content-menu-item-link">Тарифы</NavLink></li>
                    <li className="header-content-menu-item"><NavLink to="/documents" className="header-content-menu-item-link">Документы и новости</NavLink></li>
                    <li className="header-content-menu-item"><NavLink to="/profile" className="header-content-menu-item-link">Личный кабинет</NavLink></li>
                </ul>
                {!user.isAuthenticated && <NavLink to="/login" className="header-content-menu-item-link">Войти</NavLink>}
                {user.isAuthenticated && <NavLink className="header-content-menu-item-link" onClick={(e) => {
                    e.preventDefault(); logout();}}>Выйти</NavLink>}
            </div>
        </Container>
    );
}

export default DesktopHeaderContent;