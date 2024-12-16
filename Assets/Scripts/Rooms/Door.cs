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
            // Handle X-axis movement
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoomX(nextRoom);
            }
            else
            {
                cam.MoveToNewRoomX(previousRoom);
            }

            // Additional logic for Room3 to handle Y-axis movement
            if (nextRoom.name == "Room3")
            {
                if (collision.transform.position.y < transform.position.y)
                {
                    cam.MoveToNewRoomY(nextRoom);
                }
                else
                {
                    cam.MoveToNewRoomY(previousRoom);
                }
            }
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Door : MonoBehaviour
// {
//     [SerializeField] private Transform previousRoom;
//     [SerializeField] private Transform nextRoom;
//     [SerializeField] private CameraController cam;
    
//     private void OnTriggerEnter2D(Collider2D collision){
//         if(collision.tag == "Player"){
//             if(collision.transform.position.x < transform.position.x){
//                 cam.MoveToNewRoom(nextRoom);
//             } else {
//                 cam.MoveToNewRoom(previousRoom);
//             } 

//             if(nextRoom.name == "Room3"){
//                 if(collision.transform.position.y < transform.position.y){
//                     cam.MoveToNewRoomX(nextRoom);
//                 } else {
//                     cam.MoveToNewRoomY(previousRoom);
//                 } 
//             }
//             // if(collision.transform.position.y < transform.position.y){
//             //     cam.MoveToNewRoom(nextRoom);
//             // } else {
//             //     cam.MoveToNewRoom(previousRoom);
//             // } 

//         }
//     }
// }
