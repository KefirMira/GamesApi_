import logo from './logo.svg';
import './App.css';
import React from "react";
import FirstPage from "../src/components/FirstPage.js";
import SecondPage from"../src/components/SecondPage.js";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";

function App (){
  return(
      <Router>
        <div className="App">
            <Routes>
                <Route path="/"  exact component={FirstPage}/>
                <Route path="/second" exact component={SecondPage}/>
            </Routes>
        </div>
      </Router>
  )
}
export default App;

