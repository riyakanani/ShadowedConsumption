using UnityEngine;

public class ShadowHandCollision : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Reference to the Audio Source
    [SerializeField] private string playerTag = "Player"; // Tag to identify the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            // Play the sound effect
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Optional: Add additional effects here
            Debug.Log("Player collided with the shadow hand!");
        }
    }
}
