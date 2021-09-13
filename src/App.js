import { BrowserRouter, Route, Switch } from "react-router-dom";
import AllVideos from "./AllVideos";
import "./App.css";
import Covid19Videos from "./Covid19Videos";
import SearchVideos from "./SearchVideos";
import Navbar from "./Navbar";
import YouTubeVideo from "./YouTubeVideo";
import { WaveLoading } from "react-loadingg";

function App() {
	return (
		<div className="App">
			<BrowserRouter>
				<Navbar />
				<Switch>
					<Route exact path="/">
						<WaveLoading color="#01193e" speed="0.8" size="large" />
					</Route>
					<Route path="/covid19videos">
						<Covid19Videos />
					</Route>
					<Route path="/allvideos">
						<AllVideos />
					</Route>
					<Route path="/searchvideos">
						<SearchVideos />
					</Route>
					<Route path="/youtubevideo/:videoId">
						<YouTubeVideo />
					</Route>
				</Switch>
			</BrowserRouter>
		</div>
	);
}

export default App;
