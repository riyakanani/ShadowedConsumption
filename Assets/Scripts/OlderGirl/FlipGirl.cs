using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGirl : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = true;  // Face left
        }
    }
}
