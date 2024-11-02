import React from "react";
import {Link} from "react-router-dom";

class Navigation extends React.Component {
	render() {
		return (
			<div className="container">
				{/* Navigation */}
				<nav className="navigation">
					{/* Navigation right */}
					<div className="navigation_block">
						<li className="navigation_link">
							<Link to="/">Главная</Link>
						</li>
						<li className="navigation_link">
							<Link to="/library">Библиотека</Link>
						</li>
					</div>
					{/* Navigation left */}
					<div className="navigation_block">
						<li className="navigation_link">
							<Link to="/">Поиск</Link>
						</li>
						<li className="navigation_link">
							<Link to="/profile">Профиль</Link>
						</li>
					</div>
				</nav>
			</div>
		);
	}
}

export default Navigation;
