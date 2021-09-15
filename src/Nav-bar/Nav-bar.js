import './Nav-bar.css';
import { playListIdSharedService } from '../Shared-Service/Shared-service'

function ShowAllVideos() {
  playListIdSharedService.setData('');
}

function ShowCovidVideos() {
  playListIdSharedService.setData('PLogA9DP2_vSekxHP73PXaKD6nbOK56CJw');
}

function Navbar() {
  return (
    <div className="nav-bar-container">
        <div className="video-button" onClick={ShowCovidVideos.bind(this)}>
            Covid-19 Videos
        </div>
        <div className="video-button" onClick={ShowAllVideos.bind(this)}>
            All Videos
        </div>
    </div>
  );
}

export default Navbar;