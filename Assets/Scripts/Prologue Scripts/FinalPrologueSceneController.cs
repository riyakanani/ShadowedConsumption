using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FinalPrologueSceneController : MonoBehaviour
{
    [Header("Girl Eating Chips Animation")]
    [SerializeField] private List<GameObject> girlEatingChipsFrames;
    [SerializeField] private float eatingFrameInterval = 1f;
    public AudioSource chipEatingSound;

    [Header("UI Elements")]
    public GameObject thoughtBubble;
    public TextMeshProUGUI thoughtBubbleText;
    public GameObject happinessBar;
    public Slider happinessSlider;

    [Header("Circles for Thought Bubble Timing")]
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;

    [Header("Shadow")]
    public GameObject shadow;
    public float shadowGrowDuration = 0.5f;

    [Header("Lighting and End Button")]
    public GameObject roomLight;
    public GameObject finalSpotlight;
    public GameObject finalSceneButton;
    public GameObject shadowSpotlight;

    [Header("Timing")]
    public float thoughtBubbleStartDelay = 0.5f;

    void Start()
    {
        // Reset all objects
        thoughtBubble?.SetActive(false);
        thoughtBubbleText?.gameObject.SetActive(false);
        circle1?.SetActive(false);
        circle2?.SetActive(false);
        circle3?.SetActive(false);
        finalSpotlight?.SetActive(false);
        finalSceneButton?.SetActive(false);
        shadowSpotlight?.SetActive(true);
        happinessBar?.SetActive(true);

        StartCoroutine(PlayFinalPrologueSequence());
    }

    IEnumerator PlayFinalPrologueSequence()
    {
        yield return new WaitForSeconds(1f);

        Coroutine chipAnim = StartCoroutine(PlayEatingChipsAnimation());
        yield return new WaitForSeconds(thoughtBubbleStartDelay);
        Coroutine bubbleSequence = StartCoroutine(PlayThoughtBubbleSequence());

        yield return chipAnim;
        yield return bubbleSequence;

        yield return new WaitForSeconds(1f);

        if (shadow != null)
            yield return StartCoroutine(GrowShadow());

        yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(1f);

        HideThoughtBubbleAndCircles();

        if (happinessSlider != null)
        {
            float originalValue = happinessSlider.value;
            float targetValue = Mathf.Max(0, originalValue - 0.1f);
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime;
                happinessSlider.value = Mathf.Lerp(originalValue, targetValue, t);
                yield return null;
            }
        }

        yield return new WaitForSeconds(1.5f);

        if (shadowSpotlight != null) shadowSpotlight.SetActive(false);
        roomLight?.SetActive(false);
        finalSpotlight?.SetActive(true);
        finalSceneButton?.SetActive(true);
    }

    IEnumerator PlayEatingChipsAnimation()
    {
        foreach (var frame in girlEatingChipsFrames)
            frame.SetActive(false);

        for (int i = 0; i < girlEatingChipsFrames.Count; i++)
        {
            girlEatingChipsFrames[i].SetActive(true);
            if (i > 0) girlEatingChipsFrames[i - 1].SetActive(false);

            if (i == girlEatingChipsFrames.Count - 1 && chipEatingSound != null)
            {
                chipEatingSound.Play();
            }

            yield return new WaitForSeconds(eatingFrameInterval);
        }
    }

    IEnumerator PlayThoughtBubbleSequence()
    {
        thoughtBubble?.SetActive(true);

        ShowCircle(circle1);
        yield return new WaitForSeconds(0.5f);
        ShowCircle(circle2);
        yield return new WaitForSeconds(0.5f);
        ShowCircle(circle3);
        yield return new WaitForSeconds(0.5f);

        if (thoughtBubbleText != null)
        {
            thoughtBubbleText.text = "These chips are yummy";
            thoughtBubbleText.gameObject.SetActive(true);
        }
    }

    IEnumerator GrowShadow()
    {
        if (shadow == null) yield break;

        Vector3 originalScale = shadow.transform.localScale;
        Vector3 targetScale = originalScale * 1.07f; // slightly more than before
        Vector3 overshootScale = targetScale * 1.04f; // subtle bounce above target

        float duration = shadowGrowDuration;
        float elapsed = 0f;

        // Phase 1: grow to overshoot
        while (elapsed < duration * 0.5f)
        {
            elapsed += Time.deltaTime;
            shadow.transform.localScale = Vector3.Lerp(originalScale, overshootScale, elapsed / (duration * 0.5f));
            yield return null;
        }

        // Phase 2: settle back to final size
        elapsed = 0f;
        while (elapsed < duration * 0.5f)
        {
            elapsed += Time.deltaTime;
            shadow.transform.localScale = Vector3.Lerp(overshootScale, targetScale, elapsed / (duration * 0.5f));
            yield return null;
        }

        shadow.transform.localScale = targetScale;
    }


    void ShowCircle(GameObject circle)
    {
        if (circle != null) circle.SetActive(true);
    }

    void HideThoughtBubbleAndCircles()
    {
        thoughtBubble?.SetActive(false);
        thoughtBubbleText?.gameObject.SetActive(false);
        circle1?.SetActive(false);
        circle2?.SetActive(false);
        circle3?.SetActive(false);
    }
}
