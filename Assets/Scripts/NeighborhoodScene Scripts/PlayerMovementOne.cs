using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOne : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform visualTransform; // drag the "Visual" child here

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private bool facingRight = false; // default: facing left

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Start facing left (scale X = -1)
        visualTransform.localScale = new Vector3(-1, 1, 1);
        facingRight = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // ✅ Flip when changing directions
        if (horizontalInput > 0.01f && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < -0.01f && facingRight)
        {
            Flip();
        }

        anim.SetBool("run", horizontalInput != 0);
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = visualTransform.localScale;
        scale.x *= -1;
        visualTransform.localScale = scale;
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }
}
