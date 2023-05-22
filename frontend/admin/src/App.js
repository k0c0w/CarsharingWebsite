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

var routes = [
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
    path: '/users',
    name: "Пользователи"
  }
]


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
  const [user, setUser] = useState(null);

  API.isAdmin().then(r => {
      if(r.successed)
        localStorage.setItem('user', true);
      else {
        localStorage.clear();
      }
      setUser(localStorage.getItem('user'));
  });


  window.addEventListener('scroll', function () {
      const header = document.getElementsByTagName('header')[0];
      if(header) {
        toggleHeader(header);
      }
  })

  
  var handlePath = (path) => setPath(path);

  return (
    <>
      <ColorModeContext.Provider value={colorMode}>
        <ThemeProvider theme={theme} >
          <CssBaseline />
          <Header></Header>
          <SideNavBar path={path} routes={routes} handlePath={handlePath}></SideNavBar>
          <div className='Page' >
            <Routes>
              <Route path='/tariffs' element={<ProtectedRoute user={user}> <TarrifMngmt />  </ProtectedRoute>} />
              <Route path='/cars' element={<ProtectedRoute user={user}> <CarsMngmt /> </ProtectedRoute>} />
              <Route path='/users' element={<ProtectedRoute user={user}> <UserMngmt /> </ProtectedRoute>} />
              <Route path='/login' element={<Login />} />
            </Routes>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </>
  );
}

export default App;
