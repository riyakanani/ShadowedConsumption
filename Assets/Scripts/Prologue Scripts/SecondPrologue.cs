//////using UnityEngine;
//////using TMPro;
//////using System.Collections;
//////using System.Collections.Generic;

//////public class SecondPrologue : MonoBehaviour
//////{
//////    [SerializeField] private List<GameObject> girlTypingFrames;
//////    [SerializeField] private float typingFrameInterval = 0.5f;

//////    public AudioSource backgroundMusic;
//////    public AudioSource typingSound;
//////    public AudioSource phoneRingSound;

//////    public TextMeshProUGUI textMessage1;
//////    public TextMeshProUGUI textMessage2;
//////    public GameObject textBubble;

//////    public GameObject thoughtBubble;
//////    public GameObject thoughtBubbleText;
//////    public GameObject thoughtAsset;

//////    public GameObject goToShoppingCenterSign;
//////    public GameObject spotlight;
//////    public GameObject roomLight;
//////    public GameObject shadowSpotlight;

//////    public GameObject shadowArmsReaching;
//////    public GameObject shadow;
//////    public float shadowGrowDuration = 0.5f;

//////    [SerializeField] private GameObject Circle;
//////    [SerializeField] private GameObject X;
//////    public GameObject circle1;
//////    public GameObject circle2;
//////    public GameObject sparklingLines;

//////    public GameObject happinessBar;

//////    public GameObject shadowThoughtBubble;
//////    public TextMeshProUGUI shadowThoughtText;
//////    public GameObject shadowCircle1;
//////    public GameObject shadowCircle2;
//////    public GameObject shadowCircle3;
//////    public GameObject morphedShadow;
//////    public GameObject thoughtBubbleTextTwo;

//////    void Start()
//////    {


//////        if (backgroundMusic != null)
//////        {
//////            backgroundMusic.loop = true;
//////            backgroundMusic.volume = 0.2f;
//////            backgroundMusic.Play();
//////        }

//////        if (typingSound != null)
//////        {
//////            typingSound.loop = true;
//////            typingSound.Play();
//////        }

//////        if (phoneRingSound != null) phoneRingSound.Stop();

//////        textMessage1?.gameObject.SetActive(false);
//////        textMessage2?.gameObject.SetActive(false);
//////        textBubble?.SetActive(false);
//////        thoughtBubble?.SetActive(false);
//////        thoughtBubbleText?.SetActive(false);
//////        thoughtAsset?.SetActive(false);
//////        sparklingLines?.SetActive(false);
//////        goToShoppingCenterSign?.SetActive(false);
//////        spotlight?.SetActive(false);
//////        shadowArmsReaching?.SetActive(false);
//////        shadow?.SetActive(true);
//////        Circle?.SetActive(false);
//////        X?.SetActive(false);
//////        circle1?.SetActive(false);
//////        circle2?.SetActive(false);
//////        happinessBar?.SetActive(false);

//////        StartCoroutine(RunSequence());
//////    }

//////    IEnumerator RunSequence()
//////    {
//////        yield return new WaitForSeconds(0.5f);

//////        yield return StartCoroutine(PlayTypingAnimation());

//////        if (typingSound != null) typingSound.Stop();
//////        if (phoneRingSound != null) phoneRingSound.Play();

//////        yield return new WaitForSeconds(1f);

//////        yield return StartCoroutine(PlayCloseLaptopAndPickPhoneAnimation());


//////        yield return new WaitForSeconds(1f);
//////        Circle?.SetActive(true);
//////        X?.SetActive(true);
//////        textBubble?.SetActive(true);

//////        yield return new WaitUntil(() => !textBubble.activeSelf);

//////        yield return new WaitForSeconds(2f);

//////        if (happinessBar != null) happinessBar.SetActive(true);

//////        yield return new WaitForSeconds(2f);

//////        ShowFirstCircle();
//////        yield return new WaitForSeconds(0.5f);
//////        ShowSecondCircle();
//////        yield return new WaitForSeconds(0.5f);
//////        ShowThoughtBubble();
//////        yield return new WaitForSeconds(0.5f);
//////        ShowThoughtText();
//////        yield return new WaitForSeconds(3f);

//////        // Corrected order: hide thought bubble and asset first
//////        HideThoughtBubbleAndText();
//////        thoughtAsset?.SetActive(false);

//////        // THEN disable circles
//////        circle1?.SetActive(false);
//////        circle2?.SetActive(false);

//////      //  yield return new WaitForSeconds(1f);



//////        yield return new WaitForSeconds(2f);

//////        Coroutine shadowBubbleSequence = StartCoroutine(PlayShadowThoughtSequence());

//////        HideThoughtBubbleAndText();

//////        yield return shadowBubbleSequence;

//////        yield return new WaitForSeconds(2f);

//////        HideShadowThoughtBubbleAndCircles();

//////        yield return new WaitForSeconds(2f);

//////        ShowFirstCircle();
//////        yield return new WaitForSeconds(0.5f);
//////        ShowSecondCircle();
//////        yield return new WaitForSeconds(0.5f);
//////        ShowThoughtBubbleWithAsset();
//////        yield return new WaitForSeconds(0.5f);
//////        ShowThoughtTextTwo();
//////        yield return new WaitForSeconds(10f);

//////        if (shadow != null)
//////            yield return StartCoroutine(GrowShadow());

//////        // Corrected order: hide thought bubble and asset first
//////        HideThoughtBubbleAndText();
//////        thoughtAsset?.SetActive(false);

//////        // THEN disable circles
//////        circle1?.SetActive(false);
//////        circle2?.SetActive(false);

//////        yield return new WaitForSeconds(3f);

//////        if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
//////        if (roomLight != null) roomLight.SetActive(false);
//////        if (spotlight != null) spotlight.SetActive(true);
//////        if (goToShoppingCenterSign != null) goToShoppingCenterSign.SetActive(true);
//////    }

//////    IEnumerator PlayTypingAnimation()
//////    {
//////        foreach (var frame in girlTypingFrames)
//////            frame.SetActive(false);

//////        if (girlTypingFrames.Count > 0)
//////            girlTypingFrames[0].SetActive(true);

//////        yield return new WaitForSeconds(typingFrameInterval);
//////    }

//////    IEnumerator PlayCloseLaptopAndPickPhoneAnimation()
//////    {
//////        if (girlTypingFrames.Count >= 3)
//////        {
//////            girlTypingFrames[0].SetActive(false);
//////            girlTypingFrames[1].SetActive(true);
//////            yield return new WaitForSeconds(typingFrameInterval);

//////            girlTypingFrames[1].SetActive(false);
//////            girlTypingFrames[2].SetActive(true);
//////            yield return new WaitForSeconds(typingFrameInterval);
//////        }
//////    }

//////    void ShowFirstCircle() { if (circle1 != null) circle1.SetActive(true); }
//////    void ShowSecondCircle() { if (circle2 != null) circle2.SetActive(true); }
//////    void ShowThoughtBubble() { if (thoughtBubble != null) thoughtBubble.SetActive(true); }
//////    void ShowThoughtText() { if (thoughtBubbleText != null) thoughtBubbleText.SetActive(true); }
//////    void ShowThoughtTextTwo() { if (thoughtBubbleTextTwo != null) thoughtBubbleTextTwo.SetActive(true); }

//////    void HideThoughtBubbleAndText()
//////    {
//////        if (circle1 != null) circle1.SetActive(false);
//////        if (circle2 != null) circle2.SetActive(false);
//////        if (thoughtBubble != null) thoughtBubble.SetActive(false);
//////        if (thoughtBubbleText != null) thoughtBubbleText.SetActive(false);
//////        if (thoughtBubbleTextTwo != null) thoughtBubbleTextTwo.SetActive(false);
//////    }

//////    void ShowThoughtBubbleWithAsset()
//////    {
//////        if (thoughtBubble != null) thoughtBubble.SetActive(true);
//////        StartCoroutine(ShowAssetAfterDelay());
//////    }
//////    IEnumerator ShowAssetAfterDelay()
//////    {
//////        yield return new WaitForSeconds(1f);
//////        if (thoughtAsset != null) thoughtAsset.SetActive(true);
//////        yield return new WaitForSeconds(1f);
//////        ShowSparklingLines();
//////        TriggerArmMovement();
//////    }

//////    void ShowSparklingLines()
//////    {
//////        if (sparklingLines != null) sparklingLines.SetActive(true);
//////    }

//////    void TriggerArmMovement()
//////    {
//////        if (shadowArmsReaching != null)
//////            shadowArmsReaching.SetActive(true);

//////        if (shadow != null)
//////            shadow.SetActive(false);
//////        StartCoroutine(StopSparklesAfterAnimation());
//////    }

//////    IEnumerator StopSparklesAfterAnimation()
//////    {
//////        yield return new WaitForSeconds(4f);
//////        if (sparklingLines != null) sparklingLines.SetActive(false);
//////    }


//////    IEnumerator GrowShadow()
//////    {
//////        if (shadow == null || morphedShadow == null) yield break;

//////        // Step 1: Disable original, enable morphed in same position & original scale
//////        Vector3 originalScale = shadow.transform.localScale;
//////        morphedShadow.transform.position = shadow.transform.position;
//////        morphedShadow.transform.localScale = originalScale;

//////        shadow.SetActive(false);
//////        morphedShadow.SetActive(true);

//////        // Step 2: Wait before growth begins
//////        yield return new WaitForSeconds(0.5f); // Adjust delay as needed

//////        // Step 3: Animate growth (with anticipation stretch and settle)
//////        Vector3 targetScale = originalScale * 1.2f;
//////        Vector3 overshootScale = targetScale * 1.05f;

//////        float duration = shadowGrowDuration;
//////        float elapsed = 0f;

//////        // Phase 1: Grow to overshoot
//////        while (elapsed < duration * 0.5f)
//////        {
//////            elapsed += Time.deltaTime;
//////            morphedShadow.transform.localScale = Vector3.Lerp(originalScale, overshootScale, elapsed / (duration * 0.5f));
//////            yield return null;
//////        }

//////        // Phase 2: Settle back to final size
//////        elapsed = 0f;
//////        while (elapsed < duration * 0.5f)
//////        {
//////            elapsed += Time.deltaTime;
//////            morphedShadow.transform.localScale = Vector3.Lerp(overshootScale, targetScale, elapsed / (duration * 0.5f));
//////            yield return null;
//////        }

//////        morphedShadow.transform.localScale = targetScale;
//////    }


//////    void HideShadowThoughtBubbleAndCircles()
//////    {
//////        shadowThoughtBubble?.SetActive(false);
//////        shadowThoughtText?.gameObject.SetActive(false);
//////        shadowCircle1?.SetActive(false);
//////        shadowCircle2?.SetActive(false);
//////        shadowCircle3?.SetActive(false);
//////    }

//////    IEnumerator PlayShadowThoughtSequence()
//////    {
//////        shadowThoughtBubble?.SetActive(true);

//////        ShowCircle(shadowCircle1);
//////        yield return new WaitForSeconds(0.5f);
//////        ShowCircle(shadowCircle2);
//////        yield return new WaitForSeconds(0.5f);
//////        ShowCircle(shadowCircle3);
//////        yield return new WaitForSeconds(0.5f);

//////        if (shadowThoughtText != null)
//////        {
//////            shadowThoughtText.text = "No No No We must go";
//////            shadowThoughtText.gameObject.SetActive(true);
//////        }
//////    }

//////    void ShowCircle(GameObject circle)
//////    {
//////        if (circle != null) circle.SetActive(true);
//////    }

//////}
////using UnityEngine;
////using TMPro;
////using System.Collections;
////using System.Collections.Generic;

////public class SecondPrologue : MonoBehaviour
////{
////    [SerializeField] private List<GameObject> girlTypingFrames;
////    [SerializeField] private float typingFrameInterval = 0.5f;

////    public AudioSource backgroundMusic;
////    public AudioSource typingSound;
////    public AudioSource phoneRingSound;

////    public TextMeshProUGUI textMessage1;
////    public TextMeshProUGUI textMessage2;
////    public GameObject textBubble;

////    public GameObject thoughtBubble;
////    public GameObject thoughtBubbleText;
////    public GameObject thoughtAsset;

////    public GameObject goToShoppingCenterSign;
////    public GameObject spotlight;
////    public GameObject roomLight;
////    public GameObject shadowSpotlight;

////    public GameObject shadowArmsReaching;
////    public GameObject shadow;
////    public float shadowGrowDuration = 0.5f;

////    [SerializeField] private GameObject Circle;
////    [SerializeField] private GameObject X;
////    public GameObject circle1;
////    public GameObject circle2;
////    public GameObject sparklingLines;

////    public GameObject happinessBar;

////    public GameObject shadowThoughtBubble;
////    public TextMeshProUGUI shadowThoughtText;
////    public GameObject shadowCircle1;
////    public GameObject shadowCircle2;
////    public GameObject shadowCircle3;
////    public GameObject morphedShadow;
////    public GameObject thoughtBubbleTextTwo;

////    void Start()
////    {
////        Debug.Log("SecondPrologue: Start() called.");

////        if (backgroundMusic != null)
////        {
////            backgroundMusic.loop = true;
////            backgroundMusic.volume = 0.2f;
////            backgroundMusic.Play();
////            Debug.Log("SecondPrologue: Background music started.");
////        }

////        if (typingSound != null)
////        {
////            typingSound.loop = true;
////            typingSound.Play();
////            Debug.Log("SecondPrologue: Typing sound started.");
////        }

////        if (phoneRingSound != null) phoneRingSound.Stop();

////        textMessage1?.gameObject.SetActive(false);
////        textMessage2?.gameObject.SetActive(false);
////        textBubble?.SetActive(false);
////        thoughtBubble?.SetActive(false);
////        thoughtBubbleText?.SetActive(false);
////        thoughtAsset?.SetActive(false);
////        sparklingLines?.SetActive(false);
////        goToShoppingCenterSign?.SetActive(false);
////        spotlight?.SetActive(false);
////        shadowArmsReaching?.SetActive(false);
////        shadow?.SetActive(true);
////        Circle?.SetActive(false);
////        X?.SetActive(false);
////        circle1?.SetActive(false);
////        circle2?.SetActive(false);
////        happinessBar?.SetActive(false);

////        Debug.Log("SecondPrologue: All initial objects hidden.");

////        StartCoroutine(RunSequence());
////    }

////    IEnumerator RunSequence()
////    {
////        Debug.Log("RunSequence started.");

////        yield return new WaitForSeconds(0.5f);

////        yield return StartCoroutine(PlayTypingAnimation());

////        Debug.Log("Typing animation complete.");

////        if (typingSound != null) typingSound.Stop();
////        Debug.Log("Typing sound stopped.");

////        if (phoneRingSound != null) phoneRingSound.Play();
////        Debug.Log("Phone ring sound played.");

////        yield return new WaitForSeconds(1f);

////        yield return StartCoroutine(PlayCloseLaptopAndPickPhoneAnimation());

////        Debug.Log("Close laptop and pick up phone animation done.");

////        yield return new WaitForSeconds(1f);
////        Circle?.SetActive(true);
////        X?.SetActive(true);
////        textBubble?.SetActive(true);
////        Debug.Log("Text bubble, Circle and X shown.");

////        yield return new WaitUntil(() => !textBubble.activeSelf);
////        Debug.Log("Text bubble was closed by user.");

////        yield return new WaitForSeconds(2f);

////        if (happinessBar != null) happinessBar.SetActive(true);
////        Debug.Log("Happiness bar shown.");

////        yield return new WaitForSeconds(2f);

////        ShowFirstCircle();
////        yield return new WaitForSeconds(0.5f);
////        ShowSecondCircle();
////        yield return new WaitForSeconds(0.5f);
////        ShowThoughtBubble();
////        yield return new WaitForSeconds(0.5f);
////        ShowThoughtText();
////        Debug.Log("First thought sequence shown.");
////        yield return new WaitForSeconds(3f);

////        HideThoughtBubbleAndText();
////        thoughtAsset?.SetActive(false);
////        circle1?.SetActive(false);
////        circle2?.SetActive(false);
////        Debug.Log("First thought sequence hidden.");

////        yield return new WaitForSeconds(2f);

////        Coroutine shadowBubbleSequence = StartCoroutine(PlayShadowThoughtSequence());
////        Debug.Log("Shadow thought sequence started.");

////        HideThoughtBubbleAndText();

////        yield return shadowBubbleSequence;

////        Debug.Log("Shadow thought sequence complete.");

////        yield return new WaitForSeconds(2f);

////        HideShadowThoughtBubbleAndCircles();
////        Debug.Log("Shadow thought visuals hidden.");

////        yield return new WaitForSeconds(2f);

////        ShowFirstCircle();
////        yield return new WaitForSeconds(0.5f);
////        ShowSecondCircle();
////        yield return new WaitForSeconds(0.5f);
////        ShowThoughtBubbleWithAsset();
////        yield return new WaitForSeconds(0.5f);
////        ShowThoughtTextTwo();
////        Debug.Log("Second thought sequence shown.");
////        yield return new WaitForSeconds(10f);

////        if (shadow != null)
////            yield return StartCoroutine(GrowShadow());

////        Debug.Log("Shadow growth complete.");

////        HideThoughtBubbleAndText();
////        thoughtAsset?.SetActive(false);
////        circle1?.SetActive(false);
////        circle2?.SetActive(false);

////        yield return new WaitForSeconds(3f);

////        if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
////        if (roomLight != null) roomLight.SetActive(false);
////        if (spotlight != null) spotlight.SetActive(true);
////        if (goToShoppingCenterSign != null) goToShoppingCenterSign.SetActive(true);
////        Debug.Log("Spotlight and shopping center sign shown.");
////    }

////    IEnumerator PlayTypingAnimation()
////    {
////        Debug.Log("PlayTypingAnimation started.");
////        if (girlTypingFrames.Count == 0)
////        {
////            Debug.LogError("No girl typing frames assigned!");
////            yield break;
////        }

////        foreach (var frame in girlTypingFrames)
////            frame.SetActive(false);

////        if (girlTypingFrames.Count > 0)
////        {
////            girlTypingFrames[0].SetActive(true);
////            Debug.Log("First typing frame shown.");
////        }

////        yield return new WaitForSeconds(typingFrameInterval);
////    }

////    IEnumerator PlayCloseLaptopAndPickPhoneAnimation()
////    {
////        Debug.Log("PlayCloseLaptopAndPickPhoneAnimation started.");
////        if (girlTypingFrames.Count >= 3)
////        {
////            girlTypingFrames[0].SetActive(false);
////            girlTypingFrames[1].SetActive(true);
////            yield return new WaitForSeconds(typingFrameInterval);

////            girlTypingFrames[1].SetActive(false);
////            girlTypingFrames[2].SetActive(true);
////            Debug.Log("Laptop closed and phone picked up frames shown.");
////            yield return new WaitForSeconds(typingFrameInterval);
////        }
////        else
////        {
////            Debug.LogError("Not enough frames for laptop and phone animation!");
////        }
////    }

////    void ShowFirstCircle() { if (circle1 != null) circle1.SetActive(true); }
////    void ShowSecondCircle() { if (circle2 != null) circle2.SetActive(true); }
////    void ShowThoughtBubble() { if (thoughtBubble != null) thoughtBubble.SetActive(true); }
////    void ShowThoughtText() { if (thoughtBubbleText != null) thoughtBubbleText.SetActive(true); }
////    void ShowThoughtTextTwo() { if (thoughtBubbleTextTwo != null) thoughtBubbleTextTwo.SetActive(true); }

////    void HideThoughtBubbleAndText()
////    {
////        if (circle1 != null) circle1.SetActive(false);
////        if (circle2 != null) circle2.SetActive(false);
////        if (thoughtBubble != null) thoughtBubble.SetActive(false);
////        if (thoughtBubbleText != null) thoughtBubbleText.SetActive(false);
////        if (thoughtBubbleTextTwo != null) thoughtBubbleTextTwo.SetActive(false);
////    }

////    void ShowThoughtBubbleWithAsset()
////    {
////        if (thoughtBubble != null) thoughtBubble.SetActive(true);
////        StartCoroutine(ShowAssetAfterDelay());
////    }

////    IEnumerator ShowAssetAfterDelay()
////    {
////        yield return new WaitForSeconds(1f);
////        if (thoughtAsset != null) thoughtAsset.SetActive(true);
////        Debug.Log("Thought asset shown.");
////        yield return new WaitForSeconds(1f);
////        ShowSparklingLines();
////        TriggerArmMovement();
////    }

////    void ShowSparklingLines()
////    {
////        if (sparklingLines != null) sparklingLines.SetActive(true);
////        Debug.Log("Sparkling lines shown.");
////    }

////    void TriggerArmMovement()
////    {
////        if (shadowArmsReaching != null)
////            shadowArmsReaching.SetActive(true);

////        if (shadow != null)
////            shadow.SetActive(false);

////        Debug.Log("Shadow arms reaching triggered.");
////        StartCoroutine(StopSparklesAfterAnimation());
////    }

////    IEnumerator StopSparklesAfterAnimation()
////    {
////        yield return new WaitForSeconds(4f);
////        if (sparklingLines != null) sparklingLines.SetActive(false);
////        Debug.Log("Sparkling lines stopped.");
////    }

////    IEnumerator GrowShadow()
////    {
////        Debug.Log("GrowShadow started.");
////        if (shadow == null || morphedShadow == null)
////        {
////            Debug.LogError("Shadow or morphedShadow is null!");
////            yield break;
////        }

////        Vector3 originalScale = shadow.transform.localScale;
////        morphedShadow.transform.position = shadow.transform.position;
////        morphedShadow.transform.localScale = originalScale;

////        shadow.SetActive(false);
////        morphedShadow.SetActive(true);

////        yield return new WaitForSeconds(0.5f);

////        Vector3 targetScale = originalScale * 1.2f;
////        Vector3 overshootScale = targetScale * 1.05f;

////        float duration = shadowGrowDuration;
////        float elapsed = 0f;

////        while (elapsed < duration * 0.5f)
////        {
////            elapsed += Time.deltaTime;
////            morphedShadow.transform.localScale = Vector3.Lerp(originalScale, overshootScale, elapsed / (duration * 0.5f));
////            yield return null;
////        }

////        elapsed = 0f;
////        while (elapsed < duration * 0.5f)
////        {
////            elapsed += Time.deltaTime;
////            morphedShadow.transform.localScale = Vector3.Lerp(overshootScale, targetScale, elapsed / (duration * 0.5f));
////            yield return null;
////        }

////        morphedShadow.transform.localScale = targetScale;
////        Debug.Log("GrowShadow completed.");
////    }

////    void HideShadowThoughtBubbleAndCircles()
////    {
////        shadowThoughtBubble?.SetActive(false);
////        shadowThoughtText?.gameObject.SetActive(false);
////        shadowCircle1?.SetActive(false);
////        shadowCircle2?.SetActive(false);
////        shadowCircle3?.SetActive(false);
////    }

////    IEnumerator PlayShadowThoughtSequence()
////    {
////        shadowThoughtBubble?.SetActive(true);
////        Debug.Log("Shadow thought bubble shown.");

////        ShowCircle(shadowCircle1);
////        yield return new WaitForSeconds(0.5f);
////        ShowCircle(shadowCircle2);
////        yield return new WaitForSeconds(0.5f);
////        ShowCircle(shadowCircle3);
////        yield return new WaitForSeconds(0.5f);

////        if (shadowThoughtText != null)
////        {
////            shadowThoughtText.text = "No No No We must go";
////            shadowThoughtText.gameObject.SetActive(true);
////            Debug.Log("Shadow thought text displayed.");
////        }
////    }

////    void ShowCircle(GameObject circle)
////    {
////        if (circle != null) circle.SetActive(true);
////    }
////}
//using UnityEngine;
//using TMPro;
//using System.Collections;
//using System.Collections.Generic;

//public class SecondPrologue : MonoBehaviour
//{
//    [SerializeField] private List<GameObject> girlTypingFrames;
//    [SerializeField] private float typingFrameInterval = 0.5f;

//    public AudioSource backgroundMusic;
//    public AudioSource typingSound;
//    public AudioSource phoneRingSound;

//    public TextMeshProUGUI textMessage1;
//    public TextMeshProUGUI textMessage2;
//    public GameObject textBubble;

//    public GameObject thoughtBubble;
//    public GameObject thoughtBubbleText;
//    public GameObject thoughtAsset;

//    public GameObject goToShoppingCenterSign;
//    public GameObject spotlight;
//    public GameObject roomLight;

//    public GameObject shadowArmsReaching;
//    public GameObject shadow;

//    [SerializeField] private GameObject Circle;
//    [SerializeField] private GameObject X;
//    public GameObject circle1;
//    public GameObject circle2;
//    public GameObject sparklingLines;

//    public GameObject happinessBar;

//    void Start()
//    {
//        if (backgroundMusic != null)
//        {
//            backgroundMusic.loop = true;
//            backgroundMusic.volume = 0.2f;
//            backgroundMusic.Play();
//        }

//        if (typingSound != null)
//        {
//            typingSound.loop = true;
//            typingSound.Play();
//        }

//        if (phoneRingSound != null) phoneRingSound.Stop();

//        textMessage1?.gameObject.SetActive(false);
//        textMessage2?.gameObject.SetActive(false);
//        textBubble?.SetActive(false);
//        thoughtBubble?.SetActive(false);
//        thoughtBubbleText?.SetActive(false);
//        thoughtAsset?.SetActive(false);
//        sparklingLines?.SetActive(false);
//        goToShoppingCenterSign?.SetActive(false);
//        spotlight?.SetActive(false);
//        shadowArmsReaching?.SetActive(false);
//        shadow?.SetActive(true);
//        Circle?.SetActive(false);
//        X?.SetActive(false);
//        circle1?.SetActive(false);
//        circle2?.SetActive(false);
//        happinessBar?.SetActive(false);

//        StartCoroutine(RunSequence());
//    }

//    IEnumerator RunSequence()
//    {
//        yield return new WaitForSeconds(0.5f);

//        yield return StartCoroutine(PlayTypingAnimation());

//        if (typingSound != null) typingSound.Stop();
//        if (phoneRingSound != null) phoneRingSound.Play();

//        yield return new WaitForSeconds(1f);

//        yield return StartCoroutine(PlayCloseLaptopAndPickPhoneAnimation());

//        ShowFirstCircle();
//        yield return new WaitForSeconds(0.5f);
//        ShowSecondCircle();
//        yield return new WaitForSeconds(0.5f);

//        ShowThoughtBubble();
//        yield return new WaitForSeconds(1f);
//        ShowThoughtText();
//        yield return new WaitForSeconds(3f);
//        HideThoughtBubbleAndText();

//        yield return new WaitForSeconds(1f);
//        Circle?.SetActive(true);
//        X?.SetActive(true);
//        textBubble?.SetActive(true);

//        yield return new WaitUntil(() => !textBubble.activeSelf);

//        yield return new WaitForSeconds(2f);

//        // Show Happiness Bar after text messages
//        if (happinessBar != null) happinessBar.SetActive(true);

//        yield return new WaitForSeconds(2f);

//        ShowFirstCircle();
//        yield return new WaitForSeconds(0.5f);
//        ShowSecondCircle();
//        yield return new WaitForSeconds(0.5f);
//        ShowThoughtBubbleWithAsset();
//        yield return new WaitForSeconds(10f);

//        circle1?.SetActive(false);
//        circle2?.SetActive(false);

//        HideThoughtBubbleAndText();
//        thoughtAsset?.SetActive(false);

//        yield return new WaitForSeconds(3f);

//        if (roomLight != null) roomLight.SetActive(false);
//        if (spotlight != null) spotlight.SetActive(true);
//        if (goToShoppingCenterSign != null) goToShoppingCenterSign.SetActive(true);
//    }

//    IEnumerator PlayTypingAnimation()
//    {
//        foreach (var frame in girlTypingFrames)
//            frame.SetActive(false);

//        if (girlTypingFrames.Count > 0)
//            girlTypingFrames[0].SetActive(true);

//        yield return new WaitForSeconds(typingFrameInterval);
//    }

//    IEnumerator PlayCloseLaptopAndPickPhoneAnimation()
//    {
//        if (girlTypingFrames.Count >= 3)
//        {
//            girlTypingFrames[0].SetActive(false);
//            girlTypingFrames[1].SetActive(true);
//            yield return new WaitForSeconds(typingFrameInterval);

//            girlTypingFrames[1].SetActive(false);
//            girlTypingFrames[2].SetActive(true);
//            yield return new WaitForSeconds(typingFrameInterval);
//        }
//    }

//    void ShowFirstCircle() { if (circle1 != null) circle1.SetActive(true); }
//    void ShowSecondCircle() { if (circle2 != null) circle2.SetActive(true); }
//    void ShowThoughtBubble() { if (thoughtBubble != null) thoughtBubble.SetActive(true); }
//    void ShowThoughtText() { if (thoughtBubbleText != null) thoughtBubbleText.SetActive(true); }
//    void HideThoughtBubbleAndText()
//    {
//        if (thoughtBubble != null) thoughtBubble.SetActive(false);
//        if (thoughtBubbleText != null) thoughtBubbleText.SetActive(false);
//    }
//    void ShowThoughtBubbleWithAsset()
//    {
//        if (thoughtBubble != null) thoughtBubble.SetActive(true);
//        StartCoroutine(ShowAssetAfterDelay());
//    }
//    IEnumerator ShowAssetAfterDelay()
//    {
//        yield return new WaitForSeconds(1f);
//        if (thoughtAsset != null) thoughtAsset.SetActive(true);
//        yield return new WaitForSeconds(1f);
//        ShowSparklingLines();
//        TriggerArmMovement();
//    }

//    void ShowSparklingLines()
//    {
//        if (sparklingLines != null) sparklingLines.SetActive(true);
//    }

//    void TriggerArmMovement()
//    {
//        if (shadowArmsReaching != null)
//            shadowArmsReaching.SetActive(true);

//        if (shadow != null)
//            shadow.SetActive(false);
//        StartCoroutine(StopSparklesAfterAnimation());
//    }

//    IEnumerator StopSparklesAfterAnimation()
//    {
//        yield return new WaitForSeconds(4f);
//        if (sparklingLines != null) sparklingLines.SetActive(false);
//    }
//}

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