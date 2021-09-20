import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import AllVideos from "../AllVideos/AllVideos";
import "./App.css";
import Covid19Videos from "../Covid19Videos/Covid19Videos";
import SearchVideos from "../SearchVideos/SearchVideos";
import Navbar from "../Navbar/Navbar";

function App() {
	return (
		<div className="App">
			<BrowserRouter>
				<Navbar />
				<Switch>
					<Route
						exact
						path="/"
						render={() => {
							return <Redirect to="/covid19videos" />;
						}}
					/>
					<Route path="/covid19videos" >
						<Covid19Videos /> 
					</Route>
					<Route path="/allvideos">
						<AllVideos />
					</Route>
					<Route path="/searchvideos">
						<SearchVideos />
					</Route>
				</Switch>
			</BrowserRouter>
		</div>
	);
}

export default App;
