using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tokenCollected : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    private int total; // Declare but do not initialize here
    private int count = 0;

    void Start()
    {
        total = CountHealthCollectables(); // Assign value in Start()
        
        if (textElement != null)
        {
            textElement.text = count + "/" + total + " tokens";
        }
    }

    public int CountHealthCollectables()
    {
        return GameObject.FindGameObjectsWithTag("HealthCollectable").Length;
    }

    public void Increase()
    {
        count++; // Increment count
        if (textElement != null)
        {
            textElement.text = count + "/" + total + " tokens";
        }
    }
}