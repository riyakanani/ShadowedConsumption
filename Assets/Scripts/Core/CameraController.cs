using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX, currentPosY;
    private Vector3 velocity = Vector3.zero;

    // void Update()
    // {
    //     transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
    // }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(currentPosX, currentPosY, transform.position.z),
            ref velocity,
            speed
        );
    }

    public void MoveToNewRoomX(Transform _newRoom){
        currentPosX = _newRoom.position.x; 
        Debug.Log("HEre");
    }
    public void MoveToNewRoomY(Transform _newRoom){
        currentPosY = _newRoom.position.y; 
    }

}
