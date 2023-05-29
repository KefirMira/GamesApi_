import axios from 'axios';

class gamesApi {

    static setAuthorization() {

        if(sessionStorage.getItem('token') == null){
        }
        else{
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + JSON.parse( sessionStorage.getItem('token')).jwtToken;
        }
    }


    static async getGames() {
        return await axios.get("http://localhost:5044/api/Games/all")
            .then(async res => {
                return await res.data;
            })
    }
    static async authorization(login,password){
       //auth.preventDefault()
        console.log(JSON.stringify(login,password))
        return fetch(`http://localhost:5044/api/Clients/authorization`, {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                login,
                password,
            ),
        }).then(async data=> {return await data.json()})
        this.setAuthorization();
    }
}

export default gamesApi