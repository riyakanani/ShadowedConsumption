using System.Collections;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

namespace DialogueSystem {
    public class DialogueLine : DialogueBaseClass
    {
        private TMP_Text textHolder; // Change to TMP_Text
        [SerializeField] private string input;

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>(); // Get the TMP_Text component
            StartCoroutine(WriteText(input, textHolder)); // Call the base class method
        }
    }
}
