import './App.css';
import Header from './Containers/Header';
import Index  from "./Containers/Index";
import Login from './Containers/Login';
import {BrowserRouter, Routes, Route } from 'react-router-dom';
import { useEffect } from 'react';
import Tariffs from './Containers/Tariffs';
import { Registration } from './Containers/Registration';
import { CardHolder } from './Components/Card';


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
        <Route exact path="/" element={<Index/>}/>
        <Route path="/tariff" element={<Tariffs/>} />
        <Route exact path="/login" element={<Login/>}/>
        <Route path="/registration" element={<Registration/>}/>
        <Route path="/card" element={<CardHolder/>}/>
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
