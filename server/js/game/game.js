import World from "./world";
import Player from "./player";
import { Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class Game {

	constructor() {
        this.world = new World();
        this.world.players = [];
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
		return this.world.toString();
	}

}
