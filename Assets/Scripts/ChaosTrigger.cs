using UnityEngine;

public class ChaosTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            Debug.Log("✅ Player entered ChaosTrigger - Shelves should start moving!");

            // Find all shelves and start moving them
            foreach (MovingPlatform platform in FindObjectsOfType<MovingPlatform>())
            {
                platform.StartMoving();
            }
        }
    }
}

