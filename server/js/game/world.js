import { TILE_SIZE, Tile } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class World {

	constructor() {
		this.width = 10;
		this.height = 10;

		this.tiles = [];
		this.powerups = [];
		// ?? Players
		for (let y = 0; y < this.height; y ++) {
			this.tiles[y] = [];
			this.powerups[y] = [];
			for (let x = 0; x < this.width; x ++) {
				this.tiles[y][x] = null;
				this.powerups[y][x] = null;
			}
		}

		for (let y = 0; y < this.height; y ++) {
			for (let x = 0; x < this.width; x ++) {
				this.tiles[y][x] = Tile.GROUND;
			}
		}
	}

	update() {
		// TODO: Some updating logic?
	}

	/**
	 * @param {CanvasRenderingContext2D} context 
	 */
	render(context) {
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

}
