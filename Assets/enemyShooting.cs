using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    public Enemy enemy;

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

        // Get the current scale of the enemy
        Vector3 currentScale = enemy.transform.localScale;

        // Adjust the scale while maintaining the original sign
        Vector3 newScale = new Vector3(
            Mathf.Sign(currentScale.x) * (Mathf.Abs(currentScale.x) + 0.1f),  // Increase X
            Mathf.Sign(currentScale.y) * (Mathf.Abs(currentScale.y) - 0.1f),  // Decrease Y
            Mathf.Sign(currentScale.z) * (Mathf.Abs(currentScale.z) - 0.1f)   // Decrease Z
        );

        // Apply the new scale to the enemy
        enemy.transform.localScale = newScale;

        // Check if any scale component is less than 0.5
        if (newScale.x < 0.5f || newScale.y < 0.5f || newScale.z < 0.5f) {
            Destroy(enemy.gameObject); // Destroy the enemy if any scale is below 0.5
        }
    }
}
