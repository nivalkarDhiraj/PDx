import React from 'react';
import './Search-box.css';
import { searchValueSharedService } from '../Shared-Service/Shared-service';

class Searchbox extends React.Component {

  searchVideos(event) {
    searchValueSharedService.setData(document.getElementById('searchBox').value);
  }

  render() {
    return (
      <div>
          <input id = "searchBox" placeholder = " Search" onKeyUp={this.searchVideos.bind(this)}></input>
      </div>
    );
  }
}

export default Searchbox;