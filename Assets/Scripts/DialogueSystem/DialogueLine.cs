using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private TMP_Text textHolder;
        private AudioSource audioSource;

        [Header("Text Options")]
        [SerializeField] private string input;

        [Header("Sound")]
        [SerializeField] private AudioClip sound;

        [Header("Time Parameter")]
        [SerializeField] private float delay = 0.05f;
        [SerializeField] private float delayBetweenLines = 1f;

        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder = null;

        [Header("Action Trigger (Optional)")]
        [SerializeField] private GameObject actionObject;

        [Header("Click Option (UI Button)")]
        [SerializeField] private Button xButton;

        private IEnumerator lineAppear;

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.loop = true;

            imageHolder.sprite = characterSprite;
            // imageHolder.preserveAspect = true;

            if (xButton != null)
            {
                xButton.onClick.AddListener(HandleDialogueSkipOrFinish);
            }
        }

        private void OnEnable()
        {
            ResetLine();
            lineAppear = WriteText(input);
            StartCoroutine(lineAppear);

            if (actionObject != null)
            {
                actionObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                HandleDialogueSkipOrFinish();
            }
        }

        private void HandleDialogueSkipOrFinish()
        {
            if (textHolder.text != input)
            {
                StopCoroutine(lineAppear);
                textHolder.text = input;
                audioSource.Stop();
            }
            else
            {
                finished = true;

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int totalScenes = SceneManager.sceneCountInBuildSettings;
                if (currentSceneIndex == totalScenes - 1)
                {
                    // If it's the last scene, go back to scene 0 (usually the main menu)
                    SceneManager.LoadScene(0);
                }
            }
        }

        private void ResetLine()
        {
            textHolder.text = "";
            finished = false;

            if (actionObject != null)
            {
                actionObject.SetActive(false);
            }
        }

        private IEnumerator WriteText(string input)
        {
            textHolder.text = "";
            audioSource.Play();

            foreach (char c in input)
            {
                textHolder.text += c;
                yield return new WaitForSeconds(delay);
            }

            audioSource.Stop();
            yield return new WaitForSeconds(delayBetweenLines);
            finished = true;
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                int totalScenes = SceneManager.sceneCountInBuildSettings;
                if (currentSceneIndex == totalScenes - 1)
                {
                    // If it's the last scene, go back to scene 0 (usually the main menu)
                    SceneManager.LoadScene(0);
                }
        }
    }
}
