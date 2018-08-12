using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Congrats this is also in charge up updating game state kind of?
/// </summary>
public class LevelGenerator : MonoBehaviour {

    public Transform OddTile;
    public Transform EvenTile;

    public Transform Pawn;
    public Transform Rook;
    public Transform Bishop;
    public Transform Knight;
    public Transform Queen;
    public Transform King;

    private List<Transform> pieces;

	// Use this for initialization
	void Start () {
        pieces = new List<Transform> { Pawn, Rook, Bishop, Knight, Queen, King };

        for (int y = 0; y < 10; y ++) {
            for (int x = 0; x < 10; x ++) {
                bool odd = ((x + y) & 1) == 1;
                Transform tile = odd ? OddTile : EvenTile;
                Instantiate(tile, x * Vector3.right + y * Vector3.forward, Quaternion.identity);

                if (Random.value < 0.1f) {
                    Transform piece = pieces[Random.Range(0, pieces.Count)];
                    Instantiate(piece, x * Vector3.right + y * Vector3.forward + 0.1f * Vector3.up, Quaternion.identity);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
