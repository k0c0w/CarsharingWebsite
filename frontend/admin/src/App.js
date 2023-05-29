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
import CarParkMngmt from './pages/CarPark';
import Login from './pages/Login';
import API from './httpclient/axios_client';
import RequireAuth from './components/RequireAuth';
import useAuth from './hooks/useAuth';


const _routes = [
  {
    path: '/',
    name: "Главная"
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
    path: '/carpark',
    name: "Автопарк"
  },
  {
    path: '/users',
    name: "Пользователи"
  },
  {
    path: '/login',
    name: "Войти"
  }
]


const _roles = {
  Admin: "Admin",
  Manager: "Manager",
  User: "User"
};


function App() {
  const [path, setPath] = useState(window.location.pathname);
  const [theme, colorMode] = useMode();
  const { auth, setAuth } = useAuth();

  const authorize = () => API.isAdmin().then(r => {
      if(r.successed){
        const roles = r?.data?.roles;
        const isAuthorized = true;
        setAuth({ roles, isAuthorized });

      }
      else{
        const roles = [];
        const isAuthorized = false;
        setAuth({ roles, isAuthorized });
      }
  });

  useEffect(()=>{
      authorize()
  },[])

  
  const handlePath = (path) => setPath(path);

  return (
    <>
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme} >
          <CssBaseline />
          <Header></Header>
          <SideNavBar isAuthorized={auth.isAuthorized} path={path} routes={_routes} handlePath={handlePath}></SideNavBar>
          <div className='Page' >
            <Routes>
              <Route element={<RequireAuth allowedRoles={[_roles.Admin, _roles.Manager]} /> } >
                <Route path='/tariffs' element={<TarrifMngmt />} />
                <Route path='/cars' element={<CarsMngmt /> } />
                <Route path='/users' element={<UserMngmt />} />
                <Route path='/carpark' element={<CarParkMngmt/>} />
              </ Route> 
              <Route path='*' element={<div></div>} />
              <Route path='/login' element={<Login />} />
            </Routes>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </>
  );
}

export default App;
