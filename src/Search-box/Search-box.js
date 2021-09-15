import React from 'react';
import './Search-box.css';
import { searchValueSharedService } from '../Shared-Service/Shared-service';

// function Searchbox() {
//   return (
//     <div>
//         <input placeholder = "Search"></input>
//     </div>
//   );
// }

class Searchbox extends React.Component {

  searchVideos(event) {
    console.log('Event: ', event.key);
    // document.getElementById('searchBox').value;
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