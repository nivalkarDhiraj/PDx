import React from 'react'
import './App.css';
import Bodycontainer from './Body-container/Body-container';
import Headercontainer from './Header-Container/Header-container';

class Container extends React.Component {

  render() {
    return (
        <div className="container">
          <Headercontainer/>
          <Bodycontainer/>
        </div>
    );
  }
}

export default Container;
