var path = require('path');
var webpack = require('webpack');
var nodeExternals = require('webpack-node-externals');

var serverConfig = {
    entry: './js/server/server.js',
    output: {
        path: path.resolve(__dirname, 'build'),
        filename: 'server.bundle.js'
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: 'babel-loader',
                query: {
                    presets: ['env']
                }
            }
        ]
    },
    target: 'node',
    // Needed to stop webpack from mangling some node things
    node: {
        __dirname: false,
        __filename: false,
    },
    externals: [nodeExternals({
        modulesFromFile: true
    })],
    stats: {
        colors: true
    },
    devtool: 'source-map'
}

var debugClientConfig = {
    entry: './js/main.js',
    output: {
        path: path.resolve(__dirname, 'build'),
        filename: 'main.bundle.js'
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: 'babel-loader',
                query: {
                    presets: ['env']
                }
            }
        ]
    },
    stats: {
        colors: true
    },
    devtool: 'source-map'
};

module.exports = [serverConfig, debugClientConfig];