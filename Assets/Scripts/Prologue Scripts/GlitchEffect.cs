using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D spotlight; // Reference to the 2D spotlight
    public GameObject shadowMonster; // Reference to the shadow monster
    public Animator shadowAnimator; // Animator for the shadow monster's animations
    public Animator eyeAnimator; // Animator for the glowing eyes
    public GameObject girlCharacter; // Reference to the girl character
    public GameObject shadowNextToGirl; // Reference to the shadow that appears next to the girl
    public GameObject openBook; // Reference to the open book object
    public GameObject closedBook; // Reference to the closed book object
    public Animator handAnimator; // Animator for the girl's hand

    public GameObject[] sceneObjects; // All objects to disable during the glitch
    public AudioSource[] sceneAudioSources; // Audio sources to stop during the glitch

    public float glitchDelay = 3f; // Time before the glitch happens
    public float initialDelay = 10f; // Time before the glitch sequence starts
    public float flickerDuration = 1f; // Duration of the flicker effect
    public float flickerInterval = 0.3f; // Time between flickers
    public float resetDelay = 5f; // Time to wait before resetting the scene

    private bool isGlitchActive = false;
    private Vector3 girlOriginalPosition;
    private Quaternion girlOriginalRotation;

    private Dictionary<GameObject, bool> originalAnimatorStates = new Dictionary<GameObject, bool>();
    private Dictionary<AudioSource, bool> originalAudioStates = new Dictionary<AudioSource, bool>();

    void Start()
    {
        Debug.Log("GlitchEffect script is active.");

        // Save initial states of the girl character
        if (girlCharacter != null)
        {
            girlOriginalPosition = girlCharacter.transform.position;
            girlOriginalRotation = girlCharacter.transform.rotation;
        }

        // Save initial animator and audio states
        foreach (GameObject obj in sceneObjects)
        {
            if (obj.TryGetComponent<Animator>(out Animator animator))
            {
                originalAnimatorStates[obj] = animator.enabled;
            }
        }

        foreach (AudioSource audioSource in sceneAudioSources)
        {
            originalAudioStates[audioSource] = audioSource.isPlaying;
        }

        // Ensure spotlight, shadow monster, and shadow next to girl are off at start
        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(false);
        }

        if (shadowMonster != null)
        {
            shadowMonster.SetActive(false);
        }

        if (shadowNextToGirl != null)
        {
            shadowNextToGirl.SetActive(false);
        }

        if (shadowAnimator != null)
        {
            shadowAnimator.enabled = false;
        }

        if (eyeAnimator != null)
        {
            eyeAnimator.enabled = false;
        }

        // Initially disable the closed book
        if (closedBook != null)
        {
            closedBook.SetActive(false);
        }

        // Automatically start glitch after initial delay
        StartCoroutine(StartGlitchAfterDelay());
    }

    private IEnumerator StartGlitchAfterDelay()
    {
        Debug.Log($"Waiting for {initialDelay} seconds before triggering glitch...");
        yield return new WaitForSeconds(initialDelay);
        TriggerGlitch();
    }

    public void TriggerGlitch()
    {
        if (isGlitchActive) return; // Prevent multiple triggers
        isGlitchActive = true;
        Debug.Log("Glitch triggered!");
        StartCoroutine(GlitchSequence());
    }

    private IEnumerator GlitchSequence()
    {
        Debug.Log("Starting glitch sequence...");

        if (girlCharacter != null)
        {
            girlCharacter.SetActive(false);
        }

        if (shadowMonster != null)
        {
            shadowMonster.SetActive(true);
        }

        yield return new WaitForSeconds(glitchDelay);

        foreach (GameObject obj in sceneObjects)
        {
            if (obj.TryGetComponent<Animator>(out Animator animator))
            {
                animator.enabled = false;
            }
        }

        foreach (AudioSource audioSource in sceneAudioSources)
        {
            audioSource.Stop();
        }

        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(true);
            yield return StartCoroutine(FlickerLight());
        }

        if (shadowAnimator != null)
        {
            shadowAnimator.enabled = true;
        }

        if (eyeAnimator != null)
        {
            eyeAnimator.enabled = true;
        }

        Debug.Log("Glitch sequence completed.");

        yield return new WaitForSeconds(resetDelay);
        ResetScene();
    }

    private IEnumerator FlickerLight()
    {
        float elapsed = 0f;

        while (elapsed < flickerDuration)
        {
            if (spotlight != null)
            {
                spotlight.enabled = !spotlight.enabled;
            }

            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        if (spotlight != null)
        {
            spotlight.enabled = true;
        }
    }

    private void ResetScene()
    {
        Debug.Log("Resetting scene to original state...");

        if (shadowMonster != null)
        {
            shadowMonster.SetActive(false);
        }

        if (girlCharacter != null)
        {
            girlCharacter.SetActive(true);
            girlCharacter.transform.position = girlOriginalPosition;
            girlCharacter.transform.rotation = girlOriginalRotation;
        }

        if (shadowNextToGirl != null)
        {
            shadowNextToGirl.SetActive(true);
        }

        foreach (var entry in originalAnimatorStates)
        {
            if (entry.Key.TryGetComponent<Animator>(out Animator animator))
            {
                animator.enabled = entry.Value;
            }
        }

        foreach (var entry in originalAudioStates)
        {
            if (entry.Key.TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                if (entry.Value)
                {
                    audioSource.Play();
                }
            }
        }

        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(false);
        }

        StartCoroutine(TriggerHandAndBookAnimation());
    }

    private IEnumerator TriggerHandAndBookAnimation()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        if (handAnimator != null)
        {
            Debug.Log("Starting hand animation...");
            handAnimator.SetTrigger("StartHandAnimation"); // Trigger the hand animation
        }

        // Switch the book from open to closed
        if (openBook != null && closedBook != null)
        {
            Debug.Log("Switching book from open to closed...");
            openBook.SetActive(false);
            closedBook.SetActive(true);
        }
    }
}
