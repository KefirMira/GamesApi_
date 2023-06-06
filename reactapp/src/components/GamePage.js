import React from "react";
import gamesApi from "./gamesApi";
import game from "../Models/Game/Game";

export default class GamePage extends React.Component {


    async componentDidMount() {
        this.setState({game: await gamesApi.getGame(1)})
        console.log(this.state.game.toString())
    }
    constructor(props) {
        super(props);
        this.state = {
            game: game
        };
    }

    render() {
        return (
            this.state.game(
                <div>

                    <li key={game.id} ><img width={200} height={300} src={"http://localhost:5044/"+game.poster}/><br/>
                        {game.name}<br/>Дата выхода - {game.publicationDate}<br/>
                        Издатель - {game.publishingHouseBlank.name}
                        <br/>Описание - {game.description}</li>


                    <li>Жанры</li>
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
        )
    }



}