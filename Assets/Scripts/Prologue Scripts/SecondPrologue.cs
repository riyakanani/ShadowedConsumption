using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SecondPrologue : MonoBehaviour
{
    [SerializeField] private List<GameObject> girlTypingFrames;
    [SerializeField] private float typingFrameInterval = 0.5f;

    public AudioSource backgroundMusic;
    public AudioSource typingSound;
    public AudioSource phoneRingSound;

    public TextMeshProUGUI textMessage1;
    public TextMeshProUGUI textMessage2;
    public GameObject textBubble;

    public GameObject thoughtBubble;
    public GameObject thoughtBubbleText;
    public GameObject thoughtAsset;

    public GameObject goToShoppingCenterSign;
    public GameObject spotlight;
    public GameObject roomLight;
    public GameObject shadowSpotlight;

    public GameObject shadowArmsReaching;
    public GameObject shadow;

    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject X;
    public GameObject circle1;
    public GameObject circle2;
    public GameObject sparklingLines;

    public GameObject happinessBar;

    void Start()
    {


        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            backgroundMusic.volume = 0.2f;
            backgroundMusic.Play();
        }

        if (typingSound != null)
        {
            typingSound.loop = true;
            typingSound.Play();
        }

        if (phoneRingSound != null) phoneRingSound.Stop();

        textMessage1?.gameObject.SetActive(false);
        textMessage2?.gameObject.SetActive(false);
        textBubble?.SetActive(false);
        thoughtBubble?.SetActive(false);
        thoughtBubbleText?.SetActive(false);
        thoughtAsset?.SetActive(false);
        sparklingLines?.SetActive(false);
        goToShoppingCenterSign?.SetActive(false);
        spotlight?.SetActive(false);
        shadowArmsReaching?.SetActive(false);
        shadow?.SetActive(true);
        Circle?.SetActive(false);
        X?.SetActive(false);
        circle1?.SetActive(false);
        circle2?.SetActive(false);
        happinessBar?.SetActive(false);

        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(PlayTypingAnimation());

        if (typingSound != null) typingSound.Stop();
        if (phoneRingSound != null) phoneRingSound.Play();

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(PlayCloseLaptopAndPickPhoneAnimation());


        yield return new WaitForSeconds(1f);
        Circle?.SetActive(true);
        X?.SetActive(true);
        textBubble?.SetActive(true);

        yield return new WaitUntil(() => !textBubble.activeSelf);

        yield return new WaitForSeconds(2f);

        if (happinessBar != null) happinessBar.SetActive(true);

        yield return new WaitForSeconds(2f);

        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubbleWithAsset();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtText();
        yield return new WaitForSeconds(10f);

        // Corrected order: hide thought bubble and asset first
        HideThoughtBubbleAndText();
        thoughtAsset?.SetActive(false);

        // THEN disable circles
        circle1?.SetActive(false);
        circle2?.SetActive(false);

        yield return new WaitForSeconds(3f);

        if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
        if (roomLight != null) roomLight.SetActive(false);
        if (spotlight != null) spotlight.SetActive(true);
        if (goToShoppingCenterSign != null) goToShoppingCenterSign.SetActive(true);
    }

    IEnumerator PlayTypingAnimation()
    {
        foreach (var frame in girlTypingFrames)
            frame.SetActive(false);

        if (girlTypingFrames.Count > 0)
            girlTypingFrames[0].SetActive(true);

        yield return new WaitForSeconds(typingFrameInterval);
    }

    IEnumerator PlayCloseLaptopAndPickPhoneAnimation()
    {
        if (girlTypingFrames.Count >= 3)
        {
            girlTypingFrames[0].SetActive(false);
            girlTypingFrames[1].SetActive(true);
            yield return new WaitForSeconds(typingFrameInterval);

            girlTypingFrames[1].SetActive(false);
            girlTypingFrames[2].SetActive(true);
            yield return new WaitForSeconds(typingFrameInterval);
        }
    }

    void ShowFirstCircle() { if (circle1 != null) circle1.SetActive(true); }
    void ShowSecondCircle() { if (circle2 != null) circle2.SetActive(true); }
    void ShowThoughtBubble() { if (thoughtBubble != null) thoughtBubble.SetActive(true); }
    void ShowThoughtText() { if (thoughtBubbleText != null) thoughtBubbleText.SetActive(true); }
    void HideThoughtBubbleAndText()
    {
        if (circle1 != null) circle1.SetActive(false);
        if (circle2 != null) circle2.SetActive(false);
        if (thoughtBubble != null) thoughtBubble.SetActive(false);
        if (thoughtBubbleText != null) thoughtBubbleText.SetActive(false);
    }

    void ShowThoughtBubbleWithAsset()
    {
        if (thoughtBubble != null) thoughtBubble.SetActive(true);
        StartCoroutine(ShowAssetAfterDelay());
    }
    IEnumerator ShowAssetAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        if (thoughtAsset != null) thoughtAsset.SetActive(true);
        yield return new WaitForSeconds(1f);
        ShowSparklingLines();
        TriggerArmMovement();
    }

    void ShowSparklingLines()
    {
        if (sparklingLines != null) sparklingLines.SetActive(true);
    }

    void TriggerArmMovement()
    {
        if (shadowArmsReaching != null)
            shadowArmsReaching.SetActive(true);

        if (shadow != null)
            shadow.SetActive(false);
        StartCoroutine(StopSparklesAfterAnimation());
    }

    IEnumerator StopSparklesAfterAnimation()
    {
        yield return new WaitForSeconds(4f);
        if (sparklingLines != null) sparklingLines.SetActive(false);
    }
}
