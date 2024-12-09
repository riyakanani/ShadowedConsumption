//https://www.youtube.com/watch?v=iCybBI9_M2E&ab_channel=Pandemonium

using System.Collections;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private TMP_Text textHolder; // Change to TMP_Text

        [Header("Text Options")]
        [SerializeField] private string input;

        [Header("Sound")]
        [SerializeField] private AudioClip sound;

        [Header("Time Parameter")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder = null;

        [Header("Action Trigger (Optional)")]
        [SerializeField] private GameObject actionObject; // The object to activate during this dialogue line

        [Header("Hat Transfer Trigger (Optional)")]
        [SerializeField] private HatTransfer hatTransfer; // Reference to the HatTransfer script

        private IEnumerator lineAppear;

        private void Awake()
        {
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input, textHolder, sound, delayBetweenLines, delay);
            StartCoroutine(lineAppear);

            // Trigger the action object (if assigned)
            if (actionObject != null)
            {
                actionObject.SetActive(true);
            }

            // Trigger the hat transfer (if assigned)
            if (hatTransfer != null)
            {
                hatTransfer.TransferHat();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                {
                    finished = true;
                }
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";
            finished = false;

            // Ensure the actionObject is inactive at the start
            if (actionObject != null)
            {
                actionObject.SetActive(false);
            }
        }
    }
}
