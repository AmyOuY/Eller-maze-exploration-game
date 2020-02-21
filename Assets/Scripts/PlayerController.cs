using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    public Maze mazePrefab;
    private Maze mazeInstance;
    public float speed = 100f;
    public float position;
    public int row;
    public Bullet bulletPrefab;
    private Bullet bulletInstance;
    GameManager gameManager;
    

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //pressing down W, A, S, D keys control player movement forward, leftward, backward and rightward respectively
        //these keys function when player is on the ground
        if (Input.GetKeyDown("w"))
        {
            rb.AddForce(new Vector3(0, 0, -1) * speed);
        }

        if (Input.GetKeyDown("s"))
        {
            rb.AddForce(new Vector3(0, 0, 1) * speed);
        }

        if (Input.GetKeyDown("d"))
        {
            rb.AddForce(new Vector3(-1, 0, 0) * speed);
        }

        if (Input.GetKeyDown("a"))
        {
            rb.AddForce(new Vector3(1, 0, 0) * speed);
        }

        //pressing space key makes player jump into the air but not move norizontally
        if (Input.GetKeyDown("space"))
        {

            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            rb.AddForce(new Vector3(0, 1, 0) * 6 * speed);
            transform.Rotate(new Vector3(0, 90, 0), Space.Self);
            
        }
        
        //release hold on horizontal move when player is on ground
        if (rb.position.y <= 0.6f && (Input.GetKeyDown("w") | Input.GetKeyDown("s") | Input.GetKeyDown("d") | Input.GetKeyDown("a")))
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }



        //when player passes the gap in maze, generate new maze row with one bullet distributed in each cell
        row = Maze.mazeZ;
        if (rb.position.z <= -4f + (-3f) * (row - 1) && rb.position.y >= 0)
        {
            if (row == 1) //generate first row of maze
            {
                Maze.buildNewRow = false;
                Maze.firstRow = true;
            }
            else //generate other rows of maze
            {
                Maze.firstRow = false;
                Maze.buildNewRow = true;
            }
           //instantiate row
            mazeInstance = Instantiate(mazePrefab) as Maze;
            mazeInstance.Generate();
            row = int.MaxValue;
            Maze.firstRow = false;
            Maze.buildNewRow = false;
            //deploy bullet in row
            Bullet.deployBullets = true;
            bulletInstance = Instantiate(bulletPrefab) as Bullet;
            bulletInstance.BulletDeploy(0f);
            Bullet.deployBullets = false;
        }
        //stop player motion when end wall terminated the maze
        if (ShootedBulletBehavior.endWallBuilt)
        {
            this.enabled = false;
        }

    }

    //when player touches the gold on the other side of the terrain, a "win game" info will display and end game
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gold")
        {
            gameManager.canvas.enabled = true;
            this.enabled = false;
        }
    }


}