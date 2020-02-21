using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public Rigidbody rb;
    GameManager gameManager;
    public ShootedBulletBehavior shootedBulletPrefab;
    public float speed = 20f;
    public float lifeTime = 1.5f;
    public static float firePositionZ;
  
    

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
       
    }


    // Update is called once per frame
    void Update () {
        //press F key to fire bullet, only can do this when player on ground
        if (Input.GetKeyDown("f") && gameManager.currBullets > 0 && rb.position.y <= 0.5)
        {
            Fire();
            gameManager.currBullets--;
        }

	}

    //get player position and forward angle in order to fire bullets
    private void Fire()
    {
        ShootedBulletBehavior bullet = Instantiate(shootedBulletPrefab) as ShootedBulletBehavior;
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), this.GetComponent<Collider>());
        bullet.transform.position = this.transform.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        bullet.GetComponent<Rigidbody>().AddForce(-transform.forward * speed, ForceMode.Impulse);
        StartCoroutine(DestroyBullet(bullet, lifeTime));
        firePositionZ = this.transform.position.z;
    }

    //destroy bullets after certain time
    private IEnumerator DestroyBullet(ShootedBulletBehavior bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
       
    }
}
