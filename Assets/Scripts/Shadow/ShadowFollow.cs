//using System.Collections;
//using UnityEngine;

//public class ShadowFollow : MonoBehaviour
//{
//    [SerializeField] private Transform player; // Reference to the player's Transform
//    [SerializeField] private Vector3 offset = new Vector3(-0.2f, -0.1f, 0); // Default shadow offset
//    private Animator playerAnimator; // Reference to the player's Animator
//    private Animator shadowAnimator; // Reference to the shadow's Animator

//    void Start()
//    {
//        // Get the player's Animator
//        if (player != null)
//        {
//            playerAnimator = player.GetComponent<Animator>();
//        }

//        // Get the shadow's Animator
//        shadowAnimator = GetComponent<Animator>();

//        if (shadowAnimator == null)
//        {
//            Debug.LogError("Shadow Animator is missing! Ensure the shadow has an Animator component.");
//        }
//    }

//    void LateUpdate()
//    {
//        if (player != null)
//        {
//            // Update shadow's position relative to the player
//            transform.position = player.position + offset;

//            // Mirror the shadow's direction based on the player's localScale
//            Vector3 playerScale = player.localScale;
//            transform.localScale = new Vector3(playerScale.x, transform.localScale.y, transform.localScale.z);

//            // Sync animations if both Animators exist
//            if (playerAnimator != null && shadowAnimator != null)
//            {
//                SyncAnimationState();
//                SyncAnimatorParameters();
//            }
//        }
//        else
//        {
//            Debug.LogWarning($"Player Transform is not assigned for {gameObject.name}");
//        }
//    }

//    private void SyncAnimatorParameters()
//    {
//        // Iterate through all parameters in the player's Animator and copy them to the shadow
//        foreach (AnimatorControllerParameter parameter in playerAnimator.parameters)
//        {
//            switch (parameter.type)
//            {
//                case AnimatorControllerParameterType.Bool:
//                    shadowAnimator.SetBool(parameter.name, playerAnimator.GetBool(parameter.name));
//                    break;

//                case AnimatorControllerParameterType.Float:
//                    shadowAnimator.SetFloat(parameter.name, playerAnimator.GetFloat(parameter.name));
//                    break;

//                case AnimatorControllerParameterType.Int:
//                    shadowAnimator.SetInteger(parameter.name, playerAnimator.GetInteger(parameter.name));
//                    break;

//                case AnimatorControllerParameterType.Trigger:
//                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(parameter.name))
//                    {
//                        shadowAnimator.SetTrigger(parameter.name);
//                    }
//                    break;
//            }
//        }
//    }

//    private void SyncAnimationState()
//    {
//        // Get the player's current animation state
//        AnimatorStateInfo playerStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

//        // Force the shadow to play the same animation state as the player
//        shadowAnimator.Play(playerStateInfo.fullPathHash, 0, playerStateInfo.normalizedTime);
//    }

//    public void GrowShadow(float scaleMultiplier, float anticipationFactor = 0.6f, float duration = 1.0f)
//    {
//        StartCoroutine(GrowShadowWithAnticipation(scaleMultiplier, anticipationFactor, duration));
//    }

//    private IEnumerator GrowShadowWithAnticipation(float scaleMultiplier, float anticipationFactor, float duration)
//    {
//        Vector3 originalScale = transform.localScale;
//        Vector3 shrunkScale = originalScale * anticipationFactor;
//        Vector3 targetScale = originalScale * scaleMultiplier;

//        float shrinkDuration = duration * 0.4f;
//        float growDuration = duration * 0.6f;

//        float elapsed = 0f;

//        // Shrink phase 
//        while (elapsed < shrinkDuration)
//        {
//            float t = elapsed / shrinkDuration;
//            t = Mathf.SmoothStep(0f, 1f, t);
//            transform.localScale = Vector3.Lerp(originalScale, shrunkScale, t);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//        transform.localScale = shrunkScale;

//        // Grow phase
//        elapsed = 0f;
//        while (elapsed < growDuration)
//        {
//            float t = elapsed / growDuration;
//            t = Mathf.SmoothStep(0f, 1f, t);
//            transform.localScale = Vector3.Lerp(shrunkScale, targetScale, t);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//        transform.localScale = targetScale;
//    }

//}

using System.Collections;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's Transform
    [SerializeField] private Vector3 offset = new Vector3(0f, -0.05f, 0); // Shadow offset close to the player
    private Animator playerAnimator; // Reference to the player's Animator
    private Animator shadowAnimator; // Reference to the shadow's Animator

    [SerializeField] private float movementDelay = 0.1f;  // Slight delay in shadow's movement
    private Vector3 targetPosition;  // Target position of the shadow

    void Start()
    {
        // Get the player's Animator
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
        }

        // Get the shadow's Animator
        shadowAnimator = GetComponent<Animator>();

        if (shadowAnimator == null)
        {
            Debug.LogError("Shadow Animator is missing! Ensure the shadow has an Animator component.");
        }

        targetPosition = transform.position;  // Initialize target position
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Update shadow's position relative to the player with slight delay
            targetPosition = player.position + offset;

            // Lerp towards target position with a small delay
            transform.position = Vector3.Lerp(transform.position, targetPosition, movementDelay);

            // Mirror the shadow's direction based on the player's localScale
            Vector3 playerScale = player.localScale;
            transform.localScale = new Vector3(playerScale.x, transform.localScale.y, transform.localScale.z);

            // Sync animations if both Animators exist
            if (playerAnimator != null && shadowAnimator != null)
            {
                SyncAnimationState();
                SyncAnimatorParameters();
            }
        }
        else
        {
            Debug.LogWarning($"Player Transform is not assigned for {gameObject.name}");
        }
    }

    private void SyncAnimatorParameters()
    {
        // Iterate through all parameters in the player's Animator and copy them to the shadow
        foreach (AnimatorControllerParameter parameter in playerAnimator.parameters)
        {
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Bool:
                    shadowAnimator.SetBool(parameter.name, playerAnimator.GetBool(parameter.name));
                    break;

                case AnimatorControllerParameterType.Float:
                    shadowAnimator.SetFloat(parameter.name, playerAnimator.GetFloat(parameter.name));
                    break;

                case AnimatorControllerParameterType.Int:
                    shadowAnimator.SetInteger(parameter.name, playerAnimator.GetInteger(parameter.name));
                    break;

                case AnimatorControllerParameterType.Trigger:
                    if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(parameter.name))
                    {
                        shadowAnimator.SetTrigger(parameter.name);
                    }
                    break;
            }
        }
    }

    private void SyncAnimationState()
    {
        // Get the player's current animation state
        AnimatorStateInfo playerStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        // Force the shadow to play the same animation state as the player
        shadowAnimator.Play(playerStateInfo.fullPathHash, 0, playerStateInfo.normalizedTime);
    }

    public void GrowShadow(float scaleMultiplier, float anticipationFactor = 0.6f, float duration = 1.0f)
    {
        StartCoroutine(GrowShadowWithAnticipation(scaleMultiplier, anticipationFactor, duration));
    }

    private IEnumerator GrowShadowWithAnticipation(float scaleMultiplier, float anticipationFactor, float duration)
    {
        Vector3 originalScale = transform.localScale;
        Vector3 shrunkScale = originalScale * anticipationFactor;
        Vector3 targetScale = originalScale * scaleMultiplier;

        float shrinkDuration = duration * 0.4f;
        float growDuration = duration * 0.6f;

        float elapsed = 0f;

        // Shrink phase 
        while (elapsed < shrinkDuration)
        {
            float t = elapsed / shrinkDuration;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale, shrunkScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = shrunkScale;

        // Grow phase
        elapsed = 0f;
        while (elapsed < growDuration)
        {
            float t = elapsed / growDuration;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(shrunkScale, targetScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}

