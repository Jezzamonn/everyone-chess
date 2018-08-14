// TODO: all server related things

'use strict';

import express from 'express';
import http from 'http';
import SocketIO from 'socket.io';
import path from 'path';

import Game from '../game/game';
import DummyController from '../game/dummycontroller';

let app = express();
let port = process.env.PORT || 3000;
app.set('port', port);
let server = http.Server(app);
let io = new SocketIO(server);

// Not sure if necessary but we're going to return a html just in case
app.get('/', function(req, res){

    // send the debug.html file for all requests
    const htmlPath = path.resolve(__dirname + '/../debug.html');
    res.sendFile(htmlPath);
  
});

server.listen(port, function(){
    console.log('listening on *:' + port);
});

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
        // console.log(game.toString());
        io.sockets.emit('world-update', game.world.toObject());
    }, 
    500
);