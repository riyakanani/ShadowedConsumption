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
    public GameObject thoughtBubbleText;
    public GameObject secondThoughtText;
    public GameObject chipsAsset;
    public GameObject chipsText;

    public GameObject circle1;
    public GameObject circle2;

    public GameObject shadowCircle1;
    public GameObject shadowCircle2;
    public GameObject shadowThoughtBubble;
    public GameObject shadowThoughtText1;
    public GameObject shadowThoughtText2;

    public GameObject sparklingLines;
    public List<GameObject> clutterObjects;

    void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;
            backgroundMusic.volume = 0.2f;
            backgroundMusic.Play();
        }

        textMessage1.gameObject.SetActive(false);
        textMessage2.gameObject.SetActive(false);
        textBubble.SetActive(false);
        thoughtBubble.SetActive(false);
        chipsAsset.SetActive(false);

        if (thoughtBubbleText != null) thoughtBubbleText.SetActive(false);
        if (secondThoughtText != null) secondThoughtText.SetActive(false);
        if (shadowThoughtBubble != null) shadowThoughtBubble.SetActive(false);
        if (shadowThoughtText1 != null) shadowThoughtText1.SetActive(false);
        if (shadowThoughtText2 != null) shadowThoughtText2.SetActive(false);

        foreach (var obj in clutterObjects) obj.SetActive(false);

        if (pageTurningSound != null)
        {
            pageTurningSound.loop = true;
            pageTurningSound.Play();
        }

        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(PlayReadingAnimation());

        // Girl’s first thought
        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubble();
        yield return new WaitForSeconds(1f);
        ShowThoughtText(thoughtBubbleText, "This book is so good");
        yield return new WaitForSeconds(3f);
        HideThoughtBubbleAndText();

        // Phone rings
        if (pageTurningSound != null) pageTurningSound.Stop();
        if (phoneRingSound != null) phoneRingSound.Play();
        yield return new WaitForSeconds(1f);

        // Girl puts book down
        StartCoroutine(PlayGirlPuttingBookDown());
        yield return new WaitForSeconds(frameInterval * (girlBookFrames.Count - 1) + 0.5f);

        // Text messages
        yield return new WaitForSeconds(1f);
        Circle.gameObject.SetActive(true);
        X.gameObject.SetActive(true);
        textBubble.SetActive(true);
        yield return new WaitUntil(() => !textBubble.activeSelf);

        // Happiness bar
        yield return new WaitForSeconds(2f);
        happinessBar.SetActive(true);
        yield return new WaitForSeconds(2f);

        // Girl’s second thought
        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubble();
        yield return new WaitForSeconds(1f);
        ShowThoughtText(secondThoughtText, "I don't really need any more snacks... I already have so many.");
        yield return new WaitForSeconds(3f);
        HideThoughtBubbleAndText();

        // Shadow’s thought
        ShowShadowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowShadowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ShowShadowThoughtSequence());

        // Chips scene
        ShowFirstCircle();
        yield return new WaitForSeconds(0.5f);
        ShowSecondCircle();
        yield return new WaitForSeconds(0.5f);
        ShowThoughtBubbleWithChips();
        yield return new WaitForSeconds(10f);

        if (circle1 != null) circle1.SetActive(false);
        if (circle2 != null) circle2.SetActive(false);

        HideThoughtBubbleAndText();
        if (chipsAsset != null) chipsAsset.SetActive(false);
        if (chipsText != null) chipsText.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);
        StartCoroutine(ShowClutterGradually());

        if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
        if (roomLight != null) roomLight.SetActive(false);
        if (goToGroceryStoreSign != null) goToGroceryStoreSign.SetActive(true);
        if (spotlightOnGroceryStore != null) spotlightOnGroceryStore.SetActive(true);
    }

    IEnumerator PlayGirlPuttingBookDown()
    {
        foreach (var frame in girlBookReadingFrames)
            frame.SetActive(false);

        for (int i = 1; i < girlBookFrames.Count; i++)
        {
            girlBookFrames[i - 1].SetActive(false);
            girlBookFrames[i].SetActive(true);
            yield return new WaitForSeconds(frameInterval);
        }
    }

    IEnumerator PlayReadingAnimation()
    {
        foreach (var frame in girlBookReadingFrames)
            frame.SetActive(false);

        if (girlBookReadingFrames.Count > 0)
            girlBookReadingFrames[0].SetActive(true);

        yield return new WaitForSeconds(readingFrameInterval);

        for (int i = 1; i < girlBookReadingFrames.Count; i++)
        {
            girlBookReadingFrames[i - 1].SetActive(false);
            girlBookReadingFrames[i].SetActive(true);
            yield return new WaitForSeconds(readingFrameInterval);
        }
    }

    void ShowFirstCircle() { if (circle1 != null) circle1.SetActive(true); }
    void ShowSecondCircle() { if (circle2 != null) circle2.SetActive(true); }
    void ShowThoughtBubble() { if (thoughtBubble != null) thoughtBubble.SetActive(true); }

    void ShowThoughtText(GameObject textObj, string message)
    {
        if (textObj != null && textObj.GetComponent<TextMeshProUGUI>() != null)
        {
            var tmp = textObj.GetComponent<TextMeshProUGUI>();
            tmp.text = message;
            textObj.SetActive(true);
        }
    }

    void HideThoughtBubbleAndText()
    {
        // Girl's
        if (circle1 != null) circle1.SetActive(false);
        if (circle2 != null) circle2.SetActive(false);
        if (thoughtBubbleText != null) thoughtBubbleText.SetActive(false);
        if (secondThoughtText != null) secondThoughtText.SetActive(false);
        if (thoughtBubble != null) thoughtBubble.SetActive(false);

        // Shadow's
        if (shadowThoughtText1 != null) shadowThoughtText1.SetActive(false);
        if (shadowThoughtText2 != null) shadowThoughtText2.SetActive(false);
        if (shadowThoughtBubble != null) shadowThoughtBubble.SetActive(false);
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
        StartCoroutine(DelayedArmReach());
        StartCoroutine(GrowShadowOverTime());
    }

    IEnumerator DelayedArmReach()
    {
        yield return new WaitForSeconds(0.3f);
        if (shadowArmsReaching != null) shadowArmsReaching.SetActive(true);
        if (shadow != null) shadow.SetActive(false);
        StartCoroutine(StopSparklesAfterAnimation());
    }

    IEnumerator GrowShadowOverTime()
    {
        if (shadow != null)
        {
            Vector3 originalScale = shadow.transform.localScale;
            Vector3 targetScale = originalScale * 1.5f;
            float duration = 2f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                shadow.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            shadow.transform.localScale = targetScale;
        }
    }

    IEnumerator StopSparklesAfterAnimation()
    {
        yield return new WaitForSeconds(4f);
        HideSparklingLines();
    }

    IEnumerator ShowClutterGradually()
    {
        foreach (var obj in clutterObjects)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator ShowShadowThoughtSequence()
    {
        if (shadowThoughtBubble != null) shadowThoughtBubble.SetActive(true);

        if (shadowThoughtText1 != null) shadowThoughtText1.SetActive(true);
        yield return new WaitForSeconds(2f);

        if (shadowThoughtText1 != null) shadowThoughtText1.SetActive(false);
        if (shadowThoughtText2 != null) shadowThoughtText2.SetActive(true);
        yield return new WaitForSeconds(2f);

        if (shadowThoughtText2 != null) shadowThoughtText2.SetActive(false);
        if (shadowThoughtBubble != null) shadowThoughtBubble.SetActive(false);

        // ✅ Disable shadow circles when the thought ends
        if (shadowCircle1 != null) shadowCircle1.SetActive(false);
        if (shadowCircle2 != null) shadowCircle2.SetActive(false);
    }

    void ShowShadowFirstCircle() { if (shadowCircle1 != null) shadowCircle1.SetActive(true); }
    void ShowShadowSecondCircle() { if (shadowCircle2 != null) shadowCircle2.SetActive(true); }

    void ShowSparklingLines()
    {
        if (sparklingLines != null)
            sparklingLines.SetActive(true);
    }

    void HideSparklingLines()
    {
        if (sparklingLines != null)
            sparklingLines.SetActive(false);
    }
}

