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
        this.setAuthorization()
        return await axios.get("http://localhost:5044/api/Games/all")
            .then(async res => {
                if(res.status===200)
                    return await res.data;
                if(res.status===401){
                    await this.refreshToken()
                    this.setAuthorization()
                    axios.get("http://localhost:5044/api/Games/all")
                        .then(async res => {
                            if(res.status===200)
                                return await res.data;})
                }
            })
    }

    static async getGame(id) {
        this.setAuthorization()
        return await axios.get("http://localhost:5044/api/Games/"+id)
            .then(async res => {
                if(res.status===200)
                    return await res.data;
                else { await this.refreshToken()
                    this.setAuthorization()
                    axios.get("http://localhost:5044/api/Games/"+id)
                        .then(async res => {
                            if(res.status===200)
                                return await res.data;
                        })
                }
            })
    }



    static async getClients() {
        this.setAuthorization()
        return await axios.get("http://localhost:5044/api/Clients/all")
            .then(async res => {
                if(res.status===200)
                    return await res.data;
                if(res.status===401){
                    await this.refreshToken()
                    this.setAuthorization()
                    axios.get("http://localhost:5044/api/Clients/all")
                        .then(async res => {
                            if(res.status===200)
                                return await res.data;})
                }
            })
    }

    static setToken(userToken) {
        sessionStorage.clear()
        let jsonUserToken = JSON.stringify(userToken);
        sessionStorage.setItem('token', jsonUserToken);
    }

    static async refreshToken() {
        const tokenString = sessionStorage.getItem('token');
        console.log(JSON.parse(tokenString).refreshToken)
        return fetch(`http://localhost:5044/api/Clients/refreshToken`, {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
                JSON.parse(tokenString).refreshToken
            ),
        }).then(async data=> {
            let result = await data.json();
            await this.setToken(result);
            this.setAuthorization()
            return result
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
        }).then(async data=> {
            let result = await data.json();
            return result
        })
        this.setAuthorization();
    }
}

export default gamesApi