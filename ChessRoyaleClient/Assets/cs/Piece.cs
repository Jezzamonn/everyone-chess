using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public PieceType Type;
    public string Name;
    public int Id;

    // We gonna slurp to this one
    public Vector2 GamePosition;

    // TODO: Possible moves and such

	void FixedUpdate () {
        // Grid size is 1, conveniently
        Vector3 newPosition = new Vector3(GamePosition.x, transform.position.y, GamePosition.y);
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.2f);
	}
}
