using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;

    void Update(){
        timer += Time.deltaTime;

        if(timer > 2){
            timer = 0;
            shoot();
        }
    }

    void shoot(){
        // Instantiate(bullet, bulletPos.position, Quaternion.identity);
        GameObject newBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        // Set the current object as the parent of the instantiated bullet
        newBullet.transform.SetParent(transform);
    }
}
