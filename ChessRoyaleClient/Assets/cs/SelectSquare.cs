using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSquare : MonoBehaviour {

    public PlayerController Controller;
    public Vector2 CorrespondingMove;

    private void OnMouseUpAsButton()
    {
        if (Controller) {
            Controller.DoMove(CorrespondingMove);
        }
    }
}
