using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalMove : MonoBehaviour
{
    [SerializeField] private float speed = 2f;              // Movement speed
    [SerializeField] private float moveDistance = 3f;       // How far to move from starting point
    private Vector3 startPosition;
    private int direction = 1; // 1 = right, -1 = left

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        float distanceFromStart = transform.position.x - startPosition.x;

        // If we've moved too far, flip direction
        if (Mathf.Abs(distanceFromStart) >= moveDistance)
        {
            direction *= -1; // Reverse direction
            distanceFromStart = -distanceFromStart;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("here");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("here2");
            // Parent the player to the platform
            collision.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Un-parent the player from the platform
            collision.transform.SetParent(null);
        }
    }
}
