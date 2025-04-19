using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SceneSequenceController : MonoBehaviour
{
    [SerializeField] private List<GameObject> girlBookReadingFrames;
    [SerializeField] private float readingFrameInterval = 0.5f;
    public AudioSource backgroundMusic;
    public GameObject shadowArmsReaching;
    public GameObject shadow;
    public AudioSource pageTurningSound;
    public AudioSource phoneRingSound;
    public TextMeshProUGUI textMessage1;
    public TextMeshProUGUI textMessage2;
    public GameObject textBubble;
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject X;
    public GameObject goToGroceryStoreSign;
    public GameObject spotlightOnGroceryStore;
    public GameObject shadowSpotlight;
    public GameObject roomLight;
    public GameObject happinessBar;

    [SerializeField] private List<GameObject> girlBookFrames;
    [SerializeField] private float frameInterval = 1f;

    public GameObject thoughtBubble;
    public GameObject chipsAsset;
    public GameObject thoughtBubbleText;
    public GameObject chipsText;

    public GameObject circle1;
    public GameObject circle2;

    public GameObject sparklingLines;

    //void Awake()
    //{
    //    foreach (var frame in girlBookReadingFrames)
    //        frame.SetActive(false);

    //    if (girlBookReadingFrames.Count > 0)
    //        girlBookReadingFrames[0].SetActive(true);

    //    foreach (var frame in girlBookFrames)
    //        frame.SetActive(false);
    //}

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            backgroundMusic.volume = 0.2f; // Adjust this as needed
            backgroundMusic.Play();
        }

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

        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(.5f);

        yield return StartCoroutine(PlayReadingAnimation());

        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubble();
        yield return new WaitForSeconds(1f);
        ShowThoughtText(thoughtBubbleText);
        yield return new WaitForSeconds(3f);
        HideThoughtBubbleAndText(thoughtBubbleText);

        if (pageTurningSound != null) pageTurningSound.Stop();
        if (phoneRingSound != null) phoneRingSound.Play();

        yield return new WaitForSeconds(1f);

        StartCoroutine(PlayGirlPuttingBookDown());
        yield return new WaitForSeconds(frameInterval * (girlBookFrames.Count - 1) + 0.5f);

        yield return new WaitForSeconds(1f);
        Circle.gameObject.SetActive(true);
        X.gameObject.SetActive(true);
        textBubble.SetActive(true);
        yield return new WaitUntil(() => !textBubble.activeSelf);

        yield return new WaitForSeconds(2f);
        happinessBar.SetActive(true);
        yield return new WaitForSeconds(2f);

        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubbleWithChips();
        yield return new WaitForSeconds(10f);
        //TriggerArmMovement();

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
        foreach (var frame in girlBookReadingFrames)
        {
            frame.SetActive(false);
        }


        for (int i = 1; i < girlBookFrames.Count; i++)
        {
            girlBookFrames[i - 1].SetActive(false);
            girlBookFrames[i].SetActive(true);
            yield return new WaitForSeconds(frameInterval);
        }
    }

    IEnumerator PlayReadingAnimation()
    {
        // Disable all reading frames just in case
        foreach (var frame in girlBookReadingFrames)
        {
            frame.SetActive(false);
        }

        if (girlBookReadingFrames.Count > 0)
        {
            girlBookReadingFrames[0].SetActive(true); // Start from the first frame
        }

        yield return new WaitForSeconds(readingFrameInterval);

        for (int i = 1; i < girlBookReadingFrames.Count; i++)
        {
            girlBookReadingFrames[i - 1].SetActive(false);
            girlBookReadingFrames[i].SetActive(true);
            yield return new WaitForSeconds(readingFrameInterval);
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
        yield return new WaitForSeconds(1f);
        if (chipsText != null) chipsText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (chipsAsset != null) chipsAsset.SetActive(true);
        yield return new WaitForSeconds(1f);
        ShowSparklingLines();
        TriggerArmMovement();
    }

    void TriggerArmMovement()
    {
        if (shadowArmsReaching != null)
        {
            shadowArmsReaching.SetActive(true);
        }

        if (shadow != null) shadow.SetActive(false);
        StartCoroutine(StopSparklesAfterAnimation());
    }

    IEnumerator StopSparklesAfterAnimation()
    {
        yield return new WaitForSeconds(4f);
        HideSparklingLines();
    }
}
