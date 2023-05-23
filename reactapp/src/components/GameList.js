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
                            <li key={game.id}>{game.name}</li>
                        )
                }
            </ul>
        )
    }
}
