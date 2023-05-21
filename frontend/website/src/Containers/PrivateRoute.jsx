import React from "react";
import { Navigate, Outlet, useLocation, } from "react-router-dom";

export default function PrivateRoute({user, redirectPath='/login', children}) {
    const location = useLocation();
    if(!user){
        return  <Navigate to={`${redirectPath}?return_uri=${location.pathname}`} replace/>
    }
    return children ? children : <Outlet/>;
}