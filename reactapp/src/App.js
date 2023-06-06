import React, {createContext, useState} from "react";
import axios from 'axios';
import './App.css';
import GameList  from './components/GameList.js';
import ClientPage  from './components/ClientPage.js';
import AuthPage  from './components/AuthPage.js';
import {Router, Route, Routes, Link, BrowserRouter} from "react-router-dom";
import Token from "./Models/Client/Token";
import LogInButtons from "../src/components/LoginInButtons";
import View from "./View";
import ProtectedRoutes from "./ProtectedRoutes";
import CreatePage from "./components/CreatePage";

function setToken(userToken) {
    let jsonUserToken = JSON.stringify(userToken);
    sessionStorage.setItem('token', jsonUserToken);
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
function getUserRole() {
    const tokenString = sessionStorage.getItem('token');
    if(tokenString!=null){
        console.log("TOKEN^^^^^^^",tokenString);
        // const userToken = JSON.parse(tokenString);
        // const userToken = JSON.parse(tokenString);
        console.log("Role^^^^^^^^^^^", JSON.parse(tokenString).idRole);
        // console.log(12);
        return JSON.parse(tokenString).idRole
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
    const idRole = getUserRole();
    //const [user, setUser] = useState({loggedIn: false});
    //console.log("Взял ли что-то аааааааааааааааааааа",token);
    // const [token, setToken] = useState();

    if(token===null) {
        console.log(1);
        return <AuthPage setToken={setToken} />
    }
    else{
        if(idRole===1){
            return(
                <BrowserRouter>
                    <nav className="Nav-panel">
                        <Link className="Navbar-item" to="/">Авторизация</Link>
                        <Link className="Navbar-item" to="/main">Главная</Link>
                        <Link className="Navbar-item" to="/client">Клиенты</Link>
                        <Link className="Navbar-item" to="/create">Создать игру</Link>
                    </nav>
                    <Routes>
                        <Route path={'/'} element={<AuthPage setToken={setToken}/>} />
                        <Route >
                            <Route path="/main" element={<GameList />} />
                        </Route>
                        <Route >
                            <Route path="/client" element={<ClientPage />} />
                        </Route>
                        <Route >
                            <Route path="/create" element={<CreatePage />} />
                        </Route>
                    </Routes>
                </BrowserRouter>
            );

        }
        else{
            return (
                <BrowserRouter>
                    <nav>
                        <Link className="Navbar-item" to="/">Авторизация</Link>
                        <Link className="Navbar-item" to="/main">Главная</Link>
                    </nav>
                    <Routes>
                        <Route path={'/'} element={<AuthPage setToken={setToken}/>} />
                        <Route >
                            <Route path="/main" element={<GameList />} />
                        </Route>
                    </Routes>
                </BrowserRouter>
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
                // <UserContext.Provider value={{user, setUser}}>
                //     <Nav/>
                //     <LogInButtons/>
                //     <View/>
                // </UserContext.Provider>
            );
            // }
        }


    }}

export default App;