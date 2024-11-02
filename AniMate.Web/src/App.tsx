import React from 'react';
import './App.css'
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Home from "./pages/home/Home";
import Login from "./pages/login/Login";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home/>}/>
        <Route path="/login" element={<Login/>}/>
      </Routes>
    </Router>
  );
}

export default App;
