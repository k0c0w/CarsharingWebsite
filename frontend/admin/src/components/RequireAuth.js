import { Navigate, Outlet } from "react-router-dom";
import React from "react"
import useAuth from "../hooks/useAuth";

const RequireAuth = ({ allowedRoles }) => {
    const { auth } = useAuth();

    return (
        auth?.roles?.find(role => allowedRoles?.includes(role))
            ? <Outlet />
            : <Navigate to="/login"/>
    );
}

export default RequireAuth;