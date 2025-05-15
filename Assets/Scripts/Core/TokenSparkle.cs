using UnityEngine;

public class TokenSparkle : MonoBehaviour
{
    [SerializeField] private ParticleSystem sparkleEffect; // Drag the particle system here

    private void Start()
    {
        // Ensure the particle system starts playing
        if (sparkleEffect != null)
        {
            sparkleEffect.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Optional: Stop the sparkles when collected
            if (sparkleEffect != null)
            {
                sparkleEffect.Stop();
            }

            // Destroy the token after a delay to let the sparkles finish
            // Destroy(gameObject, 0.5f);
        }
    }
}

