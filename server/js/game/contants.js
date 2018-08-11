import deepFreeze from 'deep-freeze';

export const TILE_SIZE = 20;

export const Tile = deepFreeze({
    EMPTY: 0,
    GROUND: 1,
});

export const Piece = deepFreeze({
    UNKNOWN: {
        id: 0,
        letter: '.',
    },
    PAWN: {
        id: 1,
        letter: 'P',
    },
    KNIGHT: {
        id: 2,
        letter: 'N',
    },
    BISHOP: {
        id: 3,
        letter: 'B',
    },
    ROOK: {
        id: 4,
        letter: 'R',
    },
    QUEEN: {
        id: 5,
        letter: 'Q',
    },
    KING: {
        id: 6,
        letter: 'K',
    },
    // AMAZON and other fairy chess pieces?
});