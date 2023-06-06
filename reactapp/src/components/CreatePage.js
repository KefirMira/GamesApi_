import React, {useState} from "react";
import axios from "axios";
import gamesApi from './gamesApi'
import auth from "../Models/Client/Auth";
import Token from "../Models/Client/Token";
import Auth from "../Models/Client/Auth";
import PropTypes from "prop-types";
import '../style.css';
//import Select from 'react-select'


export default function CreatePage() {

    const [name, setName] = useState();
    //const [poster, setPoster] = useState();
    const [publishing_date, setPublishindDate] = useState();
    const [description, setDescription] = useState();
    const handleSubmit = async e => {
        e.preventDefault();
        try {
            const token = await gamesApi.createGame({
                name,publishing_date , description
            });
            //console.log(token);
            //setToken(token);


            //redirect('/main')
            window.location.replace('http://localhost:3000/main');
        } catch(sd) {
            console.log(sd);
            alert("Неверные данные!")
        }

    }


    //ломает навигацию
    return(
        <div onSubmit={handleSubmit}>
                <p >Введите данные</p>
                <div >
                    <form>
                        <input type="text"   required
                               onChange={e => setName(e.target.value)}></input>
                        <input type="datetime-local"   required
                               onChange={e => setPublishindDate(e.target.value)}></input>
                        <input type="text"   required
                               onChange={e => setDescription(e.target.value)}></input>
                        <button  >Сохранить</button>
                    </form>
                </div>

        </div>
    )

}

// CreatePage.propTypes = {
//     setToken: PropTypes.func.isRequired
// };
