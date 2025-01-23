using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileScript : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float damage;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        Debug.Log("Direction: " + direction);
        Debug.Log("Velocity: " + rb.velocity);
        
        // float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0,0,rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 10){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Debug.Log("collided");
            collision.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
