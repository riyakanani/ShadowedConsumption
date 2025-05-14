using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Intro Play"); // Replace "MainMenu" with your actual scene name
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name); // Replace "MainMenu" with your actual scene name
    }

    public void Escape(){
        Application.Quit();
    }
}

