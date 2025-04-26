using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
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

    [Header("Wall Slide")]
    [SerializeField] private float wallSlideSpeed = 2f;

    [SerializeField] private float platformSlideSpeed = 2f;
    [SerializeField] private float defaultGravity = 5f;
    [SerializeField] private float sideCastDistance = 0.5f;
    private bool isPlatformSliding = false;


    [Header("Slide Settings")]
    [SerializeField] private float slideSpeed = 2f;           // max downward speed while sliding
    [SerializeField] private float slideGravityScale = 5f;    // gravity while sliding
    [SerializeField] private float normalGravityScale = 5f;   // gravity when not sliding

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!inDialogue())
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            // ✅ Use key press directly for instant run animation
            bool isRunning = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
            anim.SetBool("run", isRunning);

            anim.SetBool("grounded", isGrounded());

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

    private void FixedUpdate()
{
    if (inDialogue()) return;

    // --- 1) Slide handling ---
    if (isPlatformSliding)
    {
        body.gravityScale = slideGravityScale;

        if (body.velocity.y < -slideSpeed)
            body.velocity = new Vector2(body.velocity.x, -slideSpeed);

        Debug.Log($"Platform‐sliding: velY={body.velocity.y:F2}");
    }
    else
    {
        body.gravityScale = normalGravityScale;
    }

    // --- 2) Your existing horizontal + wall‐jump cooldown logic ---
    if (wallJumpCooldown > 0.2f)
    {
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }
    else
    {
        wallJumpCooldown += Time.deltaTime;
    }
}


    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            // Jump off the wall, only applying horizontal movement if user is pressing left/right
            float jumpDirection = horizontalInput != 0 ? horizontalInput : 0f;
            body.velocity = new Vector2(jumpDirection * 3f, 20f);

            wallJumpCooldown = 0;

            // Flip sprite based on jump direction
            if (horizontalInput != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(horizontalInput), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private bool onPlatformSide()
    {
        // cast horizontally in the direction the player is facing
        Vector2 dir = new Vector2(transform.localScale.x, 0f);
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            dir,
            sideCastDistance
        );
        return hit.collider != null && hit.collider.CompareTag("Platform");
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


    private void OnCollisionStay2D(Collision2D col)
    {
        // reset each frame
        isPlatformSliding = false;

        // only care about things tagged “Platform”
        if (!col.gameObject.CompareTag("Platform")) 
            return;

        // check each contact point
        foreach (var contact in col.contacts)
        {
            // side contacts have a mostly‐horizontal normal
            if (Mathf.Abs(contact.normal.x) > 0.5f)
            {
                // normal.x points *out* of the platform; 
                // we want the player to be pushing *into* it:
                float pushDir = Mathf.Sign(horizontalInput);
                float hitSide = -Mathf.Sign(contact.normal.x);

                if (pushDir == hitSide && !isGrounded())
                {
                    isPlatformSliding = true;
                    return; // once we know, we can stop checking
                }
            }
        }
    }
}
