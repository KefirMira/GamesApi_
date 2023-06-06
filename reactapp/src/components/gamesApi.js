import axios from 'axios';

class gamesApi {

    static setAuthorization() {

        if(sessionStorage.getItem('token') == null){
            return false;
        }
        else{
            let beb = 'Bearer ' + JSON.parse( sessionStorage.getItem('token')).jwtToken;
            axios.defaults.headers.common['Authorization'] = beb;
            return true;
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


    static async createGame(name,date,description){
        //auth.preventDefault()
        this.setAuthorization();
        let content =JSON.stringify(name,date,description);
        console.log(JSON.stringify(name,date,description))
        if(this.setAuthorization()){
            return fetch(`http://localhost:5044/api/Games/create`, {
                method: 'POST',
                headers: {
                    Authorization: 'Bearer '+ JSON.parse( sessionStorage.getItem('token')).jwtToken,
                    Accept: 'application/json',
                    "Content-Type": "application/json",
                },
                body: content,
            }).then(async data=> {
                let result = await data.body;
                return result
            })
        }

    }


    //издатели
    static async getPublishers() {
        this.setAuthorization()
        return await axios.get("http://localhost:5044/api/Games/allPublishers")
            .then(async res => {
                if(res.status===200)
                    return await res.data;
                if(res.status===401){
                    await this.refreshToken()
                    this.setAuthorization()
                    axios.get("http://localhost:5044/api/Games/allPublishers")
                        .then(async res => {
                            if(res.status===200)
                                return await res.data;})
                }
            })
    }


}

export default gamesApi