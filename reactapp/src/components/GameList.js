import React  from "react";
import axios from "axios";
import gamesApi from './gamesApi'
import game from "../Models/Game/Game";
import {useNavigate} from "react-router-dom";


export default class GameList extends React.Component {


    async componentDidMount() {
        this.setState({games: await gamesApi.getGames()})
        console.log(this.state.games.toString())
    }
    constructor(props) {
        super(props);
        this.state = {
            games: []
        };
    }

    // function navigateTo(){
    //     window.location.replace('http://localhost:3000/'+game.name);
    // }

    navigateTo = (e) =>window.location.replace('http://localhost:3000/main/'+e.target.value);

    render() {
        return (
            <ul>
                {
                    this.state.games
                        .map(game =>
                            <div>
                                <button onClick={e=>this.navigateTo(e, "game.name")} value={game.id} type="button" >
                                    <li key={game.id} >{game.name}</li>
                                </button>
                                 <li key={game.id} ><img width={400} height={300} src={"http://localhost:5044/"+game.poster}/><br/>
                                    {game.name}<br/>Дата выхода - {game.publicationDate}<br/>
                                    Издатель - {game.publishingHouseBlank.name}
                                    <br/>Описание - {game.description}</li>


                                <b>Жанры</b>
                                <ul>
                                    {
                                        game.genreBlanks.map(genre=><li key={genre.id}>{genre.name}</li>)
                                    }
                                </ul>
                                <li>Разработчики</li>
                                <ul>
                                    {
                                        game.developerBlanks.map(developer=><li key={developer.id}>{developer.name}</li>)
                                    }
                                </ul>
                            </div>
                        )
                }
            </ul>
        )
    }
}
