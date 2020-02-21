using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootedBulletBehavior : MonoBehaviour {

    public EndWall endWallPrefab;
    public Maze mazePrefab;
    private Maze mazeInstance;
    public Rigidbody rb;
    public static bool endWallBuilt = false;

    //destroy bullet
    private void OnTriggerEnter(Collider other)
    {       
        Destroy(gameObject);
        
    }

    //add end wall to terminate maze and form a simple maze when bullet reached out of the maze boundary
    public void FixedUpdate()
    {
        if (this.rb.position.z < PlayerWeapon.firePositionZ - 15)
        {
            Maze.lastRow = true;
            mazeInstance = Instantiate(mazePrefab) as Maze; //form last row of cells
            mazeInstance.Generate();
            EndWall wall = Instantiate(endWallPrefab) as EndWall; //form end wall
            wall.Initialize();
            endWallBuilt = true;
        }
    }

}
