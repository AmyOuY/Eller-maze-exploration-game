using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWall : MonoBehaviour {

	public void Initialize()
    {
    
        transform.localPosition = new Vector3(7.5f, 1.5f, -3.15f * (Maze.mazeZ + 1) + 0.0001f * Maze.mazeZ);

    }
}
