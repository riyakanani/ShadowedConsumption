using UnityEngine;

public class PageTurnSound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayPageTurnSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}