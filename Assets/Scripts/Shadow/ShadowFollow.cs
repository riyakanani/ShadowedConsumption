using System.Collections;
using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's Transform
    [SerializeField] private Vector3 offset = new Vector3(0f, -0.05f, 0); // Shadow offset
    [SerializeField] private float movementDelay = 0.1f; // Smooth follow

    private Vector3 targetPosition;
    private Vector3 originalScale;

    private Animator playerAnimator;
    private Animator shadowAnimator;

    void Start()
    {
        if (player != null)
        {
            playerAnimator = player.GetComponentInChildren<Animator>();
        }

        shadowAnimator = GetComponent<Animator>();
        if (shadowAnimator == null)
        {
            Debug.LogError("Shadow Animator is missing!");
        }

        originalScale = transform.localScale; // Save original scale
        targetPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned to ShadowFollow.");
            return;
        }

        // Smooth follow with offset
        targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementDelay);

        // Flip shadow based on player's facing direction
        Vector3 scale = originalScale;
        scale.x *= Mathf.Sign(player.localScale.x); // Flip X based on player
        transform.localScale = scale;

        // Sync animations
        if (playerAnimator != null && shadowAnimator != null)
        {
            SyncAnimationState();
            SyncAnimatorParameters();
        }
    }

    private void SyncAnimatorParameters()
    {
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
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        shadowAnimator.Play(stateInfo.fullPathHash, 0, stateInfo.normalizedTime);
    }

    public void GrowShadow(float scaleMultiplier, float anticipationFactor = 0.6f, float duration = 1.0f)
    {
        StartCoroutine(GrowShadowWithAnticipation(scaleMultiplier, anticipationFactor, duration));
    }

    private IEnumerator GrowShadowWithAnticipation(float scaleMultiplier, float anticipationFactor, float duration)
    {
        Vector3 baseScale = originalScale;
        Vector3 shrinkScale = baseScale * anticipationFactor;
        Vector3 growScale = baseScale * scaleMultiplier;

        float shrinkDuration = duration * 0.4f;
        float growDuration = duration * 0.6f;

        float elapsed = 0f;
        while (elapsed < shrinkDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / shrinkDuration);
            transform.localScale = Vector3.Lerp(baseScale, shrinkScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < growDuration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed / growDuration);
            transform.localScale = Vector3.Lerp(shrinkScale, growScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = growScale;
    }
}
