// TODO: all server related things

'use strict';

import express from 'express';
import http from 'http';
import SocketIO from 'socket.io';
import compression from 'compression';

import Game from '../game/game';
import DummyController from '../game/dummycontroller';

let app = express();
let server = http.Server(app);
let io = new SocketIO(server);

// TODO: Socket io somehow?

let game = new Game();
console.log(game.toString());

let controller = new DummyController(game);

setInterval(
    () => {
        controller.newPlaya();
        console.log(game.toString());
        io.sockets.emit('world-update', game.world.toObject());
    }, 
    1000
);

server.listen(3000, function(){
  console.log('listening on *:3000');
});