import { Piece, TILE_SIZE } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class Player {

    constructor(id, x = 0, y = 0, type = Piece.UNKNOWN) {
        // TODO: Public & Private ID
        this.id = id;
        this.x = x;
        this.y = y;
        this.type = type;
        this.dead = false;
        // Potential moves that could be made.
        this.moves = [];
	}

	/**
	 * @param {CanvasRenderingContext2D} context
	 */
	render(context) {
        context.fillStyle = 'red';
        context.font = '17px Arial';
        context.fillText(this.type.letter, TILE_SIZE * this.x + 4, TILE_SIZE * this.y + 17);
    }

}
