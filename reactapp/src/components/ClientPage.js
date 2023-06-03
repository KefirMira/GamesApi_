import React  from "react";
import axios from "axios";
import gamesApi from './gamesApi'
import {useNavigate} from "react-router-dom";
import Client from"../Models/Client/Client.js"

export default class ClientPage extends React.Component {


    async componentDidMount() {
        this.setState({clients: await gamesApi.getClients()})
        console.log(this.state.clients.toString())
    }
    constructor(props) {
        super(props);
        this.state = {
            clients: []
        };
    }

    // function navigateTo(){
    //     window.location.replace('http://localhost:3000/'+game.name);
    // }


    render() {
        return (
            <ul>
                {
                    this.state.clients
                        .map(client =>
                            <div>

                                <li key={client.id} ><br/>
                                    {client.surname}<br/>{client.name}<br/>
                                    {client.patronymic}</li>
                            </div>
                        )
                }
            </ul>
        )
    }
}
