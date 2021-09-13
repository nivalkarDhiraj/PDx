import React from "react";
import YouTube from "react-youtube";
import { useParams } from "react-router";
import "./YouTubeVideo.css";

function YouTubeVideo(props) {
	let { videoId } = useParams();

	const opts = {
		height: "520",
		width: "854",
		playerVars: {
			autoplay: 1,
		},
	};

	return (
		<div className="youtubevideo">
			<YouTube videoId={videoId} opts={opts} />
		</div>
	);
}

export default YouTubeVideo;
