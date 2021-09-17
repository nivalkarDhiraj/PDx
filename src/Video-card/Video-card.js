import React from 'react';
import './Video-card.css';
import { pipSharedService } from '../Shared-Service/Shared-service';

class Videocard extends React.Component {

    PlayPIPVideo() {
        pipSharedService.setData(true, this.props.video);
    }

    render() {

        return (
            <div className="video-card" onClick={this.PlayPIPVideo.bind(this)}>
                <div className="thumbnail">
                        <img 
                            height={this.props.video.thumbnail.height + 10}
                            width={this.props.video.thumbnail.width}
                            src={this.props.video.thumbnail.url}
                            alt=""
                        />
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