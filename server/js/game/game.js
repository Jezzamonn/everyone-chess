import World from "./world";
import Player from "./player";
import { Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class Game {

	constructor() {
        this.players = [
			new Player(0, 1, 1, Piece.QUEEN),
		];
        this.world = new World();
	}

	update() {
		for (let player of this.players) {
			// do something??
		}
	}

	// For debugging really
	render(context) {
		this.world.render(context);
		for (let player of this.players) {
			player.render(context);
		}
	}

}
