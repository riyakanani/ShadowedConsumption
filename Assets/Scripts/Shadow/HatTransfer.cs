using System.Collections;
using UnityEngine;

public class HatTransfer : MonoBehaviour
{
    public GameObject friendHat;
    public GameObject playerHat;
    public GameObject shadowHat;
    public ParticleSystem sparkleEffect;
    public Animator sharedAnimator;
    public GameObject player; // Reference to the player GameObject
    public float shadowFocusDelay = 1f; // Delay before focusing on shadow
    public float shadowAnimationDuration = 2f; // Duration of shadow animation

    public void TransferHat()
    {
        if (friendHat != null) friendHat.SetActive(false);
        if (playerHat != null) playerHat.SetActive(true);
        if (shadowHat != null) shadowHat.SetActive(true);

        // Start the shadow animation sequence
        StartCoroutine(TriggerShadowEffects());
    }

    private IEnumerator TriggerShadowEffects()
    {
        // Wait for the delay before focusing on the shadow
        yield return new WaitForSeconds(shadowFocusDelay);

        // Hide the player
        if (player != null)
        {
            player.SetActive(false);
        }

        // Play the sparkle effect
        if (sparkleEffect != null)
        {
            sparkleEffect.Play();
        }

        // Trigger the shadow's independent animation
        if (sharedAnimator != null)
        {
            sharedAnimator.SetTrigger("ShadowJump");
        }

        // Wait for the shadow animation duration
        yield return new WaitForSeconds(shadowAnimationDuration);

        // Stop the sparkle effect
        if (sparkleEffect != null)
        {
            sparkleEffect.Stop();
        }

        // Show the player again after the shadow animation
        if (player != null)
        {
            player.SetActive(true);
        }
    }
}
