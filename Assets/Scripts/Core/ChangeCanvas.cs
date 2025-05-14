using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public Canvas menu;
    public Canvas canvas;
    public void LoadCanvas (){
        menu.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }
}
