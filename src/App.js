import React, { Component } from 'react';
import NavBar from './NavBar/NavBar'
import YouTubeList from './YouTubeList/YouTubeList';
import youtube from './api/youtube';
import { Container, Row, Col } from 'react-bootstrap'
import YouTubeDetails from './YouTubeDetails/YouTubeDetails';
import ErrorLogo from './assests/errorimg.PNG'
import Loadinglogo from './assests/Loading-bar.gif'


class App extends Component {
  constructor(props) {
    super(props);
    this.loadDataAxios('COVID');
  }
  state = {
    videos: [],
    selectedVideo: null,
    statusCode: null,
    loading: true
  }
  // This function will scroll the window to the top 
  scrollToTop() {
    window.scrollTo({
      top: 0,
      behavior: "smooth"
    });
  }

  //on load call
  loadDataAxios = async (termFromSearchBar) => {
       try {
      const response  = await youtube.get('/search', {
        params: {
          q: termFromSearchBar
        }
      })

      this.setState({
        videos: response.data.items,
        statusCode: response.status,
        selectedVideo: response.data.items[0],
        loading: false
      })

    }
    catch {
      this.setState({
        loading: false
      })
    }
  };

  //function used for search submit
  handleSubmit = async (textForSearch) => {
    const response = await youtube.get('/search', {
      params: {
        q: textForSearch,
        order: 'date'
      }
    })

    this.setState({
      videos: response.data.items,
      selectedVideo: response.data.items[0] //default select 1st video
    })
  };

  //fuction call when video is selected from card
  handleVideoSelect = (video) => {

    this.setState({ selectedVideo: video })
    this.scrollToTop();

  }


  render() {

    //check status code and loading icon
    if (this.state.statusCode !== 200 || this.state.statusCode == null) {
      return (

        <Container>
          {this.state.loading ? <header className="App-header">
            <img src={Loadinglogo} alt="" /></header> : [
            <><img src={ErrorLogo} width="100%" height="100%" alt="" />
              <h1 classnName="text-center"><strong>Status Code:</strong>{this.state.statusCode ? this.state.statusCode : 'API key is expired 403'}</h1>
            </>]
          }
        </Container>
      )
    }

    return (

      <div className="App">

        <NavBar handleFormSubmit={this.handleSubmit} />
        <br></br>

        <Container>
          {
            this.state.videos <= 0 ?
              <h2>
                <strong>No Result Found</strong>

              </h2> : <h2> </h2>

          }
          <Row>
            <Col sm={7}>
              <YouTubeDetails video={this.state.selectedVideo} />
            </Col>
            <Col sm={5}>
              
              <YouTubeList handleVideoSelect={this.handleVideoSelect} videos={this.state.videos} />
            </Col>
          </Row>
        </Container>

      </div>
    );
  }
}

export default App;
