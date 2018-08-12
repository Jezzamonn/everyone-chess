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

    public Transform Pawn;
    public Transform Rook;
    public Transform Bishop;
    public Transform Knight;
    public Transform Queen;
    public Transform King;

    // TODO: replace w/ Piece type
    private Dictionary<char, Transform> possiblePieces;

    private List<Transform> players;
    private List<List<int?>> tiles;

    private SocketIoClient socketBoy;
    private GameData lastGameData;
    private Camera camera;

    // Use this for initialization
    void Start()
    {
        socketBoy = FindObjectOfType<SocketIoClient>();
        camera = Camera.main;

        possiblePieces = new Dictionary<char, Transform>
        {
            {'P', Pawn},
            {'R', Rook},
            {'B', Bishop},
            {'N', Knight},
            {'Q', Queen},
            {'K', King},
        };
        players = new List<Transform>();
    }

    public void UpdateGameData(GameData gameData)
    {
        // For the first time, generate tiles?
        if (tiles == null && gameData.tiles != null)
        {
            tiles = gameData.tiles;
            for (int y = 0; y < gameData.tiles.Count; y++)
            {
                for (int x = 0; x < gameData.tiles[y].Count; x++)
                {
                    AddTileAt(x, y, gameData.tiles[y][x] ?? 0);
                }
            }
        }

        // First lets remove the old ones?? (For now)
        foreach (Transform player in players)
        {
            Destroy(player.gameObject);
        }
        players.Clear();

        // Update the players
        foreach (PlayerData playerData in gameData.players)
        {
            Transform piece = possiblePieces[playerData.type.letter];
            Transform player = Instantiate(piece, playerData.x * Vector3.right + playerData.y * Vector3.forward + 0.1f * Vector3.up, Quaternion.identity);
            players.Add(player);
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

        // Update camera to center of players
        if (players.Count > 0)
        {
            float minX = players.Min(p => p.position.x);
            float maxX = players.Max(p => p.position.x);
            float minZ = players.Min(p => p.position.z);
            float maxZ = players.Max(p => p.position.z);
            Vector3 newPosition = new Vector3(
                (minX + maxX) / 2,
                camera.transform.position.y,
                (minZ + maxZ) / 2);
            camera.transform.position = Vector3.Lerp(camera.transform.position, newPosition, 0.1f);
        }

    }
}
