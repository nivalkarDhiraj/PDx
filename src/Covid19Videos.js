import React, { useEffect, useState } from "react";
import Video from "./Video";
import { WaveLoading } from 'react-loadingg';
import "./Covid19Videos.css";

function Covid19Videos() {
	const YOUTUBE_API_URL_PLAYLIST = "https://www.googleapis.com/youtube/v3/playlistItems";
	const YOUTUBE_API_URL_SEARCH = "https://www.googleapis.com/youtube/v3/search";
	const API_KEY = process.env.REACT_APP_YOUTUBE_API_KEY;

	const [fetchedData, setFetchedData] = useState("");

	useEffect(() => {
		const fetchdata = async () => {
			try {
				const res1 = await fetch(
					`${YOUTUBE_API_URL_PLAYLIST}?part=snippet&playlistId=PLogA9DP2_vSekxHP73PXaKD6nbOK56CJw&maxResults=50&key=${API_KEY}`
				);
				const data1 = await res1.json();

				const res2 = await fetch(
					`${YOUTUBE_API_URL_SEARCH}?part=snippet&q=COVID&channelId=UCL03ygcTgIbe36o2Z7sReuQ&maxResults=25&key=${API_KEY}`
				);
				const data2 = await res2.json();
				console.log(data1);
				console.log(data2);
				const data = !data2.error ? data2.items: data1.items;
				console.log(data);
				setFetchedData(data);
			} catch (error) {
				console.log(error);
			}
		};
		fetchdata();
	},[]);

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
								videoId = {item.id.videoId? item.id.videoId : item.snippet.resourceId.videoId}
							></Video>
						);
				  })
				: <WaveLoading color="#01193e" speed="0.8" size="large"/>}
		</div>
	);
}

export default Covid19Videos;
