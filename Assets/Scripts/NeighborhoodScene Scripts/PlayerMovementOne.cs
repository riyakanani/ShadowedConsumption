using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOne : MonoBehaviour
{
    [SerializeField] private float speed; // Player speed
    [SerializeField] private LayerMask groundLayer; // Layer for ground detection

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
        // Grab references for Rigidbody2D and Animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input (left and right arrow keys)
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player sprite depending on movement direction
        //if (horizontalInput > 0.01f)
        //{
        //    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //}
        //else if (horizontalInput < -0.01f)
        //{
        //    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //}

        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // Set animator parameters based on movement
        anim.SetBool("run", horizontalInput != 0); // Make sure your walk animation state is tied to this bool

        // Apply movement
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    // Check if the player is grounded (could be useful for jumping or other logic)
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
