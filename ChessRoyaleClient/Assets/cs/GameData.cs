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
    public int? id;
}
