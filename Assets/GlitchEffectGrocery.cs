using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class GlitchEffectGrocery : MonoBehaviour
{
    [SerializeField] private GameObject normalAisles;
    [SerializeField] private GameObject platformAisles;
    [SerializeField] private PostProcessVolume glitchVolume; // Drag your Post-Process Volume here
    [SerializeField] private float glitchDuration = 1f; // Duration of the glitch effect
    [SerializeField] private float glitchDelay = 10f; // Time before glitch starts
    [SerializeField] private AudioSource glitchSound; // Optional: Drag an Audio Source with glitch sounds

    private void Start()
    {
        // Make sure the glitch effect is off at the start
        glitchVolume.gameObject.SetActive(false);

        // Start glitch sequence after 10 seconds
        StartCoroutine(StartGlitchAfterDelay());
    }

    private IEnumerator StartGlitchAfterDelay()
    {
        // Wait for the set delay
        yield return new WaitForSeconds(glitchDelay);

        // Start the glitch sequence
        TriggerGlitch();
    }

    public void TriggerGlitch()
    {
        StartCoroutine(GlitchSequence());
    }

    private IEnumerator GlitchSequence()
    {
        // Play glitch sound
        if (glitchSound) glitchSound.Play();

        // Enable glitch post-processing effect
        glitchVolume.gameObject.SetActive(true);

        // Wait for glitch duration
        yield return new WaitForSeconds(glitchDuration);

        // Disable normal aisles
        normalAisles.SetActive(false);

        // ✅ Ensure the platforming parent is enabled FIRST
        platformAisles.SetActive(true);

        // ✅ Activate all moving platforms inside the parent
        foreach (MovingPlatform platform in platformAisles.GetComponentsInChildren<MovingPlatform>())
        {
            //platform.ActivatePlatform();
        }

        // Stop the glitch effect
        glitchVolume.gameObject.SetActive(false);
    }


}
