import { NavLink } from "react-router-dom";
import "../../css/common.css";
import "../../css/header.css";
import { AuthData } from "../Auth/AuthWrapper";

export default function MobileHeaderContent() {
    const { user, logout } = AuthData();
    return (
    <div className="mobile-header-container">
        <div className="flex-container mobile-header-content">
        <div>
            <NavLink to="/" className="logo-content">Drive</NavLink>           
        </div>
        <div>
            <input id="menu-toggle" type="checkbox"/>
            <label htmlFor="menu-toggle">
                <svg width="25" height="18" viewBox="0 0 25 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect width="25" height="2" rx="1" fill="black"></rect>
                    <rect y="8" width="25" height="2" rx="1" fill="black"></rect>
                    <rect y="16" width="25" height="2" rx="1" fill="black"></rect>
                </svg>
            </label>
            <div className="page-overlay"></div>
            <ul className="mobile-menu">
                <li className="mobile-menu-item" style={{marginBottom:"30px"}}>
                    {!user.isAuthenticated && <NavLink to="/login" className="header-content-menu-item-link">Войти</NavLink>}
                    {user.isAuthenticated && <NavLink style={{color:"red", display: "block"}} onClick={(e) => {
            e.preventDefault(); logout();}} >Выйти</NavLink>}
                </li>    
                <label><li className="mobile-menu-item"><NavLink to="/" >Главная</NavLink></li></label>
                <li className="mobile-menu-item"><NavLink to="/documents">Документы</NavLink></li>
                <li className="mobile-menu-item"><NavLink to="/profile">Личный кабинет</NavLink></li>
            </ul>
        </div>
        </div>
    </div>
    );
}