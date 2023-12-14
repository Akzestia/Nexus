import React from "react";
import { DarkLightTheme, SetTheme } from "./js/theme";
import { LabelFocus } from "./js/inputs";
import { SignUpUser } from "./js/auth";
import { WiMoonAltThirdQuarter } from "react-icons/wi";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

export default class SignUp extends React.Component{


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
        LabelFocus(inputs);
        await SetTheme();
    }



    render() {
        return (
            <>
             <div className="dark-x-div">
                    <AiOutlineLoading3Quarters className="loading-x-icon"/>
                    <p className="loading-x-label">Connecting to server</p>
            </div>
            <WiMoonAltThirdQuarter className="theme-icon-x" onClick={(e) => {
                DarkLightTheme();
                try{
                    if(e.classList.contains('theme-icon-x')){
                        e.classList.remove('theme-icon-x');
                    }
                    else{
                        e.classList.add('theme-icon-x');
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
                        <input type="text" id="UserEmail-X"></input>
                        <label htmlFor="UserEmail-X" className="input-x-label">Email</label>
                    </div>
                   

                    <div className="x-div-v separator-x-div">
                        <input type="password" id="Password-X"></input>
                        <label htmlFor="Password-X" className="input-x-label">Password</label>
                    </div>
                    

                    <div className="x-div-v separator-x-div">
                        <input type="password" id="Password-R"></input>
                        <label htmlFor="Password-R" className="input-x-label">Repeat Password</label>
                    </div>
                    

                    <p className="redirect-x-label">Already have an acoount? <span onClick={() => window.location = "/login"}>Log in</span></p>
                    <button id="theme-x-btn" onClick={SignUpUser}>Sign up</button>
                    {/* <button id="theme-x-btn" onClick={SendMsg}>Send Message</button> */}        
                </div>
            </div>
        </>
        )
    }
}
