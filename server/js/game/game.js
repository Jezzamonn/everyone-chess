import World from "./world";
import Player from "./player";
import { Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class Game {

	constructor() {
        this.world = new World();
        this.world.players = [
			new Player(0, 1, 1, Piece.QUEEN),
			new Player(1, 3, 1, Piece.KING),
			new Player(2, 4, 1, Piece.PAWN),
			new Player(3, 5, 1, Piece.KNIGHT),
		];
	}

	move(id, x, y) {
		for (let player of this.world.players) {
			if (player.id == id) {
				player.x = x;
				player.y = y;
				// Kill any other players on the same square?

				const touchingPlayers = this.world.getPlayersAt(player.x, player.y);
				for (let otherPlayer of touchingPlayers) {
					if (otherPlayer === player) {
						continue;
					}
					otherPlayer.dead = true;
				}
			}
		}
	}

	// For debugging really
	render(context) {
		this.world.render(context);
	}

	// Fairly obviously for debugging :)
	toString() {
		let out = '';
		for (let y = 0; y < this.world.height; y ++) {
			for (let x = 0; x < this.world.width; x ++) {
				out += this.world.getDebugCharAt(x, y);
			}
			out += '\n';
		}
		return out;
	}

}
