using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Prologue 1.2"); // Replace "MainMenu" with your actual scene name
    }
}