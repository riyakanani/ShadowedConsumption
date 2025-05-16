using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hurtColor = Color.red;
    [SerializeField] private float hurtFlashDuration = 0.2f;
    [SerializeField] private float startingHealth = 4.0f;

    public static float persistentHealth = -1f; // -1 means not yet initialized

    public float currentHealth { get; private set; }

    private Animator anim;
    private bool dead;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();

        // If health hasn't been set yet, use startingHealth
        if (persistentHealth < 0)
        {
            persistentHealth = startingHealth;
        }

        currentHealth = persistentHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        persistentHealth = currentHealth;

        HappinessManager.currentHappiness = Mathf.Max(HappinessManager.currentHappiness - 1f, 0f);

        if (currentHealth > 0)
        {
            StartCoroutine(FlashRed());
        }
        else
        {
            if (!dead)
            {
                if (ShouldReloadScene())
                {
                    anim.SetTrigger("die");
                    GetComponent<PlayerMovement>().enabled = false;
                    dead = true;
                    persistentHealth = -1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    currentHealth = 0.2f;
                    persistentHealth = currentHealth;
                    // StartCoroutine(FlashRed());
                }
            }
        }
    }


    public void AddHealth(float _value)
    {
        Debug.Log($"Trying to add health: {_value} (Current: {currentHealth}, Max: {startingHealth})");

        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        persistentHealth = currentHealth;

        Debug.Log($"New health: {currentHealth}");
    }


    private IEnumerator FlashRed()
    {
        if (spriteRenderer == null)
            yield break;

        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = hurtColor;

        yield return new WaitForSeconds(hurtFlashDuration);

        spriteRenderer.color = originalColor;
    }

    private bool ShouldReloadScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        return currentScene == "Level1 1editing" ||
            currentScene == "Level 2 final" ||
            currentScene == "Final Boss";
    }

}
