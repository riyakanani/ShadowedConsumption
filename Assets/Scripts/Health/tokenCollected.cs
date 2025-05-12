using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class tokenCollected : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    private int total;
    private int count = 0;
    public bool AllCollected => count >= total;
    public float happinessPerToken = 0.5f;

    void Start()
    {
        total = CountHealthCollectables();

        if (textElement != null && total > 0)
        {
            // textElement.text = $"{count}/{total} Items";
            textElement.text = "";
        }
    }

    public int CountHealthCollectables()
    {
        return GameObject.FindGameObjectsWithTag("HealthCollectable").Length;
    }

    public void Increase()
    {
        count++; // Increment count

        HappinessManager.IncreaseWithToken(happinessPerToken);

        if (textElement != null)
        {
            textElement.text = count + "/" + total + " Items";
        }

        // if (count >= total)
        // {
        //     StartCoroutine(LoadNextScene());
        // }
    }

    public void ShowCount()
    {
        total = CountHealthCollectables();
        if (textElement != null && total > 0)
        {
            textElement.text = count + "/" + total + " Items";
        }
    }

    // private IEnumerator LoadNextScene()
    // {
    //     yield return new WaitForSeconds(1f); // optional delay
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // }
}
