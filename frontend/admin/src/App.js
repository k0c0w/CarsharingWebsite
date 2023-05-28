import logo from './logo.svg';
import './App.css';
import React, { useEffect, useState,  } from 'react';
import { Route, Routes, Navigate } from 'react-router-dom';
import SideNavBar from './components/SideNavBar';
import CarsMngmt from './pages/Cars';
import { useMode, ColorModeContext } from './theme';
import { ThemeProvider, CssBaseline } from '@mui/material';
import Header from './components/Header';
import TarrifMngmt from './pages/Tarrifs';
import UserMngmt from './pages/Users';
import Login from './pages/Login';
import API from './httpclient/axios_client';
import RequireAuth from './components/RequireAuth';
import Chats from './pages/Chats';
import Chat from './pages/Chat';
import useAuth from './hooks/useAuth';

const _routes = [
  {
    path: '/',
    name: "Чаты"
  },
  {
    path: '/cars',
    name: "Машины"
  },
  {
    path: '/tariffs',
    name: "Тарифы"
  },
  {
    path: '/users',
    name: "Пользователи"
  }
]


const _roles = {
  Admin: "Admin",
  Manager: "Manager",
  User: "User"
};


function toggleHeader(header){
  if (window.scrollY > 20) {
    header.classList.add("fixed");
  } else {
      header.classList.remove("fixed");
  }
}

const ProtectedRoute = ({ user, children }) => {
  if (!user) {
    return <Navigate to="/login" replace />;
  }

  return children;
};


function App() {
  const [path, setPath] = useState(window.location.pathname);
  const [theme, colorMode] = useMode();
  const { setAuth } = useAuth();

  var authorize = () => API.isAdmin().then(r => {
      if(r.successed){
        var roles = r?.data?.roles;
        var isAuthorized = true;
        setAuth({ roles, isAuthorized });
        debugger;
      }
      else{
        var roles = [];
        var isAuthorized = false;
        setAuth({ roles, isAuthorized });
      }
  });

  useEffect(()=>{
      authorize()
  },[])

  
  var handlePath = (path) => setPath(path);

  return (
    <>
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme} >
          <CssBaseline />
          <Header></Header>
          <SideNavBar path={path} routes={_routes} handlePath={handlePath}></SideNavBar>
          <div className='Page' >
            <Routes>
              <Route element={<RequireAuth allowedRoles={[_roles.Admin, _roles.Manager]} /> } >
                <Route path='/tariffs' element={<TarrifMngmt />} />
                <Route path='/cars' element={<CarsMngmt /> } />
                <Route path='/users' element={<UserMngmt />} />
                <Route path="/chat" element={<Chat />} />
                <Route path='*' element={<Chats />} />
              </ Route> 
              <Route path='/login' element={<Login />} />
              <Route path='/unauthorized' element={<div>Не авторизован</div>} />
            </Routes>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </>
  );
}

export default App;
