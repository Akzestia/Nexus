import React from "react";
import { DarkLightTheme, SetTheme } from "./js/theme";
import { LabelFocus } from "./js/inputs";
import { SignUpUser } from "./js/auth";
import { WiMoonAltThirdQuarter } from "react-icons/wi";
import { AiOutlineLoading3Quarters } from "react-icons/ai";

export default class Nexus extends React.Component{


    constructor(props) {
        super(props);

        this.state = {
            data: '',
        }

        var p = new Promise( async () => await SetTheme());

    }

    componentDidMount = async () => {
        
    }


    render() {
        return (
            <>
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
                <button onClick={() => window.location = "/signup"}>Click</button>
            </div>
        </>
        )
    }
}
