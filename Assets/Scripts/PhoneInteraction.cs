using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    [SerializeField] private Animator phoneAnimator; // Reference to the phone's Animator
    [SerializeField] private GameObject messageBubble;  // The message bubble GameObject
    [SerializeField] private GameObject secondCharacter; // The second character GameObject

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the trigger is the Player
        if (collision.CompareTag("Player"))
        {
            phoneAnimator.SetBool("Glow", false); // Stop the glow animation
            messageBubble.SetActive(true); // Show the message bubble

            // Hide the message after 5 seconds and make the second character appear
            Invoke("HideMessage", 5f);
            Invoke("ShowSecondCharacter", 5f);
        }
    }

    private void HideMessage()
    {
        messageBubble.SetActive(false); // Hide the message bubble
    }

    private void ShowSecondCharacter()
    {
        secondCharacter.SetActive(true); // Make the second character appear
    }
}
