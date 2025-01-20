using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPath : MonoBehaviour
{
    public AIPath aiPath;
   
    void Update(){
        if(aiPath.desiredVelocity.x >= .01f){
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } else if(aiPath.desiredVelocity.x <= -.01f){
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
