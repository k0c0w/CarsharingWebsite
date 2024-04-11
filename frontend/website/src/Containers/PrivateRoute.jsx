import React from "react";
import { Navigate, Outlet, useLocation, } from "react-router-dom";
import { AuthData } from "../Components/Auth/AuthWrapper";

export default function PrivateRoute({redirectPath='/login', children}) {
    const { user } = AuthData();
    const location = useLocation();
    if(!user.isAuthenticated){
        return  <Navigate to={`${redirectPath}?return_uri=${location.pathname}`} replace/>
    }
    return children ? children : <Outlet/>;
}