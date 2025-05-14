using UnityEngine;

public class ChaosTrigger : MonoBehaviour
{
    [SerializeField] private Rotate_Movement rotateMovementScript;  // Reference to the Rotate_Movement script

    private void OnTriggerEnter2D(Collider2D other)
    {
            // Find all shelves and start moving them
            foreach (MovingPlatform platform in FindObjectsOfType<MovingPlatform>())
            {
                platform.StartMoving();
            }
    }
}
