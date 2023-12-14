import React from "react";

export default class Message extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isSended: props.isSended,
      userAvatar: props.userAvatar,
      userName: props.userName,
      messageText: props.messageText,
    };
  }

  render() {
    return (
      <>
        <div
          id={"nx-id-" + this.state.userName}
          className={
            this.state.isSended ? "message-n-div-s" : "message-n-div-r"
          }
        >
          <img
            className="u-border-img"
            src={"data:image/png;base64," + this.state.userAvatar}
          ></img>

          <div className="message-div-x">
            <div className="message-div">
              <div className="x-div-v">
                <p className="username-n">{this.state.userName}</p>
              </div>
            </div>
            <div className="textdiv">
              <p className="message-text-n">{this.state.messageText}</p>
            </div>
          </div>
        </div>
      </>
    );
  }
}
