import React from "react";
import { DarkLightTheme, SetTheme } from "./js/theme";
import { LabelFocus } from "./js/inputs";
import { SignUpUser } from "./js/auth";
import { WiMoonAltThirdQuarter } from "react-icons/wi";
import { AiOutlineLoading3Quarters } from "react-icons/ai";
import { SetScroll, SetMessageInput } from './js/animation';
import {SendMsg, SetFilesInput} from './js/SendReceive';

import { BsSendFill } from "react-icons/bs";
import { BsFileEarmarkArrowUpFill } from "react-icons/bs";
import Image from './images/Avatar.png'; 

export default class Nexus extends React.Component{


    constructor(props) {
        super(props);

        this.state = {
            data: '',
        }

        var p = new Promise( async () => await SetTheme());

    }

    componentDidMount = async () => {
        SetScroll();
        SetMessageInput();
        SetFilesInput();
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
            {/* <div className="main-x-div">
                <button onClick={() => window.location = "/signup"}>Click</button>
            </div> */}

            <div className="nexus-x-div">
                <div className="contacts-x-div">
                    <div className="contact-n-div">
                        <div className="x-div-h">
                            <img src={Image}></img>
                            <div className="x-div-v">
                                <p className="user-name">Azure</p>
                                <p className="last-message">Last message</p>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="chat-x-div">
                    <div className="input-n-div">
                        <textarea className="textarea-n" type="text" id="message-n-input"></textarea>
                        <div className="ui-btns-n-div x-div-h">
                            <BsSendFill className="send-icon" onClick={() => SendMsg}></BsSendFill>
                            <BsFileEarmarkArrowUpFill className="send-file-icon" onClick={() => document.getElementById('file-n-input').click()}></BsFileEarmarkArrowUpFill>
                            <input type="file" id="file-n-input" style={{display: "none"}}></input>
                        </div>
                    </div>
                </div>

                <div className="side-x-div">

                </div>
            </div>
        </>
        )
    }
}
