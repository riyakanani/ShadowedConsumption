using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GlassesPrologueSceneController : MonoBehaviour
{
    [Header("Girl Trying On Glasses Animation")]
    [SerializeField] private List<GameObject> girlTryingOnGlassesFrames;
    [SerializeField] private float frameInterval = 1f;

    [Header("Shadow Settings")]
    public GameObject shadow;
    public float shadowGrowDuration = 0.5f;

    [Header("Lighting and Spotlight")]
    public GameObject roomLight;
    public GameObject finalSpotlight;
    public GameObject shadowSpotlight;

    [Header("Glitch and Scene Transition")]
    public GameObject glitchOverlay; 
    public Image fadePanel; 
    public string nextSceneName;
    public float glitchDuration = 1f;
    public float fadeDuration = 1f;

    [Header("Timing")]
    public float startDelay = 1f;

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

        
        Coroutine glassesAnim = StartCoroutine(PlayGlassesAnimation());
        yield return glassesAnim;

        yield return new WaitForSeconds(1f);

        if (shadow != null)
            yield return StartCoroutine(GrowShadow());

        yield return new WaitForSeconds(1f);

        // 3. Start Glitch
        if (glitchOverlay != null) glitchOverlay.SetActive(true);
        StartCoroutine(CameraGlitchShake(glitchDuration, 0.1f));
        yield return new WaitForSeconds(glitchDuration);

        if (glitchOverlay != null) glitchOverlay.SetActive(false);

        // 4. Fade to Black
        if (fadePanel != null)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / fadeDuration;
                fadePanel.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, t));
                yield return null;
            }
        }

        // 5. Load Boss Scene
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator PlayGlassesAnimation()
    {
        foreach (var frame in girlTryingOnGlassesFrames)
            frame.SetActive(false);

        for (int i = 0; i < girlTryingOnGlassesFrames.Count; i++)
        {
            girlTryingOnGlassesFrames[i].SetActive(true);
            if (i > 0) girlTryingOnGlassesFrames[i - 1].SetActive(false);
            yield return new WaitForSeconds(frameInterval);
        }
    }

    IEnumerator GrowShadow()
    {
        if (shadow == null) yield break;

        Vector3 originalScale = shadow.transform.localScale;
        Vector3 targetScale = originalScale * 1.07f;
        Vector3 overshootScale = targetScale * 1.04f;

        float duration = shadowGrowDuration;
        float elapsed = 0f;

        // Phase 1: Grow to Overshoot
        while (elapsed < duration * 0.5f)
        {
            elapsed += Time.deltaTime;
            shadow.transform.localScale = Vector3.Lerp(originalScale, overshootScale, elapsed / (duration * 0.5f));
            yield return null;
        }

        // Phase 2: Settle Back to Target
        elapsed = 0f;
        while (elapsed < duration * 0.5f)
        {
            elapsed += Time.deltaTime;
            shadow.transform.localScale = Vector3.Lerp(overshootScale, targetScale, elapsed / (duration * 0.5f));
            yield return null;
        }

        shadow.transform.localScale = targetScale;
    }

    IEnumerator CameraGlitchShake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }
}

