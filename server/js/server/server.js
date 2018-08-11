// TODO: all server related things

'use strict';

import express from 'express';
import http from 'http';
import SocketIO from 'socket.io';
import compression from 'compression';

import Game from '../game/game';

let app = express();
let server = http.Server(app);
let io = new SocketIO(server);
let port = process.env.PORT || 3000;

app.use(compression({}));
// TODO: Socket io somehow?

let game = new Game();
console.log(game.toString());

server.listen(port, () => {
    console.log('[INFO] Listening on *:' + port);
});