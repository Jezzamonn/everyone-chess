import Game from "./game/game";

export default class Controller {

	constructor() {
		this.game = new Game();
	}

	update() {
		this.game.update();
	}

	render(context) {
		this.game.render(context);
	}

}
