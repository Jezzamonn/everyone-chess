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
let controller = new DummyController(game);

io.on('connection', (socket) => {
    // console.log('a user connected');
  
    socket.on('do-move', (id, x, y) => {
        // Totally unverified, in many ways :) :) :)
        game.move(id, x, y);

        // world update? Could be bad haha
        io.sockets.emit('world-update', game.world.toObject());
    });

    socket.on('add-player', (id) => {
        game.addPlayer(id);

        // again, maybe consider buffering these
        io.sockets.emit('world-update', game.world.toObject());
    });
});


setInterval(
    () => {
        controller.doAction();
        console.log(game.toString());
        io.sockets.emit('world-update', game.world.toObject());
    }, 
    500
);

server.listen(3000, function(){
  console.log('listening on *:3000');
});