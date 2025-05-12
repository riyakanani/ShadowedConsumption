using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool startFacingLeft = false; // Set this in the Inspector per scene
    [SerializeField] private float jumpPower = 18f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject X;
    private NPC_Controller npc;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;

    private bool isWallSliding;
    private float wallSlidingSpeed;

    // [SerializeField] private Transform wallCheck;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (startFacingLeft)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

    }

    private void Update()
    {
        if (!inDialogue())
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            // ✅ Use key press directly for instant run animation
            bool isRunning = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
            anim.SetBool("run", isRunning);

            // anim.SetBool("grounded", isGrounded());

            // Flip sprite
            if (horizontalInput > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            if (wallJumpCooldown > 0.2f)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();
                }
            }
            else
            {
                wallJumpCooldown += Time.deltaTime;
            }
        }
    }

    // private void FixedUpdate()
    // {
    //     if (!inDialogue())
    //     {
    //         if (wallJumpCooldown > 0.2f)
    //         {
    //             // Apply movement
    //             body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

    //             // Handle wall slide
    //             if (onWall() && !isGrounded())
    //             {
    //                 body.gravityScale = 1; // Light gravity for sliding down the wall
    //                 body.velocity = new Vector2(body.velocity.x, Mathf.Max(body.velocity.y, -3f)); // Limit fall speed
    //             }
    //             else
    //             {
    //                 body.gravityScale = 5; // Normal gravity
    //             }
    //         }
    //     }
    // }

    private void FixedUpdate()
    {
        if (!inDialogue())
        {
            if (wallJumpCooldown > 0.2f)
            {
                bool pressingIntoWall = (horizontalInput > 0 && transform.localScale.x > 0) || (horizontalInput < 0 && transform.localScale.x < 0);
                bool wallCondition = onWall() && !isGrounded() && pressingIntoWall;

                if (wallCondition)
                {
                    // WALL SLIDING
                    body.velocity = new Vector2(0, Mathf.Max(body.velocity.y, -3f)); // No horizontal movement
                    body.gravityScale = 1; // Reduced gravity
                    isWallSliding = true;
                }
                else
                {
                    // NORMAL MOVEMENT
                    body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                    body.gravityScale = 5; // Normal gravity
                    isWallSliding = false;
                }
            }
        }
    }



    private void Jump()
    {
        if (isWallSliding)
        {
            // Prevent jumping while sliding
            return;
        }

        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            // Wall jump (only if not sliding anymore)
            float jumpDirection = horizontalInput != 0 ? horizontalInput : 0f;
            body.velocity = new Vector2(jumpDirection * 3f, 20f);

            wallJumpCooldown = 0;

            if (horizontalInput != 0)
            {
                // transform.localScale = new Vector3(Mathf.Sign(horizontalInput), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    // private bool onWall()
    // {
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    //     return raycastHit.collider != null;
    // }

    private bool onWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer | groundLayer);
        if (hit.collider != null)
        {
            // Check for "Platform" tag if it's not in wallLayer
            if (wallLayer == (wallLayer | (1 << hit.collider.gameObject.layer)))
            {
                return true;
            }
            else if (hit.collider.CompareTag("Platform"))
            {
                return true;
            }
        }
        return false;
    }


    private bool inDialogue()
    {
        return npc != null && npc.DialogueActive();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            Circle?.SetActive(true);
            X?.SetActive(true);

            npc = collision.GetComponent<NPC_Controller>();

            if (Input.GetKey(KeyCode.X))
            {
                npc.ActiveDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
    }
}