

import React from "react";
import ReactDOM from "react-dom";

class SecondPage extends React.Component {
    onclick () {
        window.location.assign('http://localhost:3000/');
    }

    render() {
        return (<button  onClick={(e) => this.onclick(e)}>Back </button>);
    }
}
export default SecondPage;