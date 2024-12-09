using UnityEngine;

public class ShadowAnimatorController : MonoBehaviour
{
    public Animator shadowAnimator; // Reference to the shadow's Animator

    public void TriggerIndependentMovement()
    {
        if (shadowAnimator != null)
        {
            shadowAnimator.SetTrigger("StartIndependentMovement");
        }
        else
        {
            Debug.LogError("Shadow Animator is not assigned!");
        }
    }
}
