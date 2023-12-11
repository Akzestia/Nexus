import React from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { DarkLightTheme, SetTheme } from "./js/theme";
import { LabelFocus, LabelFocusSmall } from "./js/inputs";
import { SignUpUser, LoginUser } from "./js/auth";
import { WiMoonAltThirdQuarter } from "react-icons/wi";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

export default class Login extends React.Component{


    constructor(props) {
        super(props);

        this.state = {
            data: '',
            navigate: props.navigate,
        }

        var p = new Promise( async () => await SetTheme());
    }

    componentDidMount = async () => {

        var inputs = document.querySelectorAll('input');
        LabelFocusSmall(inputs);
    }



    render() {
        return (
            <>
                <div className="dark-x-div">
                    <AiOutlineLoading3Quarters className="loading-x-icon"/>
                    <p className="loading-x-label">Connecting to server</p>
                </div>
                <WiMoonAltThirdQuarter className="theme-icon" onClick={(e) => {
                    DarkLightTheme();
                    try{
                        if(e.classList.contains('theme-icon')){
                            e.classList.remove('theme-icon');
                        }
                        else{
                            e.classList.add('theme-icon');
                        }
                    }
                    catch{}
                   
                   
                }}></WiMoonAltThirdQuarter>
                <div className="main-x-div">
                    <div className="login-x-div x-div-v">
                        <p>Nexus</p>
                        <div className="x-div-v separator-x-div">
                            <input type="text" id="UserName-X"></input>
                            <label htmlFor="UserName-X" className="input-x-label">UserName</label>
                        </div>

                        <div className="x-div-v separator-x-div">
                            <input type="password" id="Password-X"></input>
                            <label htmlFor="Password-X" className="input-x-label">Password</label>
                        </div>

                        <p className="redirect-x-label">Don't have an acoount? <span onClick={() => window.location = "/signup"}>Sign up</span></p>
                        <button id="theme-x-btn" onClick={LoginUser}>Log in</button>      
                    </div>
                </div>
            </>
        )
    }
}

export function LoginX() {
    const navigate = useNavigate();

    const location = useLocation();

    return <Login location={location} navigate={navigate}></Login>
}