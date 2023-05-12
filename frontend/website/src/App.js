import './App.css';
import Header from './Containers/Header';
import Index  from "./Containers/Index";
import Login from './Containers/Login';
import {BrowserRouter, Routes, Route, Outlet } from 'react-router-dom';
import { Fragment, useEffect, useState } from 'react';
import  BeforeTariffs from './Containers/Tariffs';
import { Registration } from './Containers/Registration';
import { Documents } from './Containers/Documents';
import Profile, { ProfileEdit, ProfileChangePassword } from './Containers/Profiles';
import FixHeader from './Components/FixHeader';
import CarRent from './Containers/CarRent';
import { getDataFromEndpoint } from './httpclient/axios_client';


function LoadContextData({endpoint}) {
  const [info, setInfo] = useState([]);
  const [madeRequest, setMadeRequest] = useState(false);
  useEffect(() => {
    getDataFromEndpoint(endpoint, setInfo)
  }, []);
  return <Outlet context={info} />;
}


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
        <Route path="/" element={<LoadContextData endpoint={"/tariff/tariffs"}/>}>
          <Route index exact path="" element={<Index/>}/>
          <Route path="tariffs">
            <Route exact path=':tariffName' element={<BeforeTariffs/>}/>
            <Route path=':taiffName/rent/:car' element={<CarRent/>}/>
          </Route>
        </Route>
        <Route exact path="/documents" element={<Documents/>}/>
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
