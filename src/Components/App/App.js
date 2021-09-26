import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";
import AllVideos from "../AllVideos/AllVideos";
import "./App.css";
import Covid19Videos from "../Covid19Videos/Covid19Videos";
import Navbar from "../Navbar/Navbar";
import { useState, useEffect } from "react";

function App() {
	const [searchText, setSearchText] = useState("");

	useEffect(() => {
		console.log("app", searchText);
	})
	
	return (
		<div className="App">
			<BrowserRouter>
				<Navbar searchText={searchText} setSearchText={setSearchText} />
				<Switch>
					<Route
						exact
						path="/"
						render={() => {
							return <Redirect to="/covid19videos" />;
						}}
					/>
					<Route path="/covid19videos">
						<Covid19Videos searchText={searchText}/>
					</Route>
					<Route path="/allvideos">
						<AllVideos searchText={searchText}/>
					</Route>
				</Switch>
			</BrowserRouter>
		</div>
	);
}

export default App;
