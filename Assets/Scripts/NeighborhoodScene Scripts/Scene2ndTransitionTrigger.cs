using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2ndTransitionTrigger : MonoBehaviour
{
    public string nextSceneName; // Set this in the inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

