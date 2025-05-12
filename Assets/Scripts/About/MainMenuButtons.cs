using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void GoToAboutScene()
    {
        SceneManager.LoadScene("About"); // Make sure your scene is named "About" in the Build Settings
    }
}

