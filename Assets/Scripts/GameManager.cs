using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Maze mazePrefab;
    private Maze mazeInstance;
    public GameObject player;
    public int currBullets = 0;
    public Text displayInfo;
    public Canvas canvas;

    private void Start()
    {
        //control last step WIN message display
        canvas = FindObjectOfType<Canvas>();
        canvas.enabled = false;
    }

}
