import React from 'react';
import Navigation from "../../components/navigation/Navigation";
import {Link} from "react-router-dom";

class Home extends React.Component {

	render() {
		return (
			<div className="wrapper">
				<div className="home">
					<Navigation/>
					<div className="container">
						{/* Home screen */}
						<div className="home_block">
							<h1 className="home_block_title">Посмотреть аниме в прямом эфире</h1>

							{/* Buttons */}
							<div className="home_block_buttons">
								{/* Link */}
								<Link to="/welcome">
									<div className="home_block_buttons_button_1">
										<img src="./assets/icon/play.png" alt=""/>
										<p>Доступно в 4K</p>
									</div>
								</Link>

								{/* Link */}
								<Link to="/welcome">
									<div className="home_block_buttons_button_2">
										<p>Подробнее</p>
									</div>
								</Link>
							</div>
						</div>
					</div>
				</div>

				{/* Sliders */}
				<div className="home_container">
					{/* Slider */}
					<div className="home_slider">

					</div>
				</div>
			</div>
		);
	}
}

export default Home;
