import { TILE_SIZE, Tile, random } from "./contants";

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
