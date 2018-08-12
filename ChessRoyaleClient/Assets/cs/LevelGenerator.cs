using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Transform tile;

	// Use this for initialization
	void Start () {

        for (int y = 0; y < 10; y ++) {
            for (int x = 0; x < 10; x ++) {
                Instantiate(tile, x * Vector3.right + y * Vector3.forward, Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
