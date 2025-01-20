using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;

    [SerializeField] private float speed;
    private float currentPosX, currentPosY;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

        // transform.position = Vector3.SmoothDamp(
        //     transform.position,
        //     new Vector3(currentPosX, currentPosY, transform.position.z),
        //     ref velocity,
        //     speed
        // );
    }



    public void MoveToNewRoomX(Transform _newRoom){
        currentPosX = _newRoom.position.x; 
        Debug.Log("HEre");
    }
    
    // public void MoveToNewRoomY(Transform _newRoom){
    //     currentPosX = _newRoom.position.y; 
    //     Debug.Log("HEre");
    // }
}
