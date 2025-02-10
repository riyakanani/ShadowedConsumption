using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToNextScene : MonoBehaviour
{
    public string sceneToLoad = "NeighborhoodScene"; // Change to your scene name

    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
