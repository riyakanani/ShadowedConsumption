using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialSceneSwitch : MonoBehaviour
{
    public void TutorialScene()
    {
        SceneManager.LoadScene("How To Scene"); // Replace "MainMenu" with your actual scene name
    }
}

