using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player triggered door");
            // Handle X-axis movement
            if (collision.transform.position.x < transform.position.x)
            {
                Debug.Log("move next room");
                cam.MoveToNewRoomX(nextRoom);
            }
            else
            {
                Debug.Log("move prev room");
                cam.MoveToNewRoomX(previousRoom);
            }
        }
    }
}