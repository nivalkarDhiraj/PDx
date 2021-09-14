import React, { useEffect, useState } from "react";
import { WaveLoading } from "react-loadingg";
import { useLocation } from "react-router";
import Video from "./Video";

function SearchVideos() {
	const YOUTUBE_API_URL_SEARCH = "https://www.googleapis.com/youtube/v3/search";
	const API_KEY = process.env.REACT_APP_YOUTUBE_API_KEY;

	const [fetchedData, setFetchedData] = useState("");
	const location = useLocation();

	useEffect(() => {
        const { searchText } = location;

		const fetchdata = async () => {
			try {
				const res = await fetch(
					`${YOUTUBE_API_URL_SEARCH}?part=snippet&q=${searchText}&channelId=UCL03ygcTgIbe36o2Z7sReuQ&maxResults=25&key=${API_KEY}`
				);
				const data = await res.json();
				setFetchedData(data.items);
                console.log(fetchedData);
			} catch (error) {
				console.log(error);
			}
		};
		fetchdata();
	}, [location]);

	return (
		<div className="covid19videos">
			{fetchedData
				? fetchedData.map((item, i) => {
						return (
							<Video
								key={i}
								image_url={item.snippet.thumbnails.high.url}
								title={item.snippet.title}
								description={item.snippet.description}
								date={item.snippet.publishedAt}
								videoId = {item.id.videoId}
							></Video>
						);
				  })
				: <WaveLoading color="#01193e" speed="0.8" size="large"/>}
		</div>
	);
}

export default SearchVideos;
