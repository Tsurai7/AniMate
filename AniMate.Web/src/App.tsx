import React from 'react';
import './App.css'
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import Home from "./pages/home/Home";
import Login from "./pages/login/Login";
import Registration from "./pages/registration/Registration";
import Footer from "./components/footer/Footer";

function App() {
  return (
		<div className="App">
			<Router>
				<Routes>
					<Route path="/welcome" element={<Home/>}/>
					<Route path="/login" element={<Login/>}/>
					<Route path="/registration" element={<Registration/>}/>
				</Routes>
			</Router>

			<Router>
				<Footer/>
			</Router>
		</div>
  );
}

export default App;
