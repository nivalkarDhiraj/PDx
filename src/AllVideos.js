import React, { useEffect, useState } from "react";
import Video from "./Video";
import "./AllVideos.css";
import { WaveLoading } from "react-loadingg";

function AllVideos() {
	const YOUTUBE_API_URL_PLAYLIST = "https://www.googleapis.com/youtube/v3/playlistItems";
	const API_KEY = process.env.REACT_APP_YOUTUBE_API_KEY;

	const [fetchedData, setFetchedData] = useState("");

	useEffect(() => {
		const fetchdata = async () => {
			try {
				const res = await fetch(
					`${YOUTUBE_API_URL_PLAYLIST}?part=snippet&playlistId=UUL03ygcTgIbe36o2Z7sReuQ&maxResults=50&key=${API_KEY}`
				);
				const data = await res.json();

				console.log(fetchedData);
				setFetchedData(data.items);
			} catch (error) {
				console.log(error);
			}
		};
		fetchdata();
	}, []);

	return (
		<div className="allvideos">
			{fetchedData
				? fetchedData.map((item, i) => {
						return (
							<Video
								key={i}
								image_url={item.snippet.thumbnails.high.url}
								title={item.snippet.title}
								description={item.snippet.description}
								date={item.snippet.publishedAt}
								videoId = {item.snippet.resourceId.videoId}
							></Video>
						);
				  })
				: <WaveLoading color="#01193e" speed="0.8" size="large"/>}
		</div>
	);
}

export default AllVideos;
