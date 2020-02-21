using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerPlayer : MonoBehaviour {

    public Transform player;
    public Vector3 offset;

    // Update is called once per frame
    void Update () {
        //camara always follows player in a relative position
        transform.position = player.position + offset;
    }
}
