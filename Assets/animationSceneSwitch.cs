using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAnimationAndChangeScene : MonoBehaviour
{
    private Animator animator;             // Animator component of the current GameObject
    public string animationName;           // Name of the animation to play
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            // Enable the Animator component
            animator.enabled = true;

            // Play the animation (optional if it is not playing by default)
            animator.Play(animationName);

            // Start the coroutine to wait for animation to finish and change the scene
            StartCoroutine(WaitForAnimationAndChangeScene());
        }
        else
        {
            Debug.LogError("Animator component not found on this GameObject.");
        }
    }

    IEnumerator WaitForAnimationAndChangeScene()
    {
        // Wait until the animation has finished playing
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
                                        animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}