

//using UnityEngine;
//using System.Collections;

//public class SceneSequenceController : MonoBehaviour
//{
//    // Assign these in the Inspector
//    public GameObject openBook;
//    public GameObject closedBook;
//    public GameObject phoneInHand;
//    public AudioSource pageTurningSound; // AudioSource for page-turning sound
//    public AudioSource phoneRingSound; // AudioSource for phone ringing sound

//    void Start()
//    {
//        // Initial states
//        openBook.SetActive(true);         // Open book visible
//        closedBook.SetActive(false);      // Closed book hidden
//        phoneInHand.SetActive(false);     // Phone hidden initially

//        // Start page-turning sound loop
//        if (pageTurningSound != null)
//        {
//            pageTurningSound.loop = true; // Ensure loop is on initially
//            pageTurningSound.Play();
//        }

//        // Begin the sequence coroutine
//        StartCoroutine(RunSequence());
//    }

//    IEnumerator RunSequence()
//    {
//        // 1. Girl reads the book for 7 seconds
//        yield return new WaitForSeconds(7f);

//        // 2. Phone starts ringing at 7 seconds
//        if (phoneRingSound != null)
//        {
//            phoneRingSound.Play();
//        }

//        // 3. Wait 1 second after the phone starts ringing
//        yield return new WaitForSeconds(1f);

//        // 4. Hide open book, show closed book
//        openBook.SetActive(false);
//        closedBook.SetActive(true);

//        // 5. Stop the page-turning sound permanently once the book is closed
//        if (pageTurningSound != null)
//        {
//            // Stop the page-turning sound and ensure it does not restart or loop
//            pageTurningSound.Stop();  // Stop the looping sound
//            pageTurningSound.loop = false; // Explicitly set loop to false
//            pageTurningSound.mute = true; // Mute the AudioSource to stop sound
//            pageTurningSound.enabled = false; // Disable the AudioSource completely
//            Debug.Log("Page-turning sound stopped, AudioSource disabled, and muted.");
//        }

//        // 6. Wait 1.5 seconds with the closed book visible
//        yield return new WaitForSeconds(1.5f);

//        // 7. Hide closed book, show phone in hand
//        closedBook.SetActive(false);
//        phoneInHand.SetActive(true);

//        // Sequence finished (runs only once)
//    }
//}

using UnityEngine;
using TMPro;  // Import TextMesh Pro namespace
using System.Collections;

public class SceneSequenceController : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject openBook;
    public GameObject closedBook;
    public GameObject phoneInHand;
    public AudioSource pageTurningSound; // AudioSource for page-turning sound
    public AudioSource phoneRingSound; // AudioSource for phone ringing sound
    public TextMeshProUGUI textMessage; // TextMeshProUGUI for displaying the message
    public GameObject textBubble; // The text bubble GameObject (PNG)

    void Start()
    {
        // Initial states
        openBook.SetActive(true);         // Open book visible
        closedBook.SetActive(false);      // Closed book hidden
        phoneInHand.SetActive(false);     // Phone hidden initially
        textMessage.gameObject.SetActive(false);  // Hide the text message initially
        textBubble.SetActive(false);  // Hide the text bubble initially

        // Start page-turning sound loop
        if (pageTurningSound != null)
        {
            pageTurningSound.loop = true; // Ensure loop is on initially
            pageTurningSound.Play();
        }

        // Begin the sequence coroutine
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        // 1. Girl reads the book for 7 seconds
        yield return new WaitForSeconds(7f);

        // 2. Phone starts ringing at 7 seconds
        if (phoneRingSound != null)
        {
            phoneRingSound.Play();
        }

        // 3. Wait 1 second after the phone starts ringing
        yield return new WaitForSeconds(1f);

        // 4. Hide open book, show closed book
        openBook.SetActive(false);
        closedBook.SetActive(true);

        // 5. Stop the page-turning sound permanently once the book is closed
        if (pageTurningSound != null)
        {
            pageTurningSound.Stop();  // Stop the looping sound
            pageTurningSound.loop = false; // Ensure it's not looping anymore
            pageTurningSound.mute = true; // Mute the AudioSource to stop sound
            pageTurningSound.enabled = false; // Disable the AudioSource completely
        }

        // 6. Wait 1.5 seconds with the closed book visible
        yield return new WaitForSeconds(1.5f);

        // 7. Hide closed book, show phone in hand
        closedBook.SetActive(false);
        phoneInHand.SetActive(true);

        // 8. Wait for 2 seconds after phone appears, then display text message and text bubble
        yield return new WaitForSeconds(2f);

        // Show the text bubble and text message at the same time
        ShowTextMessage("Hey, let's go to the store and try these new chips!");

        // Sequence finished (runs only once)
    }

    // Function to display the text message and text bubble
    void ShowTextMessage(string message)
    {
        if (textMessage != null && textBubble != null)
        {
            textMessage.text = message; // Set the message
            textMessage.gameObject.SetActive(true); // Show the text
            textBubble.SetActive(true); // Show the text bubble

        }
    }
}

