import React, {createContext, useState} from "react";
import axios from 'axios';
import GameList  from './components/GameList.js';
import AuthPage  from './components/AuthPage.js';
import {Router, Route, Routes, Link} from "react-router-dom";
import Token from "./Models/Client/Token";
import LogInButtons from "../src/components/LoginInButtons";
import View from "./View";

function setToken(userToken) {
    console.log(JSON.stringify(userToken))
    sessionStorage.setItem('token', JSON.stringify(userToken));
}

function getToken() {
    const tokenString = sessionStorage.getItem('token');
    if(tokenString!=null){
        console.log("TOKEN^^^^^^^",tokenString);
        // const userToken = JSON.parse(tokenString);
        // const userToken = JSON.parse(tokenString);
        console.log("JWT^^^^^^^^^^^", JSON.parse(tokenString).jwtToken);
        // console.log(12);
        return JSON.parse(tokenString).jwtToken
    }

}


function Nav() {
    return <Nav>
        <div className="Nav-panel">
            <Link className="Navbar-item" to="/">Авторизация</Link>
            <Link className="Navbar-item" to="/main">Главная</Link>
        </div>
    </Nav>;
}
export const UserContext = createContext(this);


function App() {
    const token = getToken();
    const [user, setUser] = useState({loggedIn: false});
    console.log("Взял ли что-то аааааааааааааааааааа",token);
  // const [token, setToken] = useState();
    if(token=== undefined) {
        console.log(1);
        return <AuthPage setToken={setToken} />
    }
    else{
        return (
            // <Router >
            //     <div>
            //         <Nav />
            //         <Routes>
            //             <Route path="/login" element={<AuthPage />} />
            //         </Routes>
            //         <Routes>
            //             <Route path="/" element={<GameList />} />
            //         </Routes>
            //     </div>
            // </Router>
            <UserContext.Provider value={{user, setUser}}>
                <Nav/>
                <LogInButtons/>
                <View/>
            </UserContext.Provider>
        );
    }

}

export default App;
