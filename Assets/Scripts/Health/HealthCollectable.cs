using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private float shadowGrowMultiplier = 1.2f;
    [SerializeField] private tokenCollected tokenUI;
    public AudioSource audioSource;       // Assign via Inspector

    private BoxCollider2D itemCollider;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
        itemCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            itemCollider.enabled = false;
            Destroy(transform.GetChild(0).gameObject);
            audioSource.Play();

            if (tokenUI != null)
            {
                tokenUI.Increase();
            }
            else
            {
                Debug.LogWarning("TokenCollected reference is missing!");
            }


            ShadowFollow shadowFollow = FindObjectOfType<ShadowFollow>();
            if (shadowFollow != null)
            {
                shadowFollow.GrowShadow(shadowGrowMultiplier);
            }

            // Destroy(gameObject, audioSource.clip.length);
        }
    }
}