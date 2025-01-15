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
            Debug.Log("Spotlight reference is valid.");
            spotlight.gameObject.SetActive(false); // Initially disable GameObject
        }
        else
        {
            Debug.LogError("Spotlight reference is null!");
        }

        if (shadowMonster != null)
        {
            shadowMonster.SetActive(false); // Disable shadow monster at the start
        }

        if (shadowNextToGirl != null)
        {
            shadowNextToGirl.SetActive(false); // Disable shadow next to girl at the start
        }

        if (shadowAnimator != null)
        {
            shadowAnimator.enabled = false; // Disable shadow animations at the start
        }

        if (eyeAnimator != null)
        {
            eyeAnimator.enabled = false; // Disable eye animations at the start
        }

        // Automatically start glitch after initial delay
        StartCoroutine(StartGlitchAfterDelay());
    }

    private IEnumerator StartGlitchAfterDelay()
    {
        Debug.Log($"Waiting for {initialDelay} seconds before triggering glitch...");
        yield return new WaitForSeconds(initialDelay);
        TriggerGlitch(); // Trigger the glitch sequence
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

        // Immediately disable the girl character and enable the shadow monster
        if (girlCharacter != null)
        {
            Debug.Log("Disabling girl character...");
            girlCharacter.SetActive(false); // Girl disappears immediately
        }

        if (shadowMonster != null)
        {
            Debug.Log("Enabling shadow monster...");
            shadowMonster.SetActive(true); // Shadow appears immediately
        }

        // Wait for the glitch delay
        yield return new WaitForSeconds(glitchDelay);
        Debug.Log("Glitch delay elapsed.");

        // Disable animations and sounds
        foreach (GameObject obj in sceneObjects)
        {
            if (obj.TryGetComponent<Animator>(out Animator animator))
            {
                animator.enabled = false;
                Debug.Log($"Disabled animator on {obj.name}");
            }
        }

        foreach (AudioSource audioSource in sceneAudioSources)
        {
            audioSource.Stop();
            Debug.Log($"Stopped audio source on {audioSource.gameObject.name}");
        }

        // Enable spotlight GameObject before flickering
        if (spotlight != null)
        {
            Debug.Log("Enabling spotlight GameObject...");
            spotlight.gameObject.SetActive(true); // Activate the GameObject
            yield return StartCoroutine(FlickerLight()); // Perform flickering
        }

        // After spotlight flickering, enable animations for shadow and eyes
        if (shadowAnimator != null)
        {
            Debug.Log("Enabling shadow animations...");
            shadowAnimator.enabled = true; // Start shadow animations
        }

        if (eyeAnimator != null)
        {
            Debug.Log("Enabling eye animations...");
            eyeAnimator.enabled = true; // Start eye animations
        }

        Debug.Log("Glitch sequence completed.");

        // Wait for reset delay, then reset the scene
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
                spotlight.enabled = !spotlight.enabled; // Toggle the light
                Debug.Log($"Spotlight flicker: {spotlight.enabled}");
            }

            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        if (spotlight != null)
        {
            spotlight.enabled = true; // Ensure the light stays on afterward
            Debug.Log("Flicker complete. Spotlight enabled.");
        }
    }

    private void ResetScene()
    {
        Debug.Log("Resetting scene to original state...");

        // Disable the shadow monster
        if (shadowMonster != null)
        {
            shadowMonster.SetActive(false);
        }

        // Re-enable the girl character
        if (girlCharacter != null)
        {
            girlCharacter.SetActive(true);
            girlCharacter.transform.position = girlOriginalPosition;
            girlCharacter.transform.rotation = girlOriginalRotation;
        }

        // Re-enable the shadow next to the girl
        if (shadowNextToGirl != null)
        {
            shadowNextToGirl.SetActive(true); // Simply make the shadow visible
            Debug.Log("Shadow next to girl enabled.");
        }

        // Restore animations and sounds
        foreach (var entry in originalAnimatorStates)
        {
            if (entry.Key.TryGetComponent<Animator>(out Animator animator))
            {
                animator.enabled = entry.Value;
                Debug.Log($"Restored animator on {entry.Key.name} to {entry.Value}");
            }
        }

        foreach (var entry in originalAudioStates)
        {
            if (entry.Key.TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                if (entry.Value)
                {
                    audioSource.Play();
                    Debug.Log($"Restarted audio source on {entry.Key.name}");
                }
            }
        }

        // Disable spotlight
        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(false);
        }

        Debug.Log("Scene reset complete.");
    }
}
