import axios from 'axios';

class gamesApi {

    static async getGames() {
        return await axios.get("http://localhost:5044/api/Games/all")
            .then(async res => {
                return await res.data;
            })
    }
}

export default gamesApi