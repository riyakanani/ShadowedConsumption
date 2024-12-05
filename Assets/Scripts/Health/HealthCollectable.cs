using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private float shadowGrowMultiplier = 1.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);

            ShadowFollow shadowFollow = FindObjectOfType<ShadowFollow>();
            if (shadowFollow != null)
            {
                shadowFollow.GrowShadow(shadowGrowMultiplier);
            }

            gameObject.SetActive(false);
        }
    }
}