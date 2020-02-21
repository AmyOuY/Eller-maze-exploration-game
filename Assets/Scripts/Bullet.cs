using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    GameManager gameManager;
    public Rigidbody rb;
    public static bool deployBullets = false;


    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    public Bullet bulletPrefab;
    //deploy one bullet to each cell
    public void BulletDeploy(float offset)
    {
        if (deployBullets)
        {
            deployBullets = false;
            for (int i = 0; i < 8; i++)
            {
                Bullet bullet = Instantiate(bulletPrefab) as Bullet;
                bullet.Initialize(0f + 4f * i, 7f - 3f * (Maze.mazeZ));
            }
        }
    }

 
    public void Initialize(float XOffset, float ZOffset)
    {
        transform.localPosition = new Vector3(-6.5f + XOffset, 0.125f, -6.5f + ZOffset);
        deployBullets = false;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            gameManager.currBullets++;
        }
    }

}
