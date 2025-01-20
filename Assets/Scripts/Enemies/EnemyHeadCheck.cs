//https://www.youtube.com/watch?v=LFGZUEudjY4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRb;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<PlayerCheck>()){
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
            playerRb.AddForce(Vector2.up * 300f);
        }
    }
}
