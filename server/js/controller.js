import Game from "./game/game";

export default class Controller {

	constructor() {
		this.game = new Game();
		window.game = this.game;
	}

	update() {
		// NOTHING
	}

	render(context) {
		this.game.render(context);
	}

}
