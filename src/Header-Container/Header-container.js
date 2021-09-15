import React from 'react';
import './Header-container.css';
import Header from '../Header/Header';
import Navbar from '../Nav-bar/Nav-bar';
import Searchbox from '../Search-box/Search-box';
import User from '../User/User';

class Headercontainer extends React.Component {

  constructor(props) {
    super(props);
    this.user = {
      userId: 'kolhenil',
      userName: {
        firstName: 'Nilesh',
        lastName: 'Kolhe'
      }
    }
  }

  render() {
    return (
      <div className="header-container">
        <Header />
        <Navbar />
        <Searchbox />
        <User user = {this.user} />
      </div>
    );
  }
}

export default Headercontainer;