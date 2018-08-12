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

        // Kill all fools foolish enough to occupy this space
        this.game.world.getPlayersAt(newPlayer.x, newPlayer.y).forEach(p => p.dead = true);
        this.game.world.removeDeadPlayers();

        this.game.world.players.push(newPlayer);
    }

}
