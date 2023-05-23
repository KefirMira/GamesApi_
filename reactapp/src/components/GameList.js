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
                                <li key={game.id}><img width={200} height={300} src={"http://localhost:5044/"+game.poster}/>
                                    {game.name}<br/>Дата выхода - {game.publicationDate}<br/>
                                    Издетель - {game.publishingHouseBlank.name}</li>
                                {/*<ul>*/}
                                {/*    {*/}
                                {/*        this.state.genres.map(genre=><li key={genre.id}>{genre.name}</li>)*/}
                                {/*    }*/}
                                {/*</ul>*/}
                            </div>
                        )
                }
            </ul>
        )
    }
}
