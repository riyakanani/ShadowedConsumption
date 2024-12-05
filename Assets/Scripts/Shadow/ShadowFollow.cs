using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's Transform
    [SerializeField] private Vector3 offset = new Vector3(-0.2f, -0.1f, 0); // Default shadow offset
    private Animator playerAnimator; // Reference to the player's Animator
    private Animator shadowAnimator; // Reference to the shadow's Animator

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
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Update shadow's position relative to the player
            transform.position = player.position + offset;

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

    public void GrowShadow(float scaleMultiplier)
    {
        // Scale the shadow by the multiplier
        transform.localScale *= scaleMultiplier;
    }
}
