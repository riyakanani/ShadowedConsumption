using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonShooting : MonoBehaviour
{
    public GameObject[] bulletPrefabs;

    public Transform bulletSpawnPoint;
    public float shootingInterval = 2f;
    public float bulletForce = 5f;

    private float timer;
    [SerializeField] private float damage;
    [SerializeField] private Vector2 shootingDirection = Vector2.right;


    void Update()
    {
        timer += Time.deltaTime;

        if (timer > shootingInterval)
        {
            timer = 0;
            Shoot();
        }
    }
    void Shoot()
    {
        int randomIndex = Random.Range(0, bulletPrefabs.Length - 1);
        GameObject selectedBulletPrefab = bulletPrefabs[randomIndex];

        GameObject bullet = Instantiate(selectedBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootingDirection.normalized * bulletForce;
        }
    }


}
