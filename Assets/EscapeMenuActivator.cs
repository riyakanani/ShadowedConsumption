using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuActivator : MonoBehaviour
{
    public bool EscapeMenuOpen = false;
    public Canvas menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeMenuOpen = !EscapeMenuOpen;
            menu.gameObject.SetActive(EscapeMenuOpen);

            // Pause or resume the game
            Time.timeScale = EscapeMenuOpen ? 0f : 1f;
        }
    }
}
