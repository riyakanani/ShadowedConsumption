using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public float fadeDuration = 1f;
    private float currentAlpha = 0f;
    private float targetAlpha = 1f;

    void Start()
    {
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0);
    }

    void Update()
    {
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime / fadeDuration);
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, currentAlpha);
    }
}
