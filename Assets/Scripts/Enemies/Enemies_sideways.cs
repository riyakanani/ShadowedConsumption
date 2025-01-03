using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float detectionRange = 15f; // Distance to detect the player
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private Animator animator;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        animator = GetComponent<Animator>(); // Reference the Animator component
    }

    private void Update()
    {
        HandleMovement();
        DetectPlayerAndTriggerAnimation();
    }

    private void HandleMovement()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void DetectPlayerAndTriggerAnimation()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player"); // Find the player
    if (player != null)
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        // Debug.Log(distance);
        if (distance <= detectionRange) //15f is the dection range;
        {
            if (!animator.GetBool("PlayerClose")) // Only update if there's a change
            {
                animator.SetBool("PlayerClose", true);
            }
        }
        else
        {
            if (animator.GetBool("PlayerClose")) // Only update if there's a change
            {
                animator.SetBool("PlayerClose", false);
            }
        }
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}