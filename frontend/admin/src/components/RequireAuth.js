import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";

const RequireAuth = ({ allowedRoles }) => {
    const { auth } = useAuth();
    const location = useLocation();

    console.log(auth);
    console.log(allowedRoles);
    return (
        auth?.roles?.find(role => allowedRoles?.includes(role))
            ? <Outlet />
            : auth?.isAuthorized
                ? <Navigate to="/unauthorized" replace />
                : <Navigate to="/login" replace />
    );
}

export default RequireAuth;