import React from 'react';
import YouTubeItems from './YouTubeItems';

//List of videos with select video handler binding for items
const YouTubeList = ({videos, handleVideoSelect }) => {

    const renderedVideos =  videos.map((video) => {
        return <YouTubeItems  key={video.id.videoId} video={video} handleVideoSelect={handleVideoSelect}   />
    });

    return <div className='ui relaxed divided list'>{renderedVideos}</div>;
};
export default YouTubeList;