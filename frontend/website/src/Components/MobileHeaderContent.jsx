import { NavLink } from "react-router-dom";
import "../css/common.css";

export default function MobileHeaderContent() {
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
            <li className="mobile-menu-item"><NavLink to="/#tariffs" className="header-content-menu-item-link">Выйти</NavLink></li>
            <li className="mobile-menu-item"><NavLink to="/documents" className="header-content-menu-item-link">Документы</NavLink></li>
            <li className="mobile-menu-item"><NavLink to="/profile" className="header-content-menu-item-link">Личный кабинет</NavLink></li>
            </ul>
        </div>
        </div>
    </div>
    );
}