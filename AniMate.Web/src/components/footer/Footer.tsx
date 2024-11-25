import React from "react";
import {Link} from "react-router-dom";

class Footer extends React.Component {
	render() {
		return (
			<div className="footer">
				<div className="container">
					{/* Footer container */}
					<div className="footer_container">



						{/* Left */}
						<div className="footer_left">
							{/* Logo */}
							<div className="footer_left_logo">
								<p className="footer_left_logo_title">Animate</p>
							</div>
							{/* Date */}
							<div className="footer_left_date">
								<p className="footer_left_date_title">animate.fun © 2023 — 2024</p>
							</div>
							{/* Button */}
							<button className="footer_left_button">
								<div>
									<p>Задать вопрос</p>
								</div>
							</button>
						</div>



						{/* Center */}
						<div className="footer_center">
							{/* Social Networks */}
							<div className="footer_center_line">
								<Link to="/">
									<img src="./assets/icon/discord.png" alt="discord"/>
								</Link>
								<Link to="/">
									<img src="./assets/icon/vk.png" alt="discord"/>
								</Link>
								<Link to="/">
									<img src="./assets/icon/telegram.png" alt="discord"/>
								</Link>
							</div>

							{/* Info */}
							<div className="footer_center_line">
								<Link to="/">
									<p>Контакты</p>
								</Link>
								<Link to="/">
									<p>Мобильная версия</p>
								</Link>
								<Link to="/">
									<p>FAQ</p>
								</Link>
								<Link to="/">
									<p>Очистить cookie</p>
								</Link>
								<Link to="/">
									<p>Пользовательское соглашение</p>
								</Link>
								<Link to="/">
									<p>Политика конфиденциальности</p>
								</Link>
								<Link to="/">
									<p>Публичная оферта</p>
								</Link>
								<Link to="/">
									<p>Правообладатели</p>
								</Link>
								<Link to="/">
									<p>DMCA</p>
								</Link>
							</div>
						</div>



						{/* Right */}
						<div className="footer_right">
							<Link to="/">
								<img src="./assets/icon/app_store.png"/>
							</Link>
						</div>
					</div>
				</div>
			</div>
		);
	}
}

export default Footer;
