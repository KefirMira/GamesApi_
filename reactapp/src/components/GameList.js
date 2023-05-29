import React  from "react";
import axios from "axios";
import gamesApi from './gamesApi'


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

    render() {
        return (
            <ul>
                {
                    this.state.games
                        .map(game =>
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
                }
            </ul>
        )
    }
}
