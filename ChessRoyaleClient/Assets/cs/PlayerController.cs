using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Add logic for spectating.
public class PlayerController : MonoBehaviour {

    public SelectSquare SelectSquare;
    public Piece Player;

    List<SelectSquare> selectSquares = new List<SelectSquare>();
    private SocketIoClient socketBoy;

	// Use this for initialization
	void Start () {
        socketBoy = FindObjectOfType<SocketIoClient>();
        AddSquares();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player) {
            transform.position = Player.transform.position;
        }
	}

    void AddSquares() {
        List<Vector2> moves = new List<Vector2>{
            new Vector2(0, 1),
            new Vector2(0, -1),
            new Vector2(-1, 0),
            new Vector2(1, 0),
        };

        foreach (Vector2 move in moves) {
            SelectSquare square = Instantiate(
                SelectSquare,
                transform.position + move.x * Vector3.right + move.y * Vector3.forward,
                Quaternion.identity);
            // You're my child now baby
            square.transform.parent = transform;
            square.Controller = this;
            square.CorrespondingMove = move;
        }
    }

    void ClearSquares() {
        foreach (SelectSquare square in selectSquares) {
            Destroy(square.gameObject);
        }
        selectSquares.Clear();
    }

    public void DoMove(Vector2 move) {
        // convert to global thingo
        Vector2 nextPosition = Player.GamePosition + move;
        socketBoy.DoMove(Player.Id, nextPosition);
    }
}