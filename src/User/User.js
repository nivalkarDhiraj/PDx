import React from 'react';
import './User.css';

class User extends React.Component {

  render() {
    return (
      <div className="user">
        <label>Welcome </label>
        &nbsp;{this.props.user.userName.firstName + ' ' + this.props.user.userName.lastName}
      </div>
    );
  }
}

export default User;