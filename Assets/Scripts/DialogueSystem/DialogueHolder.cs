using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem {
    public class DialogueHolder : MonoBehaviour
    {

        private IEnumerator dialogueSeq;
        [SerializeField] private GameObject X;
        [SerializeField] private GameObject Circle;
 
        private void OnEnable(){
            dialogueSeq = dialogueSequence();
            StartCoroutine(dialogueSeq);
        }

        private void Update() {
            if(Input.GetKey(KeyCode.E)){
                Deactivate();
                gameObject.SetActive(false);
                Circle.SetActive(false);
                X.SetActive(false);
                StopCoroutine(dialogueSeq);
            }
        }

        private IEnumerator dialogueSequence(){
            //types one line
            for(int i = 0; i < transform.childCount; i++){
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            Circle.SetActive(false);
            X.SetActive(false);
            gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name == "EndingMessage")
            {
                SceneManager.LoadScene(0); // or whatever scene you want to load next
            }
        }

        //sets other lines to inactive
        private void Deactivate(){
            for(int i = 0; i < transform.childCount; i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

    }

    
}