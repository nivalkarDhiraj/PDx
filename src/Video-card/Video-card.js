import React from 'react';
import './Video-card.css';
import { pipSharedService } from '../Shared-Service/Shared-service';

class Videocard extends React.Component {

    PlayPIPVideo() {
        console.log('Play PIP Video')
        pipSharedService.setData(true, this.props.video);
    }

    handleClick() {
        console.log('Handle Click')
        this.setState({active: true});
    }

    render() {

        return (
            <div className="video-card" onClick={this.PlayPIPVideo.bind(this)}>
                <div className="thumbnail">
                {/* <div class="thumbnail"> */}
                    {/* <a href={`https://www.youtube.com/watch?v=${this.props.video.videoId}`}> */}
                        <img 
                            height={this.props.video.thumbnail.height + 10}
                            width={this.props.video.thumbnail.width}
                            src={this.props.video.thumbnail.url}
                            alt=""
                        />
                    {/* </a> */}
                </div>
                <div className="video-info">
                    <div className="heading"> {this.props.video.title} </div>
                    <div className="description"> {this.props.video.description} </div>
                    <div className="publishdate"> Publish Date { this.props.video.publishedAt } </div>
                </div>
            </div>
        );
    }
}

export default Videocard;