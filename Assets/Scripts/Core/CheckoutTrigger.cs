using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckoutTrigger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color activeColor = Color.white;

    public GameObject sparkleEffect; // Assign the child object here in the Inspector

    private tokenCollected collector;
    private bool activated = false;

    void Start()
    {
        collector = FindObjectOfType<tokenCollected>();

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (sparkleEffect != null)
        {
            sparkleEffect.SetActive(false); // Hide at start
        }
    }

    void Update()
    {
        if (collector != null && collector.AllCollected && !activated)
        {
            activated = true;
            spriteRenderer.color = activeColor;

            if (sparkleEffect != null)
            {
                sparkleEffect.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collector.AllCollected)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
