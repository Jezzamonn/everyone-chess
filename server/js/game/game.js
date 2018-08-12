import World from "./world";
import Player from "./player";
import { Piece, Tile, random } from "./contants";

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
		this.world.removeDeadPlayers();
		this.world.updateAllPlayerMoves();
	}

	addPlayer(id) {
		let x, y;
		let goodSpot = false;
		// Try a hundred times, otherwise give up
		for (let i = 0; i < 100; i ++) {
			x = random.integer(0, this.world.width-1);
			y = random.integer(0, this.world.height-1);
			
			if (this.world.getPlayersAt(x, y).length == 0
			&& this.world.getTileAt(x, y) == Tile.GROUND) {
				goodSpot = true;
				break;
			}
		}
		if (goodSpot) {
			const newPlayer = new Player(
				id,
				x,
				y,
				random.pick([Piece.PAWN, Piece.KNIGHT, Piece.ROOK, Piece.BISHOP, Piece.QUEEN, Piece.KING])
			);
			this.world.players.push(newPlayer);

			this.world.updateAllPlayerMoves();

			return newPlayer;
		}

		return null;
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
