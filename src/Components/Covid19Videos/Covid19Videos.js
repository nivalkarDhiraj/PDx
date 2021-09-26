import React, { useEffect, useState } from "react";
import Video from "../Video/Video";
import { WaveLoading } from "react-loadingg";
import "./Covid19Videos.css";
import { YOUTUBE_API_URL_SEARCH, CHANNEL_ID, API_KEY } from "../../Utils/config";

function Covid19Videos({ searchText }) {
	const [fetchedData, setFetchedData] = useState("");

	useEffect(() => {
		const fetchdata = async () => {
			try {
				const res = await fetch(
					`${YOUTUBE_API_URL_SEARCH}?` +
						new URLSearchParams({
							key: API_KEY,
							part: "snippet",
							q: "COVID-19",
							channelId: CHANNEL_ID,
							order: "date",
							maxResults: 25,
						})
				);
				const data = await res.json();

				let videoList = data.items;
				const playlistName = "COVID-19 Vaccine Podcast";

				for (let i = 0, j = 0; i < videoList.length; i++) {
					if (videoList[i].snippet.title.includes(playlistName)) {
						let temp = videoList[i];
						videoList[i] = videoList[j];
						videoList[j] = temp;
						j++;
					}
				}

				videoList = videoList.filter((video) =>
					video.snippet.title.toLowerCase().includes(searchText.toLowerCase())
				);

				setFetchedData(videoList);
			} catch (error) {
				console.log(error);
			}
		};
		fetchdata();
	}, [searchText]);

	return (
		<div className="covid19videos">
			{fetchedData ? (
				fetchedData.map((item, i) => {
					return (
						<Video
							key={i}
							image_url={item.snippet.thumbnails.high.url}
							title={item.snippet.title}
							description={item.snippet.description}
							date={item.snippet.publishedAt}
							videoId={item.id.videoId}
						></Video>
					);
				})
			) : (
				<WaveLoading color="#01193e" speed="0.8" size="large" />
			)}
		</div>
	);
}

export default Covid19Videos;
