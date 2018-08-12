import { TILE_SIZE, Tile, random, Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class World {

	constructor() {
		this.width = 10;
		this.height = 10;

		this.tiles = [];
		this.powerups = [];
		this.players = [];

		for (let y = 0; y < this.height; y ++) {
			this.tiles[y] = [];
			this.powerups[y] = [];
			for (let x = 0; x < this.width; x ++) {
				this.tiles[y][x] = Tile.EMPTY;
				this.powerups[y][x] = null;
			}
		}

		for (let y = 0; y < this.height; y ++) {
			for (let x = 0; x < this.width; x ++) {
				if (random.bool(0.9)) {
					this.tiles[y][x] = Tile.GROUND;
				}
			}
		}
	}

	removeDeadPlayers() {
		this.players = this.players.filter(player => !player.dead);
	}

	updateAllPlayerMoves() {
		this.players.forEach(player => this.updatePlayerMoves(player));
	}

	/**
	 * @param {Player} player 
	 */
	updatePlayerMoves(player) {
		player.moves = [];
		switch (player.type) {
			case Piece.PAWN: {
				this.addMoveIfValid(player, 0, 1);
			}
			break;
			case Piece.KING: {
				this.addMoveIfValid(player,  0,  1);
				this.addMoveIfValid(player,  0, -1);
				this.addMoveIfValid(player,  1,  0);
				this.addMoveIfValid(player, -1,  0);
			}
			break;
			case Piece.BISHOP: {
				this.addMovesInDirection(player,  1,  1, 8);
				this.addMovesInDirection(player,  1, -1, 8);
				this.addMovesInDirection(player, -1,  1, 8);
				this.addMovesInDirection(player, -1, -1, 8);
			}
			break;
			case Piece.ROOK: {
				this.addMovesInDirection(player,  0,  1, 8);
				this.addMovesInDirection(player,  0, -1, 8);
				this.addMovesInDirection(player,  1,  0, 8);
				this.addMovesInDirection(player, -1,  0, 8);
			}
			break;
			case Piece.QUEEN: {
				this.addMovesInDirection(player,  0,  1, 8);
				this.addMovesInDirection(player,  0, -1, 8);
				this.addMovesInDirection(player,  1,  0, 8);
				this.addMovesInDirection(player, -1,  0, 8);

				this.addMovesInDirection(player,  1,  1, 8);
				this.addMovesInDirection(player,  1, -1, 8);
				this.addMovesInDirection(player, -1,  1, 8);
				this.addMovesInDirection(player, -1, -1, 8);
			}
			break;
			case Piece.KNIGHT: {
				this.addMoveIfValid(player,  1,  2);
				this.addMoveIfValid(player,  1, -2);
				this.addMoveIfValid(player, -1,  2);
				this.addMoveIfValid(player, -1, -2);

				this.addMoveIfValid(player,  2,  1);
				this.addMoveIfValid(player,  2, -1);
				this.addMoveIfValid(player, -2,  1);
				this.addMoveIfValid(player, -2, -1);
			}
			break;
		}
	}

	addMovesInDirection(player, xDir, yDir, maxDist) {
		for (let i = 1; i <= maxDist; i ++) {
			const keepMoving = this.addMoveIfValid(player, i * xDir, i * yDir);
			if (!keepMoving) {
				break;
			}
		}
}

	/**
	 * @param {Player} player
	 * @returns Whether more moves can be done in this direction
	 */
	addMoveIfValid(player, x, y) {
		const newX = player.x + x;
		const newY = player.y + y;
		if (this.getTileAt(newX, newY) == Tile.EMPTY) {
			return false;
		}
		// So, this means the move is valid so we can add it.
		player.moves.push({'x': x, 'y': y});

		// Now we need to check if we would run into a player, in that case we can't keep moving
		const players = this.getPlayersAt(newX, newY);
		if (players.length > 0) {
			return false;
		}
		return true;
	}

	getPlayersAt(x, y) {
		return this.players.filter(player => player.x == x && player.y == y)
	}

	getPlayerById(id) {
		const players = this.players.filter(player => player.id == id);
		if (players.length == 0) {
			return null;
		}
		return players[0];
	}

	getDebugCharAt(x, y) {
		const players = this.getPlayersAt(x, y);
		if (players.length > 0) {
			// should only be 1 but just in case
			return players[0].type.letter;
		}
		if (this.getTileAt(x, y) == Tile.GROUND) {
			return '.';
		}
		return ' ';
	}

	getTileAt(x, y) {
		if (y < 0 || y >= this.height || x < 0 || x >= this.width) {
			return Tile.EMPTY;
		}
		return this.tiles[y][x];
	}

	setTileAt(x, y, value) {
		this.tiles[y][x] = value;
	}

	update() {
		// TODO: Some updating logic?
	}

	/**
	 * @param {CanvasRenderingContext2D} context 
	 */
	render(context) {
		this.renderTiles(context);
		this.renderPlayers(context);
	}

	/**
	 * @param {CanvasRenderingContext2D} context 
	 */
	renderTiles(context) {
		const pad = 0.05;
		const padWidth = 1 - 2 * pad;
		for (let y = 0; y < this.height; y ++) {
			for (let x = 0; x < this.width; x ++) {
				const odd = (x + y) & 1;
				if (this.tiles[y][x] == Tile.GROUND) {
					context.fillStyle = odd ? '#DDD' : '#AAA';
					context.fillRect(
						(x + pad) * TILE_SIZE,
						(y + pad) * TILE_SIZE,
						padWidth * TILE_SIZE,
						padWidth * TILE_SIZE);
				}
			}
		}
	}

	/**
	 * @param {CanvasRenderingContext2D} context 
	 */
	renderPlayers(context) {
		for (let player of this.players) {
			player.render(context);
		}
	}

	toObject() {
		return {
			tiles: this.tiles,
			powerups: this.powerups,
			players: this.players,
		}
	}

	// Fairly obviously for debugging :)
	toString() {
		let out = '';
		for (let y = 0; y < this.height; y ++) {
			for (let x = 0; x < this.width; x ++) {
				out += this.getDebugCharAt(x, y);
			}
			out += '\n';
		}
		return out;
	}

}
