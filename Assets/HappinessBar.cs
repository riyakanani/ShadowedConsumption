using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For the Image component

public class HappinessBar : MonoBehaviour
{
    // Reference to the happiness bar (image component)
    [SerializeField] private Image totalHappinessBar;   // The total happiness bar (background)
    [SerializeField] private Image currentHappinessBar; // The current happiness bar (foreground)

    // Reference to the happiness value (you can set this in your other script, just like the health value)
    private float currentHappiness = 10f;  // Assume max happiness is 10
    private float happinessDecrement = 0.5f; // Amount to decrease happiness by (you can adjust this)

    // Start is called before the first frame update
    void Start()
    {
        // Initially set the total happiness bar (background) to be full
        totalHappinessBar.fillAmount = 1f; // Set max (full) bar (100% when happiness is at max)

        // Set the current happiness bar to match the current happiness value
        currentHappinessBar.fillAmount = currentHappiness / 10; // This will initially be 1 (full)
    }

    // This method will be called to decrease happiness when the first text message appears
    public void DecreaseHappiness()
    {
        // Decrease happiness
        currentHappiness -= happinessDecrement;

        // Ensure that happiness doesn't go below 0
        if (currentHappiness < 0f)
        {
            currentHappiness = 0f;
        }

        // Update the current happiness bar (foreground) based on the current happiness
        currentHappinessBar.fillAmount = currentHappiness / 10;  // Update to reflect the reduced happiness
    }

    // This method will be triggered when the first text message appears
    public void OnFirstTextMessageAppears()
    {
        // Call the function to decrease happiness
        DecreaseHappiness();
    }
}
