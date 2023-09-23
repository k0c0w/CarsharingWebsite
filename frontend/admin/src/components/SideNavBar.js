import '../styles/side-navbar.css'
import React from 'react';
import {  Link } from 'react-router-dom';

function SideNavBar({isAuthorized, routes, handlePath, path }) {
    

    return isAuthorized &&
        <div id="mySidenav" className="sidenav">
            <div className='navList'>
                {routes.map((nav) => (
                    <Link key={nav?.name} style={{ color:(path===nav?.path ? "white" : "") }} 
                        to={nav?.path} 
                        onClick={()=>{handlePath(nav?.path)}}> {nav?.name} </Link>
                ))}
            </div>
        </div>
    
}

export default SideNavBar;