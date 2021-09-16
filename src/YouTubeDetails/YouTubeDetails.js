import React from "react";
import { Card } from 'react-bootstrap'
import Moment from 'react-moment';


const YouTubeDetails = ({ video }) => {

    //check if video is null or empty
    if (!video) {
        return <div>
        </div>;
    }

    //setting attributed for the selected video
    const videoSrc = `https://www.youtube.com/embed/${video.id.videoId}`;
    return (
        <Card>
            <iframe height='500px' scrolling='no' src={videoSrc} allowFullScreen title={video.snippet.title} />
            <Card.Body>
                <Card.Title>{video.snippet.title}</Card.Title>
                <Card.Text>
                    {video.snippet.description}
                    <p><strong> Published By</strong> <Moment format="MMM-DD-YYYY">{video.snippet.publishTime}</Moment></p>
                </Card.Text>

            </Card.Body>
        </Card>
    );
};

export default YouTubeDetails;
