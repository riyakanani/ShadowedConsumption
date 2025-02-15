using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 moveDirection; // Direction the shelf moves at first
    private float moveSpeed; // Speed of movement
    private float shrinkSpeed; // Speed of shrinking
    private bool startMoving = false;
    private Vector3 minScale; // Minimum size it can shrink to
    private float moveDuration; // How long the shelf moves outward before stopping
    private float moveTimer = 0f;
    private bool oscillating = false; // Whether the shelf should now oscillate
    private Vector3 startPosition; // Store the stopping position
    private float oscillationSpeed; // Speed of floating motion
    private float oscillationAmount; // How much it moves up/down or side to side

    private void Start()
    {
        // Set different randomized values for each shelf
        moveSpeed = Random.Range(1f, 2.5f); // Slightly lowered move speed
        shrinkSpeed = Random.Range(0.05f, 0.1f); // Slightly slower shrink
        moveDuration = Random.Range(2f, 3.5f); // Different move times

        // Prevent the first shelves from moving too high by limiting the random direction
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-0.5f, 1f); // Now max 1 instead of 1.5 to prevent too much upward movement
        moveDirection = new Vector2(randomX, randomY).normalized;

        minScale = transform.localScale * 0.7f; // Stops shrinking at 70% of original size

        oscillationSpeed = Random.Range(0.5f, 1.2f); // Different float speeds
        oscillationAmount = Random.Range(0.1f, 0.2f); // Controlled float range
    }

    private void Update()
    {
        if (startMoving)
        {
            if (!oscillating)
            {
                // Move outward but within a constrained range
                transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
                moveTimer += Time.deltaTime;

                // Shrink slightly but stop shrinking at minScale
                if (transform.localScale.x > minScale.x && transform.localScale.y > minScale.y)
                {
                    transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
                }

                // Stop movement after moveDuration and start oscillating
                if (moveTimer >= moveDuration)
                {
                    startPosition = transform.position; // Store stopping position
                    oscillating = true; // Switch to oscillation phase
                }
            }
            else
            {
                // Slight floating movement to maintain platforming
                transform.position = startPosition + new Vector3(
                    Mathf.Sin(Time.time * oscillationSpeed) * oscillationAmount, // Side to side movement
                    Mathf.Cos(Time.time * oscillationSpeed) * oscillationAmount, // Up and down movement
                    0);
            }
        }
    }

    public void StartMoving()
    {
        Debug.Log("🚀 Shelf started moving: " + gameObject.name + " Direction: " + moveDirection);
        startMoving = true;
    }
}
