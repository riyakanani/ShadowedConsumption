using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage = 20f;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    // [SerializeField] private float range;

    // [Header("Player Layer")]
    // [SerializeField] private LayerMask playerLayer;
    
    // [Header("Collider Parameters")]
    // [SerializeField] private float colliderDistance;
    // [SerializeField] private BoxCollider2D boxCollider;

    // [Header("SFX")]
    // [SerializeField] private AudioClip handSound;

    private void Awake(){
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update(){
        if(movingLeft){
            if(transform.position.x > leftEdge){
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = false;
            }
        } else{
            if(transform.position.x < rightEdge){
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            } else {
                movingLeft = true;
            }

        }

        // if (PlayerInSight())
        // {
        //     SoundManager.instance.PlaySound(handSound);
        //     Debug.Log("here");
        // }
    }

    // private bool PlayerInSight()
    // {
    //     RaycastHit2D hit =
    //         Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
    //         new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
    //         0, Vector2.left, 0, playerLayer);

    //     return hit.collider != null;
    // }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            collision.GetComponent<Health>().TakeDamage(damage); 
        }
    }     
}
