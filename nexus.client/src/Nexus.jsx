import React from "react";
import { DarkLightTheme, SetTheme } from "./js/theme";
import { LabelFocus } from "./js/inputs";
import { LogOut } from "./js/auth";
import { WiMoonAltThirdQuarter } from "react-icons/wi";
import { AiOutlineLoading3Quarters } from "react-icons/ai";
import { SetScroll, SetMessageInput } from "./js/animation";
import { SendMsg, SetFilesInput } from "./js/SendReceive";
import Image from "./images/Avatar.png";
import { BsSendFill } from "react-icons/bs";
import { BsFileEarmarkArrowUpFill } from "react-icons/bs";
import Contact from "./Contact";
import { IoLogOutOutline } from "react-icons/io5";
import { GetContacts, GetCurrentUser, GetMessages, Listen } from "./js/fetchdata";

export default class Nexus extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      currentUser: '',
      data: "",
      count: 0,
      contacts: [],
      messages: [],
      activeReceiver: '',
    };

    var p = new Promise(async () => await SetTheme());

    var px = new Promise(async () => {
      const options = {
        method: "GET",
        headers: {
          "Content-Type": "application/json; charset=utf-8",
        },
      };
      const response = await fetch("user/auth", options);
      const data = await response.json();
      this.setState({ data: data });
      console.log(data);
      if (data != "/nexus") {
        window.location = data;
      }
    });

    var pu = new Promise(async () => {
        let user;

        user = await GetCurrentUser();
    
        this.state.currentUser = user;
        console.log(this.state.currentUser);

        let x;
        x = await GetContacts(user.userName);
        x.forEach(element => {
            this.state.contacts.push(element);
        });
        this.setState({contacts: this.state.contacts});
    });

    var listener = new Promise(async () => {
        Listen(this.state.messages);
    });

    this.SetContacts = this.SetContacts.bind(this);
    this.componentDidMount = this.componentDidMount.bind(this);
    this.Send = this.Send.bind(this);
  }

  componentDidMount = async () => {
    SetScroll();
    SetMessageInput();
    SetFilesInput();
  };

  SetContacts = async (userName) => {
    var elem = document.getElementById(`n-id-${userName}`);
            elem.style.borderColor = 'var(--theme-color-border-focus-i)';
            console.log(elem);
    this.state.activeReceiver = userName;
    this.state.contacts.forEach(item => {
        if(item.userName != userName)
        {
            var elem = document.getElementById(`n-id-${item.userName}`);
            elem.style.borderColor = 'var(--theme-color-border)';

        }
    });

    this.state.messages = await GetMessages("Azure", userName);
  };

  Send = async () => {
    await SendMsg(this.state.currentUser.userName, this.state.activeReceiver)
  }

  render() {
    return this.state.data == "/nexus" ? (
      <>
        <div className="nexus-x-div">
          <div className="contacts-x-div">
            <button className="add-contact-btn">Add Contact</button>
            {
                this.state.contacts.map(element => {
                    return(
                        <Contact
                        key={element.userName}
                        userName={element.userName}
                        lastMessage={element.lastMessage}
                        userAvatar={element.userAvatar}
                        setContacts={this.SetContacts}
                      ></Contact>
                    )
                })
            }
          </div>
          <div className="chat-x-div">
            <div className="input-n-div">
              <textarea
                className="textarea-n"
                type="text"
                id="message-n-input"
              ></textarea>
              <div className="ui-btns-n-div x-div-h">
                <BsSendFill
                  className="send-icon"
                  onClick={this.Send}
                ></BsSendFill>
                <BsFileEarmarkArrowUpFill
                  className="send-file-icon"
                  onClick={() =>
                    document.getElementById("file-n-input").click()
                  }
                ></BsFileEarmarkArrowUpFill>
                <input
                  type="file"
                  id="file-n-input"
                  style={{ display: "none" }}
                ></input>
              </div>
            </div>
          </div>

          <div className="side-x-div">
            <div className="nav-icons-div">
              <IoLogOutOutline
                className="logout-icon"
                onClick={LogOut}
              ></IoLogOutOutline>
              <WiMoonAltThirdQuarter
                className="theme-icon"
                onClick={(e) => {
                  DarkLightTheme();
                  try {
                    if (e.classList.contains("theme-icon")) {
                      e.classList.remove("theme-icon");
                    } else {
                      e.classList.add("theme-icon");
                    }
                  } catch {}
                }}
              ></WiMoonAltThirdQuarter>
            </div>

            <div className="current-user-info">
              <img id="current-user-img" src={'data:image/png;base64,' + this.state.currentUser.userAvatar}></img>
              <p id="current-user-name">{this.state.currentUser.userName}</p>
            </div>
          </div>
        </div>
      </>
    ) : (
      <></>
    );
  }
}
