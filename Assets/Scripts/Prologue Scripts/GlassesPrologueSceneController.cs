using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GlassesPrologueSceneController : MonoBehaviour
{
    [Header("Girl Trying On Glasses Animation")]
    [SerializeField] private List<GameObject> girlTryingOnGlassesFrames;
    [SerializeField] private float frameInterval = 0.5f;
    public GameObject whiteLight;

    [Header("Audio")]
    public AudioSource monsterGrowlSound;

    [Header("Thought Sequences")]
    public List<GameObject> shadowThoughtCircles; // 3 circles
    public List<GameObject> shadowThoughtCircles2; // 3 circles
    public List<GameObject> shadowThoughtCircles3; // 3 circles
    public List<GameObject> shadowThoughtCircles4;


    [Header("Lighting Switch and Background")]
    public GameObject lightSwitchOff;
    public GameObject lightSwitchOn;
    public GameObject whiteBackground;


    public List<GameObject> girlThoughtCircles1;
    public List<GameObject> girlThoughtCircles2;
    public List<GameObject> shadowMonsterThoughtCircles;
    public List<GameObject> shadowMonsterThoughtCircles2;


    public GameObject shadowThoughtText;
    public GameObject shadowThoughtText3;
    public GameObject shadowThoughtText4;


    public GameObject girlThoughtText1;
    public GameObject girlThoughtText2;
    public GameObject shadowMonsterThoughtText;

    [Header("Shadow Settings")]
    public GameObject shadow;
    [SerializeField] private List<GameObject> shadowMovementFrames;
    [SerializeField] private List<GameObject> shadowVillainFrames;
    [SerializeField] private float shadowFrameInterval = 0.5f;
    public float shadowGrowDuration = 2f;

    [Header("Girl Other Animations")]
    public List<GameObject> girlDraggingFrames;
    public List<GameObject> girlCuttingFrames;
    public GameObject girlFightFrame;

    [Header("Lighting and Spotlight")]
    public GameObject roomLight;
    public GameObject finalSpotlight;
    public GameObject shadowSpotlight;

    [Header("Glitch and Scene Transition")]
    public GameObject glitchOverlay;
    public Image fadePanel;
    public string nextSceneName;
    public float glitchDuration = 2f;
    public float fadeDuration = 1f;

    [Header("Timing")]
    public float startDelay = 1f;

    [Header("Chair and Girl GameObjects")]
    public GameObject girl;
    public GameObject emptyChair;


    [Header("Get-Up Animation")]
    public List<GameObject> girlGetUpFrames;
    public GameObject shadowThoughtText2;


    void Start()
    {
        if (glitchOverlay != null) glitchOverlay.SetActive(false);
        if (fadePanel != null) fadePanel.color = new Color(0, 0, 0, 0);

        if (finalSpotlight != null) finalSpotlight.SetActive(false);
        if (shadowSpotlight != null) shadowSpotlight.SetActive(true);
        if (roomLight != null) roomLight.SetActive(true);

        StartCoroutine(PlayGlassesPrologueSequence());
    }

    IEnumerator PlayGlassesPrologueSequence()
    {
        yield return new WaitForSeconds(startDelay);

        yield return StartCoroutine(PlayGlassesAnimation());
        yield return new WaitForSeconds(1f);

        if (shadow != null)
            yield return StartCoroutine(GrowShadow());

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(PlayThoughtSequence(shadowThoughtCircles, shadowThoughtText, "YES YES YES let's go back to the store and buy more"));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(PlayThoughtSequence(girlThoughtCircles1, girlThoughtText1, "No we don't need anything more"));

        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayShadowMovementAnimation());
        StartCoroutine(PlayGirlDraggingAnimation());

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(PlayThoughtSequence(shadowThoughtCircles2, shadowThoughtText2, "No we are going"));


        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(PlayThoughtSequence(girlThoughtCircles2, girlThoughtText2, "Stop! I've had enough"));
        yield return new WaitForSeconds(0.5f);


        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(PlayThoughtSequence(shadowThoughtCircles4, shadowThoughtText4, "There is never enough! Nothing is ever enough!"));

        yield return StartCoroutine(PlayGirlCuttingAnimation());
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(PlayGirlGetUpAnimation());
        if (girlGetUpFrames.Count > 0)
            girlGetUpFrames[girlGetUpFrames.Count - 1].SetActive(false);
        if (girlFightFrame != null) girlFightFrame.SetActive(true);

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(PlayShadowVillainAnimation());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(PlayThoughtSequence(shadowThoughtCircles3, shadowThoughtText3, "Then Let's fight!"));

       
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator PlayGlassesAnimation()
    {
        foreach (var frame in girlTryingOnGlassesFrames) frame.SetActive(false);

        for (int i = 0; i < girlTryingOnGlassesFrames.Count; i++)
        {
            girlTryingOnGlassesFrames[i].SetActive(true);
            if (i > 0) girlTryingOnGlassesFrames[i - 1].SetActive(false);
            yield return new WaitForSeconds(frameInterval);
        }
    }

    IEnumerator GrowShadow()
    {
        Vector3 originalScale = shadow.transform.localScale;
        Vector3 targetScale = originalScale * 1.2f;
        Vector3 overshootScale = targetScale * 1.05f;

        float elapsed = 0f;

        while (elapsed < shadowGrowDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            shadow.transform.localScale = Vector3.Lerp(originalScale, overshootScale, elapsed / (shadowGrowDuration * 0.5f));
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < shadowGrowDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            shadow.transform.localScale = Vector3.Lerp(overshootScale, targetScale, elapsed / (shadowGrowDuration * 0.5f));
            yield return null;
        }

        shadow.transform.localScale = targetScale;
    }

    IEnumerator PlayShadowMovementAnimation()
    {
        if (shadow != null)
            shadow.SetActive(false);

        foreach (var frame in shadowMovementFrames) frame.SetActive(false);
        foreach (var frame in girlDraggingFrames) frame.SetActive(false);

        int count = Mathf.Min(shadowMovementFrames.Count, girlDraggingFrames.Count);

        // Optional: hide girl and show empty chair at start
        if (girl != null) girl.SetActive(false);
        if (emptyChair != null) emptyChair.SetActive(true);

        for (int i = 0; i < count; i++)
        {
            // SHADOW moves first
            shadowMovementFrames[i].SetActive(true);
            if (i > 0) shadowMovementFrames[i - 1].SetActive(false);

            // Delay before girl starts moving to simulate drag
            StartCoroutine(DelayedGirlFrame(i));

            // Chair stuff only once
            if (i == 0)
            {
                if (girl != null) girl.SetActive(false);
                if (emptyChair != null) emptyChair.SetActive(true);
            }

            yield return new WaitForSeconds(shadowFrameInterval);
        }
    }

    //IEnumerator PlayShadowMovementAnimation()
    //{
    //    if (shadow != null)
    //        shadow.SetActive(false);

    //    foreach (var frame in shadowMovementFrames) frame.SetActive(false);
    //    foreach (var frame in girlDraggingFrames) frame.SetActive(false);

    //    int count = Mathf.Min(shadowMovementFrames.Count, girlDraggingFrames.Count);

    //    for (int i = 0; i < count; i++)
    //    {
    //        // Enable current shadow frame
    //        shadowMovementFrames[i].SetActive(true);
    //        if (i > 0) shadowMovementFrames[i - 1].SetActive(false);

    //        // Enable current girl dragging frame
    //        girlDraggingFrames[i].SetActive(true);
    //        if (i > 0) girlDraggingFrames[i - 1].SetActive(false);

    //        // Hide chair and original girl on first step
    //        if (i == 0)
    //        {
    //            if (girl != null) girl.SetActive(false);
    //            if (emptyChair != null) emptyChair.SetActive(true);
    //        }

    //        yield return new WaitForSeconds(shadowFrameInterval);
    //    }
    //}


    IEnumerator PlayGirlDraggingAnimation()
    {
        if (girl != null) girl.SetActive(false);
        if (emptyChair != null) emptyChair.SetActive(true);

        foreach (var frame in girlDraggingFrames) frame.SetActive(false);

        for (int i = 0; i < girlDraggingFrames.Count; i++)
        {
            girlDraggingFrames[i].SetActive(true);
            if (i > 0) girlDraggingFrames[i - 1].SetActive(false);
            yield return new WaitForSeconds(frameInterval);
        }
    }

    IEnumerator PlayGirlCuttingAnimation()
    {
        // Disable other girl frames (dragging + trying on glasses)
        foreach (var frame in girlDraggingFrames) frame.SetActive(false);
        foreach (var frame in girlTryingOnGlassesFrames) frame.SetActive(false);
        if (girlFightFrame != null) girlFightFrame.SetActive(false);
        if (girl != null) girl.SetActive(false); // In case original girl GameObject is still active

        // Enable cutting animation frames one by one
        foreach (var frame in girlCuttingFrames) frame.SetActive(false);

        for (int i = 0; i < girlCuttingFrames.Count; i++)
        {
            girlCuttingFrames[i].SetActive(true);
            if (i > 0) girlCuttingFrames[i - 1].SetActive(false);
            yield return new WaitForSeconds(frameInterval);
        }
    }


    IEnumerator PlayShadowVillainAnimation()
    {
        // Play growl sound
        if (monsterGrowlSound != null) monsterGrowlSound.Play();

        // Hide last movement frame
        if (shadowMovementFrames.Count > 0)
            shadowMovementFrames[shadowMovementFrames.Count - 1].SetActive(false);

        // Hide all villain frames
        foreach (var frame in shadowVillainFrames)
            frame.SetActive(false);

        // Play villain animation frames
        for (int i = 0; i < shadowVillainFrames.Count; i++)
        {
            shadowVillainFrames[i].SetActive(true);
            if (i > 0) shadowVillainFrames[i - 1].SetActive(false);

            if (i == 1)
            {
                if (lightSwitchOff != null) lightSwitchOff.SetActive(false);
                if (lightSwitchOn != null) lightSwitchOn.SetActive(true);
                if (whiteBackground != null) whiteBackground.SetActive(true);
                if (whiteLight != null) whiteLight.SetActive(true);
                if (emptyChair != null) emptyChair.SetActive(false);
                if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
            }

            yield return new WaitForSeconds(shadowFrameInterval);
        }
    }




    IEnumerator PlayThoughtSequence(List<GameObject> circles, GameObject textBox, string text)
    {
        // Disable all bubbles and hide the text initially
        foreach (var circle in circles) circle.SetActive(false);
        if (textBox != null)
        {
            textBox.SetActive(false);
            if (textBox.GetComponent<TMPro.TextMeshProUGUI>() != null)
                textBox.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        }

        // Show bubbles one by one
        for (int i = 0; i < circles.Count; i++)
        {
            circles[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        // Enable text after all bubbles are shown
        if (textBox != null)
            textBox.SetActive(true);

        yield return new WaitForSeconds(3f);

        // Hide bubbles and text after the delay
        foreach (var circle in circles) circle.SetActive(false);
        if (textBox != null) textBox.SetActive(false);
    }

    IEnumerator PlayGirlGetUpAnimation()
    {
        if (girlCuttingFrames.Count > 0)
            girlCuttingFrames[girlCuttingFrames.Count - 1].SetActive(false);

        foreach (var frame in girlGetUpFrames) frame.SetActive(false);
        girlFightFrame?.SetActive(false);

        for (int i = 0; i < girlGetUpFrames.Count; i++)
        {
            girlGetUpFrames[i].SetActive(true);
            if (i > 0) girlGetUpFrames[i - 1].SetActive(false);
            yield return new WaitForSeconds(frameInterval);
        }

        
    }

    //IEnumerator DelayedGirlFrame(int index)
    //{
    //    yield return new WaitForSeconds(0.1f); // small delay to simulate drag

    //    if (index < girlDraggingFrames.Count)
    //    {
    //        girlDraggingFrames[index].SetActive(true);
    //        if (index > 0)
    //            girlDraggingFrames[index - 1].SetActive(false);
    //    }
    //}

    //IEnumerator DelayedGirlFrame(int index)
    //{
    //    // Skip delay for the very last frame to keep things in sync
    //    if (index < girlDraggingFrames.Count - 1)
    //        yield return new WaitForSeconds(0.2f); // was 0.1f


    //    if (index < girlDraggingFrames.Count)
    //    {
    //        girlDraggingFrames[index].SetActive(true);
    //        if (index > 0)
    //            girlDraggingFrames[index - 1].SetActive(false);
    //    }
    //}

    IEnumerator DelayedGirlFrame(int index)
    {
        if (index < girlDraggingFrames.Count)
        {
            var girlObj = girlDraggingFrames[index];
            girlObj.SetActive(true);
            if (index > 0) girlDraggingFrames[index - 1].SetActive(false);

            Vector3 originalPos = girlObj.transform.localPosition;
            Vector3 dragBackPos = originalPos + new Vector3(-0.1f, 0, 0); // back slightly

            girlObj.transform.localPosition = dragBackPos;

            
            float duration = 0.2f;
            float t = 0f;
            while (t < duration)
            {
                girlObj.transform.localPosition = Vector3.Lerp(dragBackPos, originalPos, t / duration);
                t += Time.deltaTime;
                yield return null;
            }

            girlObj.transform.localPosition = originalPos;
        }
    }





}
