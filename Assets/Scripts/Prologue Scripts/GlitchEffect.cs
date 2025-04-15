using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using System.Collections;
using System.Collections.Generic;

public class SceneSequenceController : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject shadowArmsReaching;
    public AudioSource pageTurningSound; // AudioSource for page-turning sound
    public AudioSource phoneRingSound; // AudioSource for phone ringing sound
    public TextMeshProUGUI textMessage1; // TextMeshProUGUI for displaying the first message
    public TextMeshProUGUI textMessage2; // TextMeshProUGUI for displaying the second message
    public GameObject textBubble; // The text bubble GameObject (PNG)
    [SerializeField] private GameObject Circle; //cirle for dialogue next button
    [SerializeField] private GameObject X;//x for dialogue next button
    public GameObject goToGroceryStoreSign;  // The sign to go to the grocery store
    public GameObject spotlightOnGroceryStore;  // Spotlight to highlight the sign
    public GameObject shadowSpotlight;  // The spotlight for the shadow
    public GameObject roomLight;
    public GameObject happinessBar;  // Correct, class name is HappinessBar
    public Animator girlAnimator; // Animator for the girl

    // PNG frame-by-frame animation
    [SerializeField] private List<GameObject> girlBookFrames; // Assign these in order: Frame1, Frame2, ..., FrameN
    [SerializeField] private float frameInterval = 1f; // Time between frame switches

    // Thought bubble variables
    public GameObject thoughtBubble; // Thought bubble GameObject (PNG)
    public GameObject chipsAsset;    // The chips floating asset (image or 3D object)
    public GameObject thoughtBubbleText; // Text inside the thought bubble
    public GameObject chipsText; // Text below the chips (thought continuation)

    // Shadow face and animator references
    //public GameObject oldShadowFace;  // The old face of the shadow
    //public GameObject newShadowFace;  // The new face of the shadow
    //public Animator shadowAnimator; // Shadow's main animator (for general animations)
    //public Animator shadowArmAnimator; // Animator for the shadow's arm movement

    // Animator references for head and shadow
    public Animator headAnimator;  // Head animator

    // Circle objects for the thought bubble animation
    public GameObject circle1; // First circle to appear
    public GameObject circle2; // Second circle to appear

    // Sparkling lines effect (already existing as a GameObject in the scene)
    public GameObject sparklingLines; // The sparkling lines GameObject

    void Start()
    {
        if (girlAnimator != null)
        {
            girlAnimator.SetBool("IsReading", true);
        }

        // Initial states
        textMessage1.gameObject.SetActive(false);
        textMessage2.gameObject.SetActive(false);
        textBubble.SetActive(false);
        thoughtBubble.SetActive(false);
        chipsAsset.SetActive(false);
     

        if (pageTurningSound != null)
        {
            pageTurningSound.loop = true;
            pageTurningSound.Play();
        }

        foreach (var frame in girlBookFrames) frame.SetActive(false);
        if (girlBookFrames.Count > 0) girlBookFrames[0].SetActive(true);

        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(5f);

        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubble();
        yield return new WaitForSeconds(1f);
        ShowThoughtText(thoughtBubbleText);
        yield return new WaitForSeconds(6f);
        yield return new WaitForSeconds(1f);

        HideThoughtBubbleAndText(thoughtBubbleText);

        if (phoneRingSound != null)
        {
            phoneRingSound.Play();
        }

        StopAnimators();

        yield return new WaitForSeconds(1f);

        StartCoroutine(PlayGirlPuttingBookDown());
        yield return new WaitForSeconds(frameInterval * (girlBookFrames.Count - 1) + 0.5f);

   

        yield return new WaitForSeconds(4f);
        Circle.gameObject.SetActive(true);
        X.gameObject.SetActive(true);
        textBubble.SetActive(true);
        yield return new WaitUntil(() => !textBubble.activeSelf);

        yield return new WaitForSeconds(4f);
        happinessBar.SetActive(true);
        yield return new WaitForSeconds(4f);

        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubbleWithChips();
        yield return new WaitForSeconds(10f);
        TriggerArmMovement();

        if (circle1 != null) circle1.SetActive(false);
        if (circle2 != null) circle2.SetActive(false);

        HideThoughtBubbleAndText(chipsText);
        if (chipsAsset != null) chipsAsset.SetActive(false);
        if (chipsText != null) chipsText.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
        if (roomLight != null) roomLight.SetActive(false);

        if (goToGroceryStoreSign != null) goToGroceryStoreSign.SetActive(true);
        if (spotlightOnGroceryStore != null) spotlightOnGroceryStore.SetActive(true);
    }

    IEnumerator PlayGirlPuttingBookDown()
    {
        for (int i = 1; i < girlBookFrames.Count; i++)
        {
            girlBookFrames[i - 1].SetActive(false);
            girlBookFrames[i].SetActive(true);

            if (i == 1 && pageTurningSound != null)
            {
                pageTurningSound.Stop();
            }

            yield return new WaitForSeconds(frameInterval);
        }
    }

    void ShowSparklingLines() { if (sparklingLines != null) sparklingLines.SetActive(true); }
    void HideSparklingLines() { if (sparklingLines != null) sparklingLines.SetActive(false); }
    void ShowFirstCircle() { if (circle1 != null) circle1.SetActive(true); }
    void ShowSecondCircle() { if (circle2 != null) circle2.SetActive(true); }
    void ShowThoughtBubble() { if (thoughtBubble != null) thoughtBubble.SetActive(true); }
    void ShowThoughtText(GameObject textHolder) { if (textHolder != null) textHolder.SetActive(true); }
    void HideThoughtBubbleAndText(GameObject textHolder)
    {
        if (circle1 != null) circle1.SetActive(false);
        if (circle2 != null) circle2.SetActive(false);
        if (thoughtBubble != null) thoughtBubble.SetActive(false);
        if (textHolder != null) textHolder.SetActive(false);
    }
    void ShowTextMessage1(string message)
    {
        if (textMessage1 != null && textBubble != null)
        {
            textMessage1.text = message;
            textMessage1.gameObject.SetActive(true);
            textBubble.SetActive(true);
        }
    }
    void ShowTextMessage2(string message)
    {
        if (textMessage1 != null) textMessage1.gameObject.SetActive(false);
        if (textMessage2 != null)
        {
            textMessage2.text = message;
            textMessage2.gameObject.SetActive(true);
        }
    }
    void HideTextAndBubble()
    {
        if (textMessage1 != null) textMessage1.gameObject.SetActive(false);
        if (textMessage2 != null) textMessage2.gameObject.SetActive(false);
        if (textBubble != null) textBubble.SetActive(false);
    }
    void ShowThoughtBubbleWithChips()
    {
        thoughtBubble.SetActive(true);
        StartCoroutine(ShowChipsAfterDelay());
    }
    IEnumerator ShowChipsAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (chipsAsset != null) chipsAsset.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (chipsText != null) chipsText.gameObject.SetActive(true);
        ShowSparklingLines();
        //ChangeShadowFace();
        TriggerArmMovement();
    }
   
    void TriggerArmMovement()
    {
        if (shadowArmsReaching = null) shadowArmsReaching.SetActive(true);
        StartCoroutine(StopSparklesAfterAnimation());
    }


    IEnumerator StopSparklesAfterAnimation()
    {
        yield return new WaitForSeconds(4f);
        HideSparklingLines();
    }
    void StopAnimators()
    {
        if (headAnimator != null)
        {
            headAnimator.speed = 0f;
            headAnimator.enabled = false;
        }
        
        if (girlAnimator != null)
        {
            girlAnimator.SetBool("IsReading", false);
        }
    }
    void ResumeAnimators()
    {
        if (headAnimator != null)
        {
            headAnimator.enabled = true;
            headAnimator.speed = 1f;
        }
    }
}
