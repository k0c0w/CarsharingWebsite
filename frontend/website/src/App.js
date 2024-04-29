import './App.css';
import React, { useEffect } from 'react';
import Header from './Containers/Header';
import Index  from "./Containers/Index";
import Login from './Containers/Login';
import PrivateRoute from './Containers/PrivateRoute';
import {BrowserRouter, Routes, Route } from 'react-router-dom';

import  BeforeTariffs from './Containers/Tariffs';
import { Registration } from './Containers/Registration';
import { Documents } from './Containers/Documents';
import Profile, { ProfileEdit, ProfileChangePassword } from './Containers/Profiles';
import FixHeader from './Components/FixHeader';
import CarRent from './Containers/CarRent';
import NotFound from './Containers/NotFound';
import PopupChat from './Containers/PopupChat';
import { AuthWrapper } from './Components/Auth/AuthWrapper';


function App() {
  useEffect(() => {
    window.addEventListener('scroll', function () {
      const header = document.getElementsByTagName('header')[0];
      if(header) {
        toggleHeader(header);
      }
    })
  }, []);

  return (
    <BrowserRouter>
      <AuthWrapper>
        <Header/>
        <Routes>
          <Route path="/">
            <Route index exact path="" element={<Index/>}/>
            <Route exact path='tariffs/:tariffId' element={<BeforeTariffs/>}/>
            <Route element={<PrivateRoute/>}>
              <Route path='rent/:modelId' element={<CarRent/>}/>
            </Route>
          </Route>
          <Route exact path="/documents" element={<Documents/>}/>
          <Route element={<FixHeader/>}>  
            <Route exact path="/login" element={<Login/>}/>
            <Route exact path="/registration" element={<Registration/>}/>
            <Route path='/profile' element={<PrivateRoute/>}>
              <Route exact path='' element={<Profile/>}/>
              <Route exact path='edit' element={<ProfileEdit/>}/>
              <Route exact path='edit/password' element={<ProfileChangePassword/>}/>
            </Route>
          <Route path="*" element={<NotFound/>}/>
          </Route>
        </Routes>
        <PopupChat/>
      </AuthWrapper>
    </BrowserRouter>
  );
}

function toggleHeader(header){
  if (window.scrollY > 20) {
    header.classList.add("fixed");
  } else {
      header.classList.remove("fixed");
  }
}

export default App;
