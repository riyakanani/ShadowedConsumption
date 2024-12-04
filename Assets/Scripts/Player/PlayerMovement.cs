using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    //for dialogue
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject X;
    private NPC_Controller npc;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider; 
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake (){
        //grabs references for rb and animator GameObject
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!inDialogue()){
            
            horizontalInput = Input.GetAxis("Horizontal");
            //flips player when moving left/right
            if(horizontalInput > 0.01f){
                // transform.localScale = Vector3.one;
                //fiza changed below line
                //transform.localScale = new Vector3(1,.1888354f, 1);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            } else if(horizontalInput < -0.01f){
                // transform.localScale = new Vector3(-1, 1, 1);
                //fiza changed below line
                //transform.localScale = new Vector3(-1, .1888354f, 1);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            //Set animator param
            anim.SetBool("run", horizontalInput != 0);
            anim.SetBool("grounded", isGrounded());

            //Wall Jump
            if(wallJumpCooldown > 0.2f){
                
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
                if(onWall() && !isGrounded()){
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                } else {
                    body.gravityScale = 5;
                }

                if(Input.GetKey(KeyCode.Space)){
                    Jump();
                }
            } else {
                wallJumpCooldown += Time.deltaTime;
            }

        }
    }

    private void Jump(){
        if(isGrounded()){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        } else if (onWall() && !isGrounded()){
            if(horizontalInput == 0){
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 20);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else{
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 20);
            }
            wallJumpCooldown = 0;
            
        }
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private bool inDialogue(){
        if(npc != null){
            return npc.DialogueActive();
        } else {
            return false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.gameObject.tag == "NPC"){
            Circle.gameObject.SetActive(true);
            X.gameObject.SetActive(true);

            npc = collision.gameObject.GetComponent<NPC_Controller>();
            if(Input.GetKey(KeyCode.X)){
                npc.ActiveDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        npc = null;
    }
}
 