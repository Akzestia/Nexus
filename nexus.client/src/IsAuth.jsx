import React from "react";



export class Auth extends React.Component{
    componentDidMount = async () => {
        const options = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
            },
        };
        const response = await fetch("user/auth", options);
        const data = await response.json();
        window.location = data;
    }

    render(){
        return(<></>);
    }
}