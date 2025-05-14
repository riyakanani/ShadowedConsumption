//using UnityEngine;

//public class FloatingToken : MonoBehaviour
//{
//    public float floatSpeed = 1f;
//    public float floatAmplitude = 0.5f;


//    private Vector3 startPos;

//    void Start()
//    {
//        startPos = transform.position;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude, 0f);
//        transform.Rotate(Vector3.up, 50f * Time.deltaTime);
//    }
//}

using UnityEngine;

public class FloatingToken : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatAmplitude = 0.5f;
    public float moveAwaySpeed = 2f;  // Speed at which the token moves away
    public float minDistance = 3f;    // Minimum distance at which the token starts moving away
    public Transform player;          // Reference to the player

    private Vector3 startPos;
    private Vector3 targetPosition;

    void Start()
    {
        startPos = transform.position;
        targetPosition = startPos;
    }

    void Update()
    {
        // Float up and down
        transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude, 0f);
        transform.Rotate(Vector3.up, 50f * Time.deltaTime);

        // Check the distance to the player
        if (Vector3.Distance(transform.position, player.position) < minDistance)
        {
            // Move the token away from the player
            Vector3 directionAwayFromPlayer = (transform.position - player.position).normalized;
            targetPosition = transform.position + directionAwayFromPlayer * moveAwaySpeed * Time.deltaTime;
        }

        // Smoothly move towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveAwaySpeed * Time.deltaTime);
    }
}


