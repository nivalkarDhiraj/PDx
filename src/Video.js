import React from "react";
import { Link } from "react-router-dom";
import "./Video.css";

function Video({ image_url, title, description, date, videoId }) {
	description = description.length < 200 ? description : description.slice(0, 200) + "...";
	let publishDate = new Date(date);
	var options = { year: 'numeric', month: 'long', day: 'numeric' };
	publishDate = publishDate.toLocaleDateString("en-US", options);

	return (
		<Link to={`/youtubevideo/${videoId}`}>
			<div className="video">
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
		</Link>
	);
}

export default Video;
