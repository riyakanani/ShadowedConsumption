using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Bullet hit player!");
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Platform")){
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = shootingDirection.normalized * bulletForce;
        }
    }

}
