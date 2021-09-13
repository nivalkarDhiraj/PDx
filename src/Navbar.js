import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import "./Navbar.css";

function Navbar() {
	const [searchText, setSearchText] = useState("");
	const history = useHistory();

	return (
		<div className="navbar">
			<div className="navbar__left">
				<Link to="/">HCA Video Library</Link>
			</div>
			<div className="navbar__middle1">
				<Link to="/covid19videos">Covide 19 Videos</Link>
				<Link to="/allvideos">All Videos</Link>
			</div>
			<div className="navbar__middle2">
				<input
					type="search"
					placeholder="Search..."
					value={searchText}
					onChange={(e) => setSearchText(e.target.value)}
					onKeyPress={(e) => {
						if(e.key === "Enter") {
							history.push({
								pathname :"/searchvideos",
								searchText : searchText
							});
						};
					}}
				></input>
			</div>
			<div className="navbar__right">
				<Link>Welcome Test User!</Link>
			</div>
		</div>
	);
}

export default Navbar;
