import './App.css';
import Header from './Containers/Header';
import Index  from "./Containers/Index";
import Login from './Containers/Login';
import {BrowserRouter, Routes, Route, Outlet } from 'react-router-dom';
import { useEffect } from 'react';
import Tariffs from './Containers/Tariffs';
import { Registration } from './Containers/Registration';
import { Documents } from './Containers/Documents';
import Profile, { ProfileEdit, ProfileChangePassword } from './Containers/Profiles';
import FixHeader from './Components/FixHeader';
import CarRent from './Containers/CarRent';


function App() {
  useEffect(() => {
    window.addEventListener('scroll', function () {
      let header = document.getElementsByTagName('header')[0];
      if(header) {
        toggleHeader(header);
      }
    })
  }, []);

  return (
    <BrowserRouter>
      <Header/>
      <Routes>
        <Route index exact path="/" element={<Index/>}/>
        <Route exact path="/documents" element={<Documents/>}/>
        <Route path="/tariffs">
          <Route exact path=':tariff' element={<Tariffs/>}/>
          <Route path=':taiff/rent/' element={<FixHeader/>}>
            <Route path=':car' element={<CarRent/>}/>
          </Route>
        </Route>
        <Route element={<FixHeader/>}>
          <Route exact path="/login" element={<Login/>}/>
          <Route exact path="/registration" element={<Registration/>}/>
          <Route path='/profile'>
            <Route exact path='' element={<Profile/>}/>
            <Route exact path='edit' element={<ProfileEdit/>}/>
            <Route exact path='edit/password' element={<ProfileChangePassword/>}/>
          </Route>
        <Route path="*" element={<>404</>}/>
        </Route>
      </Routes>
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
