import logo from './logo.svg';
import './App.css';
import React, { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import SideNavBar from './components/SideNavBar';
import CarsMngmt from './pages/Cars';
import { useMode, ColorModeContext } from './theme';
import { ThemeProvider, CssBaseline } from '@mui/material';
import Header from './components/Header';
import TarrifMngmt from './pages/Tarrifs';
import UserMngmt from './pages/Users';

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


function App() {
  const [path, setPath] = useState(window.location.pathname);
  const [theme, colorMode] = useMode();

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
              <Route path='/tariffs' element={<TarrifMngmt />} />
              <Route path='/cars' element={<CarsMngmt />} />
              <Route path='/users' element={<UserMngmt />} />
            </Routes>
          </div>
        </ThemeProvider>
      </ColorModeContext.Provider>
    </>
  );
}

export default App;
