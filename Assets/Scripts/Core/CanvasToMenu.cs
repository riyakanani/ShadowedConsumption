using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToMenu : MonoBehaviour
{
    public Canvas menu;
    void Update()
    {
        // Pause or resume the game
        Time.timeScale = 0f;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
