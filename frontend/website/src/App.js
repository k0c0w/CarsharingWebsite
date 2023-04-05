import './App.css';
import Header from './Containers/Header';
import Index  from "./Containers/Index";
import Login from './Containers/Login';
import {BrowserRouter, Routes, Route, Outlet } from 'react-router-dom';
import { useEffect, useState } from 'react';
import  BeforeTariffs from './Containers/Tariffs';
import { Registration } from './Containers/Registration';
import { Documents } from './Containers/Documents';
import Profile, { ProfileEdit, ProfileChangePassword } from './Containers/Profiles';
import FixHeader from './Components/FixHeader';
import CarRent from './Containers/CarRent';
import { getDataFromEndpoint } from './httpclient/axios_client';



function App() {
  useEffect(() => {
    window.addEventListener('scroll', function () {
      let header = document.getElementsByTagName('header')[0];
      if(header) {
        toggleHeader(header);
      }
    })
  }, []);

  const [tariffsData, setTariffsData] = useState([]);
  const [madeRequest, setMadeRequest] = useState(false);
  useEffect(() => { if(!madeRequest) getDataFromEndpoint("tariff/tariffs", setTariffsData)}, []);

  return (
    <BrowserRouter>
      <Header/>
      <Routes>
        <Route index exact path="/" element={<Index tariffsData={tariffsData}/>}/>
        <Route exact path="/documents" element={<Documents/>}/>
        <Route path="/tariffs">
          <Route exact path=':tariffName' element={<BeforeTariffs tariffsData={tariffsData}/>}/>
          <Route path=':taiffName/rent/' element={<FixHeader/>}>
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
