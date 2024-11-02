import React from 'react';
import Navigation from "../../components/navigation/Navigation";

class Home extends React.Component {

	render() {
		return (
			<div className="wrapper">
				<div className="home">
					<Navigation/>
					<div className="container">
						<h1>Home</h1>
					</div>
				</div>
			</div>
		);
	}
}

export default Home;
