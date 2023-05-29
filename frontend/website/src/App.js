import './App.css';
import Header from './Containers/Header';
import Index  from "./Containers/Index";
import Login from './Containers/Login';
import PrivateRoute from './Containers/PrivateRoute';
import {BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import {useEffect, useState } from 'react';
import  BeforeTariffs from './Containers/Tariffs';
import { Registration } from './Containers/Registration';
import { Documents } from './Containers/Documents';
import Profile, { ProfileEdit, ProfileChangePassword } from './Containers/Profiles';
import FixHeader from './Components/FixHeader';
import CarRent from './Containers/CarRent';
import API from './httpclient/axios_client';
import Chat from './Containers/Chat';


function App() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    /*API.IsUserAuthorized().then(r => {
      if(r.successed)
        localStorage.setItem('user', true);
      else {
        localStorage.clear();
      }
      setUser(localStorage.getItem('user'));
      });*/


    window.addEventListener('scroll', function () {
      const header = document.getElementsByTagName('header')[0];
      if(header) {
        toggleHeader(header);
      }
    })
  }, []);

  return (
    <BrowserRouter>
      <Header user={user}/>
      <Routes>
        <Route path="/">
          <Route index exact path="" element={<Index/>}/>
          <Route exact path='tariffs/:tariffId' element={<BeforeTariffs/>}/>
          <Route element={<PrivateRoute user={user}/>}>
            <Route path='rent/:modelId' element={<CarRent/>}/>
          </Route>
        </Route>
        <Route exact path="/documents" element={<Documents/>}/>
        <Route element={<FixHeader/>}>  
          <Route exact path="/login" element={<Login setUser={setUser} user={user}/>}/>
          <Route exact path="/registration" element={<Registration/>}/>
          <Route path='/profile' element={<PrivateRoute user={user}/>}>
            <Route exact path='' element={<Profile/>}/>
            <Route exact path='edit' element={<ProfileEdit/>}/>
            <Route exact path='edit/password' element={<ProfileChangePassword/>}/>
          </Route>
        <Route path='/logout' element={<Logout setUser={setUser}/>}/>
        <Route path='/chat' element={<Chat />} />
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

function Logout({setUser}) {
  setUser(null);
    API
    .logout();


  return <Navigate to='/'/>

}

export default App;
