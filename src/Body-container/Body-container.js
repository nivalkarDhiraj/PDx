import React from 'react';
import './Body-container.css';
import Draggable from 'react-draggable';
import moment from 'moment';
import Videocard from '../Video-card/Video-card';
// import Resizablevideo from '../Resizable-video/Resizable-video'
import { searchValueSharedService, playListIdSharedService, pipSharedService } from '../Shared-Service/Shared-service';

class Bodycontainer extends React.Component {

  constructor(props) {
    super(props);
    this.gapi = 'https://www.googleapis.com/youtube/v3/';
    // this.apiKey = 'AIzaSyD8y7e2UigOwZG-9blGBVM3ComcfRyRyV0';
    this.apiKey = 'AIzaSyDlhenQOZvU2pPITQq6LF5ihNCm4A_Qusw';
    this.playlistId = '';
    
    this.state = {
      videos: [],
      filteredVideos: [],
      togglePip: false,
      pipVideo: null
    };
  }

  Mapper(result) {
    return result.items.map(item => {
      return {
        videoId: (item.snippet.resourceId === undefined ? item.id.videoId : item.snippet.resourceId.videoId),
        thumbnail: {
          height: item.snippet.thumbnails.medium.height,
          width: item.snippet.thumbnails.medium.width,
          url: item.snippet.thumbnails.medium.url
        },
        title: item.snippet.title,
        description: item.snippet.description,
        publishedAt: (moment(item.snippet.publishedAt).format("MMM Do YYYY"))
      }
    });
  }

  ApiCall() {
    const channelId = 'UCL03ygcTgIbe36o2Z7sReuQ'
    const COVID_KEYWORD = 'Covid-19'
    let videoList = [];
    const isCovidVideo = this.playlistId === '' ? false: true;

    const url =  isCovidVideo ? 
                    `${this.gapi}search?key=${this.apiKey}&channelId=${channelId}&part=snippet,id&order=date&maxResults=20&q=${COVID_KEYWORD}`:
                        `${this.gapi}search?key=${this.apiKey}&channelId=${channelId}&part=snippet,id&order=date&maxResults=20`;
                        ;

    fetch(url)
      .then(res => res.json())
      .then(result => {
          videoList = this.Mapper(result);
          if(isCovidVideo === true) {
            const COVID_PODCAST = 'COVID-19 Vaccine Podcast';
            let nonCovidVideoList = [];
            let covidVideoList = [...videoList];
            nonCovidVideoList = videoList.filter(video => {
              if(video.title.indexOf(COVID_PODCAST) === -1) {
                covidVideoList.splice(covidVideoList.indexOf(video),1);
                return video;
              }
              return null;
            });
            console.log('Covid Videos: ', covidVideoList);
            console.log('Non Covid Videos: ', nonCovidVideoList);
            videoList = [...covidVideoList, ...nonCovidVideoList];
          }
          console.log('Videos: ', videoList);
          this.setState({videos: videoList, filteredVideos: videoList});
      });
  }

  componentDidMount() {

    searchValueSharedService.getData().subscribe(value => {
      if(value.searchValue === '') {
        this.setState({filteredVideos: this.state.videos});
        return;
      }
      let filteredVideoList = [];
      if(value.searchValue !== '') {
        filteredVideoList = this.state.videos.filter(video => {
          return video.title.toLowerCase().includes(value.searchValue.toLowerCase())
        });
        this.setState({filteredVideos: filteredVideoList});
      }
    })

    playListIdSharedService.getData().subscribe(value => {
      this.playlistId = value.playlistId;
      this.ApiCall();
    });

    pipSharedService.getData().subscribe(v => {
      this.setState({togglePip: v.togglePip, pipVideo: v.video});
    });

  }

  closeDraggable() {
    this.setState({togglePip: false, pipVideo: null});
  }

  render() {
    return (
      <div className="body-container">
        {
          (this.state.togglePip === true) &&
          (
            <div>
                <Draggable
                  defaultPosition={{x: 740, y: 260}}
                >
                  <div>
                      <span className="close-btn" onClick={this.closeDraggable.bind(this)}>X
                      </span>
                      <div className="box">
                        <iframe
                          title={this.state.pipVideo?.videoId}
                          src={`https://www.youtube.com/embed/${this.state.pipVideo?.videoId}`}
                          frameBorder="0"
                          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                        />
                      </div>
                  </div>
                </Draggable>
            </div>
          )
        }
        {
          this.state.filteredVideos.map((video, index) => {
            return <Videocard key = {index} video = {video}/>
          })
        }
      </div>
    );
  }
}

export default Bodycontainer;