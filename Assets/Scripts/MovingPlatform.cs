using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int pathIndex; // Set in Inspector for each shelf
    private Vector3 targetPosition; // Where the shelf should move
    private Vector3 targetScale; // The final scale of the shelf
    private float moveSpeed = 6f; // Speed of movement
    private float scaleSpeed = 0.5f; // Speed of resizing
    private bool startMoving = false;
    private bool oscillating = false; // Enables floating motion
    private Vector3 startPosition; // Stores final stopping position

    private void Start()
    {
        // **Manually Defined Target Positions & Scales**
        Vector3[] pathPositions = new Vector3[]
        {
            new Vector3(-26.2227f, -6.9393f, 0),
            new Vector3(-3.9604f, 2.6653f, 0),
            new Vector3(-20.86f, 0.05f, 0),
            new Vector3(26.5f, -5.7f, 0),
            new Vector3(34f, -1.5f, 0),
            new Vector3(5.8f, 0.3f, 0),
            new Vector3(17.2f, -1.3f, 0),
            new Vector3(-27.54f, 5.77f, 0),
            new Vector3(42.1053f, 4.1574f, 0),
            new Vector3(-14.21f, 6.08f, 0)
        };

        Vector3[] pathScales = new Vector3[]
        {
            new Vector3(0.2245258f, 0.4361229f, 1),
            new Vector3(0.2348575f, 0.4323424f, 1),
            new Vector3(0.221791f, 0.4367399f, 1),
            new Vector3(0.2862278f, 0.5061463f, 1),
            new Vector3(0.2559863f, 0.4078633f, 1),
            new Vector3(0.2862278f, 0.5212442f, 1),
            new Vector3(0.2635544f, 0.4758772f, 1),
            new Vector3(0.2419354f, 0.4099988f, 1),
            new Vector3(0.2873277f, 0.5347999f, 1),
            new Vector3(0.2419354f, 0.4056035f, 1)
        };

        // Assign the target position & scale based on pathIndex
        if (pathIndex >= 0 && pathIndex < pathPositions.Length)
        {
            targetPosition = pathPositions[pathIndex];
            targetScale = pathScales[pathIndex];
        }
        else
        {
            targetPosition = transform.position; // Default if index is out of range
            targetScale = transform.localScale;
        }
    }

    private void Update()
    {
        if (startMoving)
        {
            if (!oscillating)
            {
                // Move shelf toward its designated position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Scale the shelf toward its target size
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

                // If the shelf reaches the target position, start floating motion
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    startPosition = transform.position;
                    oscillating = true;
                }
            }
            else
            {
                // Gentle floating movement for platforming challenge
                transform.position = startPosition + new Vector3(
                    Mathf.Sin(Time.time * 1.2f) * 0.15f, // Side to side movement
                    Mathf.Cos(Time.time * 1.2f) * 0.15f, // Up and down movement
                    0);
            }
        }
    }

    public void StartMoving()
    {
        startMoving = true;
    }
}
