using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveDistance = 2f; // How far the platform moves back and forth
    [SerializeField] private float speed = 2f; // Speed of the platform movement
    private Vector3 pointA; // Starting position
    private Vector3 pointB; // Target position
    private Vector3 targetPosition; // Current target position

    private void Start()
    {
        // Set Point A to the platform's current position
        pointA = transform.position;

        // Set Point B to a position offset by moveDistance on the X-axis
        pointB = pointA + new Vector3(moveDistance, 0f, 0f);

        // Initialize the target position
        targetPosition = pointB;
    }

    private void Update()
    {
        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Switch target position when the platform reaches it
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = targetPosition == pointA ? pointB : pointA;
        }
    }
}
