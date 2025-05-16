using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyShooting : MonoBehaviour
{
    public GameObject[] bulletPrefabs;
    public Transform bulletPos;

    public Enemy enemy;

    private float timer;

    private Health playerHealth;

    void Start(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    void Update(){
        timer += Time.deltaTime;

        if(timer > 2){
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        if (bulletPrefabs.Length == 0)
        {
            Debug.LogWarning("No bullet prefabs assigned!");
            return;
        }
        playerHealth.AddHealth(.1f);

        // Select a random bullet prefab
        int randomIndex = Random.Range(0, bulletPrefabs.Length);
        GameObject selectedBullet = bulletPrefabs[randomIndex];

        // Instantiate the bullet at the specified position
        GameObject newBullet = Instantiate(selectedBullet, bulletPos.position, Quaternion.identity);

        // Add a Rigidbody2D component if it doesn't already exist
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = newBullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Optional: Set gravity scale to 0 if you don't want gravity to affect the bullet
        }

        // Set the bullet's velocity
        rb.velocity = Vector2.left * 5f; // Adjust the direction and speed as needed

        // Shrink the enemy
        Vector3 currentScale = enemy.transform.localScale;
        Vector3 newScale = new Vector3(
            -Mathf.Sign(currentScale.x) * (Mathf.Abs(currentScale.x) - 0.1f),
            Mathf.Sign(currentScale.y) * (Mathf.Abs(currentScale.y) - 0.1f),
            Mathf.Sign(currentScale.z) * (Mathf.Abs(currentScale.z) - 0.1f)
        );
        enemy.transform.localScale = newScale;


        // Check if any scale component is less than 0.5
        if (Mathf.Abs(newScale.x) < 0.5f || Mathf.Abs(newScale.y) < 0.5f || Mathf.Abs(newScale.z) < 0.5f)
        {
            Destroy(transform.parent.gameObject); // Destroy the enemy if any scale is below 0.5
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
