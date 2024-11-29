using System.Collections;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

namespace DialogueSystem {
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished {get; protected set;}
        protected IEnumerator WriteText(string input, TMP_Text textHolder, AudioClip sound, float delayBetweenLines, float delay = .1f) // Change to TMP_Text
        {
            //type each line and play sound
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                SoundManager.instance.PlaySound(sound);
                yield return new WaitForSeconds(delay);
            }
            //wait after end of line
            // yield return new WaitForSeconds(delayBetweenLines);

            //wait after end of line for user Input
            //  yield return new WaitUntil(() => Input.GetMouseButton(0));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.X));
            finished = true;
        }
    }
}

