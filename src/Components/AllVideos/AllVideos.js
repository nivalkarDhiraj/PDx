import React, { useEffect, useState } from "react";
import Video from "../Video/Video";
import "./AllVideos.css";
import { WaveLoading } from "react-loadingg";
import {YOUTUBE_API_URL_PLAYLIST, API_KEY} from "../../Utils/config";

function AllVideos() {
	const [fetchedData, setFetchedData] = useState("");

	useEffect(() => {
		const fetchdata = async () => {
			try {
				const res = await fetch(
					`${YOUTUBE_API_URL_PLAYLIST}?` +
						new URLSearchParams({
							part: "snippet",
							playlistId: "UUL03ygcTgIbe36o2Z7sReuQ",
							maxResults: 50,
							order: "date",
							key: API_KEY,
						})
				);
				const data = await res.json();

				setFetchedData(data.items);
			} catch (error) {
				console.log(error);
			}
		};
		fetchdata();
	}, []);

	return (
		<div className="allvideos">
			{fetchedData ? (
				fetchedData.map((item, i) => {
					return (
						<Video
							key={i}
							image_url={item.snippet.thumbnails.high.url}
							title={item.snippet.title}
							description={item.snippet.description}
							date={item.snippet.publishedAt}
							videoId={item.snippet.resourceId.videoId}
						></Video>
					);
				})
			) : (
				<WaveLoading color="#01193e" speed="0.8" size="large" />
			)}
		</div>
	);
}

export default AllVideos;
