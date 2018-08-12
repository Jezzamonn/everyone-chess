import Player from "./player";
import { random, Piece } from "./contants";

/**
 * Main class for handling the game logic
 */
export default class DummyController {

	constructor(game) {
        this.game = game;
        this.lastIndex = 0;

        this.ownedPlayers = [];

        this.waitCount = 0;
    }

    doAction() {
        // if (this.waitCount < 20) {
        //     this.waitCount ++;
        //     return;
        // }

        // if (random.bool(0.01)) {
        //     // Clear all players
        //     this.game.world.players = [];
        // }
        if (this.game.world.players.length == 0 || random.bool(0.1)) {
            this.newPlaya();
        }
        else {
            this.movePlaya();
        }
    }
    
    newPlaya() {
        const player = this.game.addPlayer(this.lastIndex);
        if (player == null) {
            return;
        }
        this.ownedPlayers.push(player);
        this.lastIndex ++;
    }

    movePlaya() {
        this.ownedPlayers = this.ownedPlayers.filter(player => !player.dead);
        const player = random.pick(this.ownedPlayers);
        const movement = random.pick(player.moves);
        if (movement == null) {
            return;
        }
        this.game.move(player.id, player.x + movement.x, player.y + movement.y);
    }

}
