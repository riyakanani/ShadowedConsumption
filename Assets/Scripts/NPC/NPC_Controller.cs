using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject X;
    public void ActiveDialogue(){
        dialogue.SetActive(true);
    }
    public bool DialogueActive(){
        return dialogue.activeInHierarchy;
    }

}
