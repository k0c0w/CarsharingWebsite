import { createContext, useContext, useEffect, useState } from "react"
import API from "../../httpclient/axios_client";
import { useLocation, useNavigate } from "react-router-dom";

export const getTokenFromSessionStorage = () => sessionStorage.getItem("token");

const AuthContext = createContext();
export const AuthData = () => useContext(AuthContext);

export const AuthWrapper = ({children}) => {
     const location = useLocation();
     const navigate = useNavigate();
     const [ user, setUser ] = useState({ isAuthenticated: false })

     const login = () => {
        setUser({ isAuthenticated: true});
     }

     const logout = () => {
          API.logout();
          setUser({...user, isAuthenticated: false})
     }
     
     const getToken = () => sessionStorage.getItem("token") ?? "";

     useEffect(() => {
          (async function validateToken() {
               const isValidSession = await API.IsUserAuthorized();
               if (isValidSession == false){
                    logout();
                    if (location != "/registration")
                         navigate("/login");
               }
          })();
     }, []);

     return <AuthContext.Provider value={{user, login, logout, getToken}}>
                {children}
            </AuthContext.Provider>;
}