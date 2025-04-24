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
    public float currentHealth{get; private set;}
    private Animator anim;
    private bool dead;
    private void Awake(){
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentHealth = startingHealth;
        anim = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            // Remove anim.SetTrigger("hurt"); 
            StartCoroutine(FlashRed()); // Only flash red, don't play "hurt"
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
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

}
