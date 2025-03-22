using UnityEngine;

public class ChipsMovement : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 2f;  // Speed of up-and-down movement
    [SerializeField] private float verticalAmplitude = 1f; // Amplitude of the up-and-down movement

    private Vector3 startPosition;
    private float lerpTime = 0f;  // Timer for the Lerp function

    void Start()
    {
        // Save the initial position of the object
        startPosition = transform.position;

        // Make sure the starting position is set correctly (no initial drift)
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
    }

    void Update()
    {
        // Increment the lerp timer based on speed
        lerpTime += Time.deltaTime * verticalSpeed;

        // Use Mathf.PingPong to create oscillation between two points
        float newY = Mathf.Lerp(startPosition.y - verticalAmplitude, startPosition.y + verticalAmplitude, Mathf.PingPong(lerpTime, 1));

        // Apply the new Y position while keeping the X and Z positions unchanged
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}


