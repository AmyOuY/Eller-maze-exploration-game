using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallbtRowBehavior : MonoBehaviour {

    public int life = 0;
    //destroy inner wall when being hit by bullet 3 times
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "shootedBullet")
        {
            life++;
            if (life == 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
