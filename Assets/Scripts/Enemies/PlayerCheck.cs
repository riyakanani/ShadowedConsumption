//https://www.youtube.com/watch?v=LFGZUEudjY4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<EnemyHeadCheck>()){
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
