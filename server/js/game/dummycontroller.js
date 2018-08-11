import Player from "./player";
import { random, Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class DummyController {

	constructor(game) {
        this.game = game;
        this.lastIndex = 0;
    }
    
    newPlaya() {
        const newPlayer = new Player(
            this.lastIndex,
            random.integer(0, this.game.world.width),
            random.integer(0, this.game.world.height),
            random.pick([Piece.PAWN, Piece.KNIGHT, Piece.ROOK, Piece.BISHOP, Piece.QUEEN, Piece.KING])
        )
        this.lastIndex ++;
        this.game.world.players.push(newPlayer);
    }

}
