////using System.Collections;
////using System.Collections.Generic;
////using UnityEngine;

////public class GlitchEffect : MonoBehaviour
////{
////    public UnityEngine.Rendering.Universal.Light2D spotlight; // Reference to the 2D spotlight
////    public GameObject shadowMonster; // Reference to the shadow monster
////    public Animator shadowAnimator; // Animator for the shadow monster's animations
////    public Animator eyeAnimator; // Animator for the glowing eyes
////    public GameObject girlCharacter; // Reference to the girl character
////    public GameObject shadowNextToGirl; // Reference to the shadow that appears next to the girl
////    public GameObject openBook; // Reference to the open book object
////    public GameObject closedBook; // Reference to the closed book object
////    public Animator handAnimator; // Animator for the girl's hand

////    public GameObject sittingGirl;  // Assign the sitting girl in the Inspector
////    public GameObject standingGirl; // Assign the standing girl in the Inspector

////    public GameObject[] sceneObjects; // All objects to disable during the glitch
////    public AudioSource[] sceneAudioSources; // Audio sources to stop during the glitch

////    public float glitchDelay = 3f; // Time before the glitch happens
////    public float initialDelay = 10f; // Time before the glitch sequence starts
////    public float flickerDuration = 1f; // Duration of the flicker effect
////    public float flickerInterval = 0.3f; // Time between flickers
////    public float resetDelay = 5f; // Time to wait before resetting the scene

////    private bool isGlitchActive = false;
////    private Vector3 girlOriginalPosition;
////    private Quaternion girlOriginalRotation;

////    private Dictionary<GameObject, bool> originalAnimatorStates = new Dictionary<GameObject, bool>();
////    private Dictionary<AudioSource, bool> originalAudioStates = new Dictionary<AudioSource, bool>();

////    void Start()
////    {
////        Debug.Log("GlitchEffect script is active.");

////        // Reset the hand animation trigger at the start
////        if (handAnimator != null)
////        {
////            handAnimator.ResetTrigger("StartHandAnimation");
////            Debug.Log("Reset hand animation trigger at start.");
////        }

////        // Save initial states of the girl character
////        if (girlCharacter != null)
////        {
////            girlOriginalPosition = girlCharacter.transform.position;
////            girlOriginalRotation = girlCharacter.transform.rotation;
////        }

////        // Save initial animator and audio states
////        foreach (GameObject obj in sceneObjects)
////        {
////            if (obj.TryGetComponent<Animator>(out Animator animator))
////            {
////                originalAnimatorStates[obj] = animator.enabled;
////            }
////        }

////        foreach (AudioSource audioSource in sceneAudioSources)
////        {
////            originalAudioStates[audioSource] = audioSource.isPlaying;
////        }

////        // Ensure spotlight, shadow monster, and shadow next to girl are off at start
////        if (spotlight != null)
////        {
////            spotlight.gameObject.SetActive(false);
////        }

////        if (shadowMonster != null)
////        {
////            shadowMonster.SetActive(false);
////        }

////        if (shadowNextToGirl != null)
////        {
////            shadowNextToGirl.SetActive(false);
////        }

////        if (shadowAnimator != null)
////        {
////            shadowAnimator.enabled = false;
////        }

////        if (eyeAnimator != null)
////        {
////            eyeAnimator.enabled = false;
////        }

////        // Initially disable the closed book
////        if (closedBook != null)
////        {
////            closedBook.SetActive(false);
////        }

////        // Ensure standing girl is disabled at start
////        if (standingGirl != null)
////        {
////            standingGirl.SetActive(false);
////        }

////        // Automatically start glitch after initial delay
////        StartCoroutine(StartGlitchAfterDelay());
////    }

////    private IEnumerator StartGlitchAfterDelay()
////    {
////        Debug.Log($"Waiting for {initialDelay} seconds before triggering glitch...");
////        yield return new WaitForSeconds(initialDelay);
////        TriggerGlitch();
////    }

////    public void TriggerGlitch()
////    {
////        if (isGlitchActive) return; // Prevent multiple triggers
////        isGlitchActive = true;
////        Debug.Log("Glitch triggered!");
////        StartCoroutine(GlitchSequence());
////    }

////    private IEnumerator GlitchSequence()
////    {
////        Debug.Log("Starting glitch sequence...");

////        if (girlCharacter != null)
////        {
////            girlCharacter.SetActive(false);
////        }

////        if (shadowMonster != null)
////        {
////            shadowMonster.SetActive(true);
////        }

////        yield return new WaitForSeconds(glitchDelay);

////        foreach (GameObject obj in sceneObjects)
////        {
////            if (obj.TryGetComponent<Animator>(out Animator animator))
////            {
////                animator.enabled = false;
////            }
////        }

////        foreach (AudioSource audioSource in sceneAudioSources)
////        {
////            audioSource.Stop();
////        }

////        if (spotlight != null)
////        {
////            spotlight.gameObject.SetActive(true);
////            yield return StartCoroutine(FlickerLight());
////        }

////        if (shadowAnimator != null)
////        {
////            shadowAnimator.enabled = true;
////        }

////        if (eyeAnimator != null)
////        {
////            eyeAnimator.enabled = true;
////        }

////        Debug.Log("Glitch sequence completed.");

////        yield return new WaitForSeconds(resetDelay);
////        ResetScene();
////    }

////    private IEnumerator FlickerLight()
////    {
////        float elapsed = 0f;

////        while (elapsed < flickerDuration)
////        {
////            if (spotlight != null)
////            {
////                spotlight.enabled = !spotlight.enabled;
////            }

////            yield return new WaitForSeconds(flickerInterval);
////            elapsed += flickerInterval;
////        }

////        if (spotlight != null)
////        {
////            spotlight.enabled = true;
////        }
////    }

////    private void ResetScene()
////    {
////        Debug.Log("Resetting scene to original state...");

////        if (shadowMonster != null)
////        {
////            shadowMonster.SetActive(false);
////        }

////        if (girlCharacter != null)
////        {
////            girlCharacter.SetActive(true);
////            girlCharacter.transform.position = girlOriginalPosition;
////            girlCharacter.transform.rotation = girlOriginalRotation;
////        }

////        if (shadowNextToGirl != null)
////        {
////            shadowNextToGirl.SetActive(true);
////        }

////        foreach (var entry in originalAnimatorStates)
////        {
////            if (entry.Key.TryGetComponent<Animator>(out Animator animator))
////            {
////                animator.enabled = entry.Value;
////            }
////        }

////        foreach (var entry in originalAudioStates)
////        {
////            if (entry.Key.TryGetComponent<AudioSource>(out AudioSource audioSource))
////            {
////                if (entry.Value)
////                {
////                    audioSource.Play();
////                }
////            }
////        }

////        if (spotlight != null)
////        {
////            spotlight.gameObject.SetActive(false);
////        }

////        StartCoroutine(TriggerHandAndBookAnimation());
////    }

////    private IEnumerator TriggerHandAndBookAnimation()
////    {
////        // Close the book and immediately trigger the hand animation
////        if (openBook != null && closedBook != null)
////        {
////            Debug.Log("Switching book from open to closed...");
////            openBook.SetActive(false);
////            closedBook.SetActive(true); // Show the closed book
////        }

////        // Trigger the hand animation to move the book down
////        if (handAnimator != null)
////        {
////            Debug.Log("Starting hand animation...");
////            handAnimator.SetTrigger("StartHandAnimation"); // Play the hand animation
////        }

////        // Wait for the hand animation to finish (adjust duration as needed)
////        yield return new WaitForSeconds(1f);

////        // Disable the sitting girl and enable the standing girl
////        if (sittingGirl != null && standingGirl != null)
////        {
////            Debug.Log("Switching from sitting to standing girl...");
////            sittingGirl.SetActive(false);
////            standingGirl.SetActive(true);
////        }

////        // Optional: Disable the Animator to lock the hand in the final position
////        if (handAnimator != null)
////        {
////            handAnimator.enabled = false; // Lock the hand in place
////            Debug.Log("Hand animation completed, animator disabled.");
////        }
////    }
////}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GlitchEffect : MonoBehaviour
//{
//    public UnityEngine.Rendering.Universal.Light2D spotlight; // Reference to the 2D spotlight
//    public GameObject shadowMonster; // Reference to the shadow monster
//    public Animator shadowAnimator; // Animator for the shadow monster's animations
//    public Animator eyeAnimator; // Animator for the glowing eyes
//    public GameObject girlCharacter; // Reference to the girl character
//    public GameObject shadowNextToGirl; // Reference to the shadow that appears next to the girl
//    public GameObject openBook; // Reference to the open book object
//    public GameObject closedBook; // Reference to the closed book object
//    public Animator handAnimator; // Animator for the girl's hand

//    public GameObject sittingGirl;  // Assign the sitting girl in the Inspector
//    public GameObject standingGirl; // Assign the standing girl in the Inspector
//    public GameObject closedShelf;  // The shelf before opening
//    public GameObject openShelf;    // The shelf that appears when she stands up

//    public GameObject[] sceneObjects; // All objects to disable during the glitch
//    public AudioSource[] sceneAudioSources; // Audio sources to stop during the glitch

//    public float glitchDelay = 3f; // Time before the glitch happens
//    public float initialDelay = 10f; // Time before the glitch sequence starts
//    public float flickerDuration = 1f; // Duration of the flicker effect
//    public float flickerInterval = 0.3f; // Time between flickers
//    public float resetDelay = 5f; // Time to wait before resetting the scene

//    private bool isGlitchActive = false;
//    private Vector3 girlOriginalPosition;
//    private Quaternion girlOriginalRotation;

//    private Dictionary<GameObject, bool> originalAnimatorStates = new Dictionary<GameObject, bool>();
//    private Dictionary<AudioSource, bool> originalAudioStates = new Dictionary<AudioSource, bool>();

//    void Start()
//    {
//        Debug.Log("GlitchEffect script is active.");

//        // Reset the hand animation trigger at the start
//        if (handAnimator != null)
//        {
//            handAnimator.ResetTrigger("StartHandAnimation");
//            Debug.Log("Reset hand animation trigger at start.");
//        }

//        // Save initial states of the girl character
//        if (girlCharacter != null)
//        {
//            girlOriginalPosition = girlCharacter.transform.position;
//            girlOriginalRotation = girlCharacter.transform.rotation;
//        }

//        // Save initial animator and audio states
//        foreach (GameObject obj in sceneObjects)
//        {
//            if (obj.TryGetComponent<Animator>(out Animator animator))
//            {
//                originalAnimatorStates[obj] = animator.enabled;
//            }
//        }

//        foreach (AudioSource audioSource in sceneAudioSources)
//        {
//            originalAudioStates[audioSource] = audioSource.isPlaying;
//        }

//        // Ensure spotlight, shadow monster, and shadow next to girl are off at start
//        if (spotlight != null)
//        {
//            spotlight.gameObject.SetActive(false);
//        }

//        if (shadowMonster != null)
//        {
//            shadowMonster.SetActive(false);
//        }

//        if (shadowNextToGirl != null)
//        {
//            shadowNextToGirl.SetActive(false);
//        }

//        if (shadowAnimator != null)
//        {
//            shadowAnimator.enabled = false;
//        }

//        if (eyeAnimator != null)
//        {
//            eyeAnimator.enabled = false;
//        }

//        // Initially disable the closed book
//        if (closedBook != null)
//        {
//            closedBook.SetActive(false);
//        }

//        // Ensure standing girl and open shelf are disabled at start
//        if (standingGirl != null)
//        {
//            standingGirl.SetActive(false);
//        }

//        if (openShelf != null)
//        {
//            openShelf.SetActive(false);
//        }

//        // Automatically start glitch after initial delay
//        StartCoroutine(StartGlitchAfterDelay());
//    }

//    private IEnumerator StartGlitchAfterDelay()
//    {
//        Debug.Log($"Waiting for {initialDelay} seconds before triggering glitch...");
//        yield return new WaitForSeconds(initialDelay);
//        TriggerGlitch();
//    }

//    public void TriggerGlitch()
//    {
//        if (isGlitchActive) return; // Prevent multiple triggers
//        isGlitchActive = true;
//        Debug.Log("Glitch triggered!");
//        StartCoroutine(GlitchSequence());
//    }

//    private IEnumerator GlitchSequence()
//    {
//        Debug.Log("Starting glitch sequence...");

//        if (girlCharacter != null)
//        {
//            girlCharacter.SetActive(false);
//        }

//        if (shadowMonster != null)
//        {
//            shadowMonster.SetActive(true);
//        }

//        yield return new WaitForSeconds(glitchDelay);

//        foreach (GameObject obj in sceneObjects)
//        {
//            if (obj.TryGetComponent<Animator>(out Animator animator))
//            {
//                animator.enabled = false;
//            }
//        }

//        foreach (AudioSource audioSource in sceneAudioSources)
//        {
//            audioSource.Stop();
//        }

//        if (spotlight != null)
//        {
//            spotlight.gameObject.SetActive(true);
//            yield return StartCoroutine(FlickerLight());
//        }

//        if (shadowAnimator != null)
//        {
//            shadowAnimator.enabled = true;
//        }

//        if (eyeAnimator != null)
//        {
//            eyeAnimator.enabled = true;
//        }

//        Debug.Log("Glitch sequence completed.");

//        yield return new WaitForSeconds(resetDelay);
//        ResetScene();
//    }

//    private IEnumerator FlickerLight()
//    {
//        float elapsed = 0f;

//        while (elapsed < flickerDuration)
//        {
//            if (spotlight != null)
//            {
//                spotlight.enabled = !spotlight.enabled;
//            }

//            yield return new WaitForSeconds(flickerInterval);
//            elapsed += flickerInterval;
//        }

//        if (spotlight != null)
//        {
//            spotlight.enabled = true;
//        }
//    }

//    private void ResetScene()
//    {
//        Debug.Log("Resetting scene to original state...");

//        if (shadowMonster != null)
//        {
//            shadowMonster.SetActive(false);
//        }

//        if (girlCharacter != null)
//        {
//            girlCharacter.SetActive(true);
//            girlCharacter.transform.position = girlOriginalPosition;
//            girlCharacter.transform.rotation = girlOriginalRotation;
//        }

//        if (shadowNextToGirl != null)
//        {
//            shadowNextToGirl.SetActive(true);
//        }

//        foreach (var entry in originalAnimatorStates)
//        {
//            if (entry.Key.TryGetComponent<Animator>(out Animator animator))
//            {
//                animator.enabled = entry.Value;
//            }
//        }

//        foreach (var entry in originalAudioStates)
//        {
//            if (entry.Key.TryGetComponent<AudioSource>(out AudioSource audioSource))
//            {
//                if (entry.Value)
//                {
//                    audioSource.Play();
//                }
//            }
//        }

//        if (spotlight != null)
//        {
//            spotlight.gameObject.SetActive(false);
//        }

//        StartCoroutine(TriggerHandAndBookAnimation());
//    }

//    private IEnumerator TriggerHandAndBookAnimation()
//    {
//        // Close the book and immediately trigger the hand animation
//        if (openBook != null && closedBook != null)
//        {
//            Debug.Log("Switching book from open to closed...");
//            openBook.SetActive(false);
//            closedBook.SetActive(true); // Show the closed book
//        }

//        // Trigger the hand animation to move the book down
//        if (handAnimator != null)
//        {
//            Debug.Log("Starting hand animation...");
//            handAnimator.SetTrigger("StartHandAnimation"); // Play the hand animation
//        }

//        // Wait for the hand animation to finish (adjust duration as needed)
//        yield return new WaitForSeconds(1f);

//        // Disable the sitting girl and enable the standing girl
//        if (sittingGirl != null && standingGirl != null)
//        {
//            Debug.Log("Switching from sitting to standing girl...");
//            sittingGirl.SetActive(false);
//            standingGirl.SetActive(true);
//        }

//        yield return new WaitForSeconds(0.5f);

//        // Swap the shelf after the standing girl appears
//        SwapShelf();
//    }

//    private void SwapShelf()
//    {
//        Debug.Log("Swapping shelf after girl stands up.");

//        if (closedShelf != null)
//        {
//            closedShelf.SetActive(false); // Hide closed shelf
//        }

//        if (openShelf != null)
//        {
//            openShelf.SetActive(true); // Show open shelf
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D spotlight;
    public GameObject shadowMonster;
    public Animator shadowAnimator;
    public Animator eyeAnimator;
    public GameObject girlCharacter;
    public GameObject shadowNextToGirl;
    public GameObject openBook;
    public GameObject closedBook;
    public Animator handAnimator;

    public GameObject sittingGirl;
    public GameObject standingGirl;
    public GameObject closedShelf;
    public GameObject openShelf;
    public GameObject[] foodItems; // The food objects to appear with open shelf
    public GameObject angryFace; // The girl's angry face

    public GameObject[] sceneObjects;
    public AudioSource[] sceneAudioSources;

    public float glitchDelay = 3f;
    public float initialDelay = 10f;
    public float flickerDuration = 1f;
    public float flickerInterval = 0.3f;
    public float resetDelay = 5f;

    private bool isGlitchActive = false;
    private Vector3 girlOriginalPosition;
    private Quaternion girlOriginalRotation;

    private Dictionary<GameObject, bool> originalAnimatorStates = new Dictionary<GameObject, bool>();
    private Dictionary<AudioSource, bool> originalAudioStates = new Dictionary<AudioSource, bool>();

    void Start()
    {
        Debug.Log("GlitchEffect script is active.");

        if (handAnimator != null)
        {
            handAnimator.ResetTrigger("StartHandAnimation");
        }

        if (girlCharacter != null)
        {
            girlOriginalPosition = girlCharacter.transform.position;
            girlOriginalRotation = girlCharacter.transform.rotation;
        }

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

        if (spotlight != null) spotlight.gameObject.SetActive(false);
        if (shadowMonster != null) shadowMonster.SetActive(false);
        if (shadowNextToGirl != null) shadowNextToGirl.SetActive(false);
        if (shadowAnimator != null) shadowAnimator.enabled = false;
        if (eyeAnimator != null) eyeAnimator.enabled = false;
        if (closedBook != null) closedBook.SetActive(false);
        if (standingGirl != null) standingGirl.SetActive(false);
        if (openShelf != null) openShelf.SetActive(false);
        if (angryFace != null) angryFace.SetActive(false);

        foreach (GameObject food in foodItems)
        {
            if (food != null) food.SetActive(false);
        }

        StartCoroutine(StartGlitchAfterDelay());
    }

    private IEnumerator StartGlitchAfterDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        TriggerGlitch();
    }

    public void TriggerGlitch()
    {
        if (isGlitchActive) return;
        isGlitchActive = true;
        StartCoroutine(GlitchSequence());
    }

    private IEnumerator GlitchSequence()
    {
        if (girlCharacter != null) girlCharacter.SetActive(false);
        if (shadowMonster != null) shadowMonster.SetActive(true);

        yield return new WaitForSeconds(glitchDelay);

        foreach (GameObject obj in sceneObjects)
        {
            if (obj.TryGetComponent<Animator>(out Animator animator)) animator.enabled = false;
        }

        foreach (AudioSource audioSource in sceneAudioSources) audioSource.Stop();

        if (spotlight != null)
        {
            spotlight.gameObject.SetActive(true);
            yield return StartCoroutine(FlickerLight());
        }

        if (shadowAnimator != null) shadowAnimator.enabled = true;
        if (eyeAnimator != null) eyeAnimator.enabled = true;

        yield return new WaitForSeconds(resetDelay);
        ResetScene();
    }

    private IEnumerator FlickerLight()
    {
        float elapsed = 0f;

        while (elapsed < flickerDuration)
        {
            if (spotlight != null) spotlight.enabled = !spotlight.enabled;
            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        if (spotlight != null) spotlight.enabled = true;
    }

    private void ResetScene()
    {
        if (shadowMonster != null) shadowMonster.SetActive(false);
        if (girlCharacter != null)
        {
            girlCharacter.SetActive(true);
            girlCharacter.transform.position = girlOriginalPosition;
            girlCharacter.transform.rotation = girlOriginalRotation;
        }

        if (shadowNextToGirl != null) shadowNextToGirl.SetActive(true);

        foreach (var entry in originalAnimatorStates)
        {
            if (entry.Key.TryGetComponent<Animator>(out Animator animator)) animator.enabled = entry.Value;
        }

        foreach (var entry in originalAudioStates)
        {
            if (entry.Key.TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                if (entry.Value) audioSource.Play();
            }
        }

        if (spotlight != null) spotlight.gameObject.SetActive(false);
        StartCoroutine(TriggerHandAndBookAnimation());
    }

    private IEnumerator TriggerHandAndBookAnimation()
    {
        if (openBook != null && closedBook != null)
        {
            openBook.SetActive(false);
            closedBook.SetActive(true);
        }

        if (handAnimator != null)
        {
            handAnimator.SetTrigger("StartHandAnimation");
        }

        yield return new WaitForSeconds(1f);

        if (sittingGirl != null && standingGirl != null)
        {
            sittingGirl.SetActive(false);
            standingGirl.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        SwapShelf();
    }

    private void SwapShelf()
    {
        if (closedShelf != null) closedShelf.SetActive(false);
        if (openShelf != null) openShelf.SetActive(true);

        // Enable food immediately
        foreach (GameObject food in foodItems)
        {
            if (food != null) food.SetActive(true);
        }

        // Enable the angry face **after 3 seconds**
        StartCoroutine(EnableAngryFace());
    }

    private IEnumerator EnableAngryFace()
    {
        yield return new WaitForSeconds(3f);
        if (angryFace != null) angryFace.SetActive(true);
    }
}
