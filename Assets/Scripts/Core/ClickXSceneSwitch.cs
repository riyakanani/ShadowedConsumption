using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickXSceneSwitch : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            nextScene();
        }
    }

    public void nextScene(){
        if(HasNextScene()){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else{
            SceneManager.LoadScene(0);
        }
    }


    public bool HasNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        return currentSceneIndex + 1 < totalScenes;
    }
}
