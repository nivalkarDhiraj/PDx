import React, { useState } from "react";
import YouTube from "react-youtube";
import "./Video.css";

function Video({ image_url, title, description, date, videoId }) {
	const [clicked, setClicked] = useState(false);

	const wordLimit = 200;
	description = description.length < wordLimit ? description : description.slice(0, wordLimit) + "...";
	let publishDate = new Date(date);
	var options = { year: "numeric", month: "long", day: "numeric" };
	publishDate = publishDate.toLocaleDateString("en-US", options);
	const opts = {
		width: "100%",
		playerVars: {
			origin: window.location.origin,
			autoplay: 1,
			host: `${window.location.protocol}//www.youtube.com`,
		},
	};

	return (
		<div className="link">
			{!clicked ? (
				<div
					className="video"
					onClick={(e) => {
						setClicked(true);
					}}
				>
					<div className="video__left">
						<img className="video__image" src={image_url} alt="video" />
					</div>
					<div className="video__right">
						<h2 className="video__title"> {title}</h2>
						<p className="video__discription">{description}</p>
						<p className="video__date">
							Publish Date: <span>{publishDate}</span>
						</p>
					</div>
				</div>
			) : (
				<div className="youtube">
					<button
						className="youtube__close"
						onClick={() => {
							setClicked(false);
						}}
					>
						x
					</button>
					<YouTube className="youtube__video" videoId={videoId} opts={opts} />
				</div>
			)}
		</div>
	);
}

export default Video;
