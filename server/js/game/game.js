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
			new Player(1, 3, 1, Piece.KING),
			new Player(2, 4, 1, Piece.PAWN),
			new Player(3, 5, 1, Piece.KNIGHT),
		];
        this.world = new World();
	}

	move(id, x, y) {
		for (let player of this.players) {
			if (player.id == id) {
				player.x = x;
				player.y = y;
				// Kill any other players on the same square?

				for (let otherPlayer of this.players) {
					if (otherPlayer === player) {
						continue;
					}

					if (otherPlayer.x == player.x && otherPlayer.y == player.y) {
						// KILL!
						otherPlayer.dead = true;
					}
				}
			}
		}

		// remove dead players
		this.players = this.players.filter(p => !p.dead);
	}

	// For debugging really
	render(context) {
		this.world.render(context);
		for (let player of this.players) {
			player.render(context);
		}
	}

}
