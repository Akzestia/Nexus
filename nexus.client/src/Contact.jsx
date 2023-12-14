import React from "react";

export default class Contact extends React.Component {
  constructor(props) {
    super(props);

    
    this.state = {
      userAvatar: props.userAvatar,
      userName: props.userName,
      lastMessage: props.lastMessage,
      isSelected: false,
    };
  }

  render() {
    return (
      <>
          <div id={"n-id-"+this.state.userName} className="contact-n-div" onClick={() => {
            this.setState({isSelected: !this/this.state.isSelected});
            this.props.setContacts(this.state.userName);
            }}>
            <div className="x-div-h">
              <img src={'data:image/png;base64,' + this.state.userAvatar}></img>
              <div className="x-div-v">
                <p className="user-name">{this.state.userName}</p>
                <p className="last-message">{this.state.lastMessage}</p>
              </div>
            </div>
          </div>
      </>
    );
  }
}
