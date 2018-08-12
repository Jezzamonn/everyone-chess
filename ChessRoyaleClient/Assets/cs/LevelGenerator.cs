using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Congrats this is also in charge up updating game state kind of?
/// </summary>
public class LevelGenerator : MonoBehaviour
{

    public Transform OddTile;
    public Transform EvenTile;

    public Piece Pawn;
    public Piece Rook;
    public Piece Bishop;
    public Piece Knight;
    public Piece Queen;
    public Piece King;

    public Transform Splode;
    public Transform Boosh;

    // TODO: replace w/ Piece type
    private Dictionary<char, Piece> possiblePieces;

    private List<Piece> players;
    private List<List<int?>> tiles;

    private SocketIoClient socketBoy;
    private GameData lastGameData;
    private Camera theMainCamera;
    private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        socketBoy = FindObjectOfType<SocketIoClient>();
        playerController = FindObjectOfType<PlayerController>();
        theMainCamera = Camera.main;

        possiblePieces = new Dictionary<char, Piece>
        {
            {'P', Pawn},
            {'R', Rook},
            {'B', Bishop},
            {'N', Knight},
            {'Q', Queen},
            {'K', King},
        };
        players = new List<Piece>();
    }

    public void UpdateGameData(GameData gameData)
    {
        // For the first time, generate tiles?
        if (tiles == null && gameData.tiles != null)
        {
            GenerateTiles(gameData.tiles);
            tiles = gameData.tiles;
        }

        UpdatePlayers(gameData.players);
    }

    void UpdatePlayers(List<PlayerData> playerData) {
        // First mark all players as dead. Any one that's updated we'll undo this, leaving the ones left out as dead.
        foreach (Piece player in players) {
            player.Dead = true;
        }

        foreach (PlayerData playerDatum in playerData) {
            Piece matchingPlayer = players.SingleOrDefault(p => p.Id == playerDatum.id);
            if (matchingPlayer == null) {
                // Add new player.
                Piece piece = possiblePieces[playerDatum.type.letter];
                Piece player = Instantiate(
                    piece,
                    playerDatum.x * Vector3.right
                    + playerDatum.y * Vector3.forward
                    + 0.2f * Vector3.up,
                    Quaternion.identity);
                player.GamePosition = new Vector2(playerDatum.x, playerDatum.y);
                player.Id = playerDatum.id;
                player.Type = (PieceType)playerDatum.type.letter;
                player.Moves = playerDatum.moves;
                players.Add(player);
            }
            else {
                if (matchingPlayer == playerController.Player)
                {
                    Debug.Log(playerDatum.moves);
                }
                // Move the player
                matchingPlayer.Moves = playerDatum.moves;
                // TODO: Update the type here maybe (like if a boy becomes a queen)
                matchingPlayer.GamePosition = new Vector2(playerDatum.x, playerDatum.y);
                matchingPlayer.Dead = false;
            }
        }

        // Remove all the dead players?
        foreach (Piece player in players) {
            if (player.Dead) {
                // Do a splosion!!!
                Transform sploSplo = Instantiate(
                    Splode,
                    player.transform.position + 0.05f * Vector3.down,
                    Quaternion.identity);
                // RIP
                Destroy(sploSplo.gameObject, 0.5f);
                // Do a splosion!!!
                Transform booshBoosh = Instantiate(
                    Boosh,
                    player.transform.position + 0.05f * Vector3.down,
                    Quaternion.identity);
                // RIP
                Destroy(booshBoosh.gameObject, 2f);

                Destroy(player.gameObject);
            }
        }
        players = players.Where(p => !p.Dead).ToList();

        // Update player controller

        Piece controlledPlayer = players.SingleOrDefault(p => p.Id == playerController.Id);
        if (controlledPlayer != null) {
            // Should be ok to constantly set this?
            playerController.Player = controlledPlayer;
            playerController.UpdateSquares();
        }

        // Update currently controlled player. Also works if destroyed, surprisingly
        if (playerController.Player == null) {
            // Request a new player?
            string id = System.Guid.NewGuid().ToString();
            playerController.Id = id;
            playerController.Player = null;
            socketBoy.AddPlayer(id);
        }
    }

    void ClearPlayers() {
        // First lets remove the old ones?? (For now)
        foreach (Piece player in players)
        {
            Destroy(player.gameObject);
        }
        players.Clear();
    }

    void GenerateTiles(List<List<int?>> tiles) {
        for (int y = 0; y < tiles.Count; y++)
        {
            for (int x = 0; x < tiles[y].Count; x++)
            {
                AddTileAt(x, y, tiles[y][x] ?? 0);
            }
        }
    }

    void AddTileAt(int x, int y, int type)
    {
        if (type == 1)
        {
            bool odd = ((x + y) & 1) == 1;
            Transform tile = odd ? OddTile : EvenTile;
            Instantiate(tile, x * Vector3.right + y * Vector3.forward, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        if (socketBoy.GameData != lastGameData) {
            UpdateGameData(socketBoy.GameData);
            lastGameData = socketBoy.GameData;
        }

        UpdateCamera();
    }

    void UpdateCamera() {
        // Update camera to center of players
        if (players.Count > 0)
        {
            float minX = players.Min(p => p.transform.position.x);
            float maxX = players.Max(p => p.transform.position.x);
            float minZ = players.Min(p => p.transform.position.z);
            float maxZ = players.Max(p => p.transform.position.z);
            Vector3 newPosition = new Vector3(
                (minX + maxX) / 2,
                theMainCamera.transform.position.y,
                (minZ + maxZ) / 2);
            theMainCamera.transform.position = Vector3.Lerp(theMainCamera.transform.position, newPosition, 0.1f);
        }
    }
}
