import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';
import Home from './page/Home';
import Login from './page/Login';
import Register from './page/Register';
import Protected from './components/Protected';
import CookieManager from './components/helpers/CookieManager';

function App() {

  var token = CookieManager.getCookie("token");

  return (
    <div>
      <Routes>
        <Route path="/" element={<Protected loggedIn={token ? true : false}><Home /></Protected>} />
        <Route path="/login" element={<Login />} />
        <Route path='/register' element={<Register />} />
      </Routes>
    </div>
  );
}

export default App;
