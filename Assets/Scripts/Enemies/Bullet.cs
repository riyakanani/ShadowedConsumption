using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Bullet hit player!");
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
