using UnityEngine;

public static class HappinessManager
{
    public static float currentHappiness = 10f;
    public static float maxHappiness = 10f;

    public static void DecreaseAfterNarrativeScene(float amount)
    {
        maxHappiness = Mathf.Max(2f, maxHappiness - amount); // minimum cap
        currentHappiness = Mathf.Min(currentHappiness, maxHappiness);
    }

    public static void IncreaseWithToken(float amount)
    {
        currentHappiness = Mathf.Min(currentHappiness + amount, maxHappiness);
    }
}
