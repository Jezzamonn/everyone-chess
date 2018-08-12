import Player from "./player";
import { random, Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class DummyController {

	constructor(game) {
        this.game = game;
        this.lastIndex = 0;

        this.waitCount = 0;
    }

    doAction() {
        // if (this.waitCount < 20) {
        //     this.waitCount ++;
        //     return;
        // }

        if (random.bool(0.01)) {
            // Clear all players
            this.game.world.players = [];
        }
        else if (this.game.world.players.length == 0 || random.bool(0.1)) {
            this.newPlaya();
        }
        else {
            this.movePlaya();
        }
    }
    
    newPlaya() {
        const newPlayer = new Player(
            this.lastIndex,
            random.integer(0, this.game.world.width-1),
            random.integer(0, this.game.world.height-1),
            random.pick([Piece.PAWN, Piece.KNIGHT, Piece.ROOK, Piece.BISHOP, Piece.QUEEN, Piece.KING])
        )
        this.lastIndex ++;

        // Kill all fools foolish enough to occupy this space
        this.game.world.getPlayersAt(newPlayer.x, newPlayer.y).forEach(p => p.dead = true);
        this.game.world.removeDeadPlayers();

        this.game.world.players.push(newPlayer);
    }

    movePlaya() {
        const player = random.pick(this.game.world.players);
        const movement = random.pick([
            {x: 1, y: 0},
            {x: -1, y: 0},
            {x: 0, y: 1},
            {x: 0, y: -1},
        ])
        this.game.move(player.id, player.x + movement.x, player.y + movement.y);
    }

}
