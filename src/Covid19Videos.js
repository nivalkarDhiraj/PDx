import React, { useEffect, useState } from "react";
import Video from "./Video";
import { WaveLoading } from "react-loadingg";
import "./Covid19Videos.css";

function Covid19Videos(props) {
	const YOUTUBE_API_URL_PLAYLIST = "https://www.googleapis.com/youtube/v3/playlistItems";
	const YOUTUBE_API_URL_SEARCH = "https://www.googleapis.com/youtube/v3/search";
	const API_KEY = process.env.REACT_APP_YOUTUBE_API_KEY;

	const swapElements = (array, i, j) => {
		let temp = array[i];
	};

	const [fetchedData, setFetchedData] = useState("");
	
	useEffect(() => {
		const fetchdata = async () => {
			try {
				// const res = await fetch(
				// 	`${YOUTUBE_API_URL_SEARCH}?part=snippet&q=COVID-19&channelId=UCL03ygcTgIbe36o2Z7sReuQ&order=date&maxResults=25&key=${API_KEY}`
				// );
				const res = await fetch(
					`${YOUTUBE_API_URL_SEARCH}?` +
						new URLSearchParams({
							key: process.env.REACT_APP_YOUTUBE_API_KEY,
							part: "snippet",
							q: "COVID-19",
							channelId: "UCL03ygcTgIbe36o2Z7sReuQ",
							order: "date",
							maxResults: 25,
						})
				);
				const data = await res.json();

				const videoList = data.items;

				for (let i = 0, j = 0; i < videoList.length; i++) {
					if (videoList[i].snippet.title.includes("COVID-19 Vaccine Podcast")) {
						let temp = videoList[i];
						videoList[i] = videoList[j];
						videoList[j] = temp;
						j++;
					}
				}

				setFetchedData(videoList);
			} catch (error) {
				console.log(error);
			}
		};
		fetchdata();
	}, []);

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
