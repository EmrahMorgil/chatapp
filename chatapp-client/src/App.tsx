import React from 'react';
import './App.css';
import { Routes, Route } from 'react-router-dom';
import Home from './page/Home';
import Login from './page/Login';
import Register from './page/Register';
import { mdlUser } from './Core/Modals/User';
import Protected from './components/Protected';

function App() {

  var activeUser = localStorage.getItem("activeUser");

  return (
    <div>
      <Routes>
        <Route path="/" element={<Protected loggedIn={activeUser ? true : false}><Home /></Protected>} />
        <Route path="/login" element={<Login />} />
        <Route path='/register' element={<Register />} />
      </Routes>
    </div>
  );
}

export default App;
