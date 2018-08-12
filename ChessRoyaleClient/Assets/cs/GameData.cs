using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<List<int?>> tiles;
    public List<List<int?>> powerups;
    public List<PlayerData> players;
}

[Serializable]
public class PlayerData
{
    public int x;
    public int y;
    public int id;
    public PieceTypeData type;
}

[Serializable]
public class PieceTypeData
{
    public int id;
    public char letter;
}
