using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string id;
    public PieceTypeData type;
    public List<Vector2> moves;
}

[Serializable]
public class PieceTypeData
{
    public int id;
    public char letter;
}
