using System.Collections;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

namespace DialogueSystem {
    public class DialogueBaseClass : MonoBehaviour
    {
        protected IEnumerator WriteText(string input, TMP_Text textHolder) // Change to TMP_Text
        {
            textHolder.text = ""; // Clear existing text
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

