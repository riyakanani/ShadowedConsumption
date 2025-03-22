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
    public TextMeshProUGUI textMessage1; // TextMeshProUGUI for displaying the first message
    public TextMeshProUGUI textMessage2; // TextMeshProUGUI for displaying the second message
    public GameObject textBubble; // The text bubble GameObject (PNG)
    public GameObject goToGroceryStoreSign;  // The sign to go to the grocery store
    public GameObject spotlightOnGroceryStore;  // Spotlight to highlight the sign
    public GameObject shadowSpotlight;  // The spotlight for the shadow
    public GameObject roomLight;

    // Thought bubble variables
    public GameObject thoughtBubble; // Thought bubble GameObject (PNG)
    public GameObject chipsAsset;    // The chips floating asset (image or 3D object)
    public TextMeshProUGUI thoughtBubbleText; // Text inside the thought bubble
    public TextMeshProUGUI chipsText; // Text below the chips (thought continuation)

    // Shadow face and animator references
    public GameObject oldShadowFace;  // The old face of the shadow
    public GameObject newShadowFace;  // The new face of the shadow
    public Animator shadowAnimator; // Shadow's main animator (for general animations)
    public Animator shadowArmAnimator; // Animator for the shadow's arm movement

    // Animator references for head and shadow
    public Animator headAnimator;  // Head animator

    // Circle objects for the thought bubble animation
    public GameObject circle1; // First circle to appear
    public GameObject circle2; // Second circle to appear

    // Sparkling lines effect (already existing as a GameObject in the scene)
    public GameObject sparklingLines; // The sparkling lines GameObject

    void Start()
    {
        // Initial states
        openBook.SetActive(true);         // Open book visible
        closedBook.SetActive(false);      // Closed book hidden
        phoneInHand.SetActive(false);     // Phone hidden initially
        textMessage1.gameObject.SetActive(false);  // Hide the first text message initially
        textMessage2.gameObject.SetActive(false);  // Hide the second text message initially
        textBubble.SetActive(false);  // Hide the text bubble initially
        thoughtBubble.SetActive(false); // Thought bubble hidden initially
        chipsAsset.SetActive(false); // Hide the chips asset initially
        oldShadowFace.SetActive(true); // Show the old shadow face initially
        newShadowFace.SetActive(false); // Hide the new shadow face initially

        // Set text inside the thought bubble
        thoughtBubbleText.text = "This book is so good";
        chipsText.text = "Maybe I should go and get some"; // Default chips text

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
        // 1. Girl reads the book for a longer time (15 seconds)
        yield return new WaitForSeconds(5f); // Increased time for reading

        // 2. Show the first circle (dot) for the thought bubble
        ShowFirstCircle();

        // 3. Wait for a short delay, then show the second circle (dot)
        yield return new WaitForSeconds(0.5f); // Delay between circle 1 and circle 2
        ShowSecondCircle();

        // 4. Wait for another short delay before showing the thought bubble
        yield return new WaitForSeconds(0.5f); // Delay before the thought bubble appears
        ShowThoughtBubble();

        // 5. After showing the thought bubble, show the thought text
        yield return new WaitForSeconds(2f); // Longer delay for the thought text to be visible
        ShowThoughtText("This book is so good");

        // 6. Give more time for the girl to read, even after the thought bubble is shown (additional 6 seconds)
        yield return new WaitForSeconds(6f); // Allow the girl to continue reading with the thought bubble visible

        // 8. Wait for the effect before transitioning to the next part
        yield return new WaitForSeconds(1f);

        // **Disable everything when it's time for the text messages**
        HideThoughtBubbleAndText();

        // 9. Phone starts ringing at 7 seconds
        if (phoneRingSound != null)
        {
            phoneRingSound.Play();
        }

        // **Stop animators when the book closes**
        StopAnimators();

        // 10. Wait 1 second after the phone starts ringing
        yield return new WaitForSeconds(1f);

        // 11. Hide open book, show closed book
        openBook.SetActive(false);
        closedBook.SetActive(true);

        if (pageTurningSound != null)
        {
            pageTurningSound.Stop();  // Stop the page-turning sound
        }

        // 12. Wait 1.5 seconds with the closed book visible
        yield return new WaitForSeconds(1.5f);

        // 13. Hide closed book, show phone in hand
        closedBook.SetActive(false);
        phoneInHand.SetActive(true);

        // 14. Wait for 2 seconds after phone appears, then display the first text message
        yield return new WaitForSeconds(2f);

        // Show the text bubble and first text message
        ShowTextMessage1("Hey, Did you hear about the new flavor of chips they are selling at the grocery store?");

        // 15. Wait for 4 seconds, then disable the first message and show the second message
        yield return new WaitForSeconds(4f);
        ShowTextMessage2("Everyone is saying it is sooo yummy!! You've got to get some before they sell out!!");

        // 16. Wait for another 4 seconds to allow the second text to be shown
        yield return new WaitForSeconds(4f);

        // 17. Disable text and text bubble, then show the thought bubble with chips
        HideTextAndBubble();

        // 2. Show the first circle (dot) for the thought bubble
        ShowFirstCircle();

        // 3. Wait for a short delay, then show the second circle (dot)
        yield return new WaitForSeconds(0.5f); // Delay between circle 1 and circle 2
        ShowSecondCircle();

        ShowThoughtBubbleWithChips();

        // 18. Wait for 3 seconds, then disable the chips and thought bubble
        yield return new WaitForSeconds(10f);

        if (circle1 != null)
        {
            circle1.SetActive(false); // Disable the first circle
        }
        if (circle2 != null)
        {
            circle2.SetActive(false); // Disable the second circle
        }

        // Disable the thought bubble, chips asset, and chips text
        HideThoughtBubbleAndText();
        if (chipsAsset != null)
        {
            chipsAsset.SetActive(false);  // Hide the chips asset
        }

        if (chipsText != null)
        {
            chipsText.gameObject.SetActive(false);  // Hide the chips text
        }

        yield return new WaitForSeconds(3f);

        // Disable the shadow's spotlight (if you have a reference to it)
        if (shadowSpotlight != null)
        {
            shadowSpotlight.SetActive(false);  // Hide the shadow's spotlight
        }

        // Disable the room light
        if (roomLight != null)
        {
            roomLight.SetActive(false);  // Hide the room light
        }

        // 19. Enable the "Go to grocery store" sign and spotlight
        if (goToGroceryStoreSign != null)
        {
            goToGroceryStoreSign.SetActive(true);  // Show the grocery store sign
        }

        if (spotlightOnGroceryStore != null)
        {
            spotlightOnGroceryStore.SetActive(true);  // Spotlight on the sign
        }

        // Sequence finished (runs only once)
    }

    // Function to show the sparkling lines
    void ShowSparklingLines()
    {
        if (sparklingLines != null)
        {
            sparklingLines.SetActive(true); // Activate the sparkling lines GameObject
        }
    }

    // Function to hide the sparkling lines after the animation ends
    void HideSparklingLines()
    {
        if (sparklingLines != null)
        {
            sparklingLines.SetActive(false); // Deactivate the sparkling lines GameObject
        }
    }

    // Function to show the first circle for the thought bubble animation
    void ShowFirstCircle()
    {
        if (circle1 != null)
        {
            circle1.SetActive(true); // Enable first circle
        }
    }

    // Function to show the second circle for the thought bubble animation
    void ShowSecondCircle()
    {
        if (circle2 != null)
        {
            circle2.SetActive(true); // Enable second circle
        }
    }

    // Function to show the thought bubble after the circles
    void ShowThoughtBubble()
    {
        if (thoughtBubble != null)
        {
            thoughtBubble.SetActive(true); // Show the thought bubble
        }
    }

    // Function to show the thought text inside the thought bubble
    void ShowThoughtText(string message)
    {
        thoughtBubbleText.text = message; // Set the text for the thought bubble
        if (thoughtBubbleText != null)
        {
            thoughtBubbleText.gameObject.SetActive(true); // Enable the text inside the thought bubble
        }
    }

    // Function to hide the thought bubble and text (when switching to the next sequence like text messages)
    void HideThoughtBubbleAndText()
    {
        if (circle1 != null)
        {
            circle1.SetActive(false); // Disable the first circle
        }
        if (circle2 != null)
        {
            circle2.SetActive(false); // Disable the second circle
        }
        if (thoughtBubble != null)
        {
            thoughtBubble.SetActive(false); // Hide the thought bubble
        }
        if (thoughtBubbleText != null)
        {
            thoughtBubbleText.gameObject.SetActive(false); // Hide the text inside the thought bubble
        }
    }

    // Function to show the text message and text bubble
    void ShowTextMessage1(string message)
    {
        if (textMessage1 != null && textBubble != null)
        {
            textMessage1.text = message; // Set the message for the first text
            textMessage1.gameObject.SetActive(true); // Show the first text
            textBubble.SetActive(true); // Show the text bubble
        }
    }

    // Function to display the second text message and hide the first one
    void ShowTextMessage2(string message)
    {
        if (textMessage1 != null)
        {
            textMessage1.gameObject.SetActive(false); // Disable the first message
        }
        if (textMessage2 != null)
        {
            textMessage2.text = message; // Set the message for the second text
            textMessage2.gameObject.SetActive(true); // Enable the second text
        }
    }

    // Function to hide the text and text bubble after the second message
    void HideTextAndBubble()
    {
        if (textMessage1 != null)
        {
            textMessage1.gameObject.SetActive(false); // Disable the first text
        }
        if (textMessage2 != null)
        {
            textMessage2.gameObject.SetActive(false); // Disable the second text
        }
        if (textBubble != null)
        {
            textBubble.SetActive(false); // Hide the text bubble
        }
    }

    // Function to show the thought bubble and the chips asset
    void ShowThoughtBubbleWithChips()
    {

        // Show the thought bubble with chips
        thoughtBubble.SetActive(true);

        // Wait for a slight delay before showing chips (simulating a "thought")
        StartCoroutine(ShowChipsAfterDelay());
    }

    // Coroutine to show the chips asset after a small delay
    IEnumerator ShowChipsAfterDelay()
    {
        // Slight delay to make it feel like a "thought"
        yield return new WaitForSeconds(0.5f);

        if (chipsAsset != null)
        {
            chipsAsset.SetActive(true); // Show the chips asset
            // You can animate the chips asset to float around here, for example:
            // StartCoroutine(FloatChips());
        }

        // After chips appear, show the continuation text
        yield return new WaitForSeconds(2f); // Delay before text appears
        if (chipsText != null)
        {
            chipsText.gameObject.SetActive(true); // Show the text below the chips
        }


        ShowSparklingLines();

        // Change the shadow's face when the chips appear
        ChangeShadowFace();



        // Trigger the arm reaching animation
        TriggerArmMovement();
    }

    // Function to change the shadow's face when the chips show up
    void ChangeShadowFace()
    {
        if (oldShadowFace != null)
        {
            oldShadowFace.SetActive(false); // Disable the old shadow face
        }

        if (newShadowFace != null)
        {
            newShadowFace.SetActive(true); // Enable the new shadow face
        }
    }

    // Function to trigger the shadow's arm movement
    void TriggerArmMovement()
    {
        if (shadowArmAnimator != null)
        {
            shadowArmAnimator.SetTrigger("GrabChips");
        }

        // Stop the sparkling lines after the animation
        StartCoroutine(StopSparklesAfterAnimation());
    }

    // Coroutine to stop the sparkling lines after the arm reaching animation ends
    IEnumerator StopSparklesAfterAnimation()
    {
        // Wait until the grabbing animation ends (assuming it lasts 2 seconds)
        yield return new WaitForSeconds(2f); // Adjust to match the duration of the grabbing animation

        HideSparklingLines(); // Hide the sparkling lines
    }

    // Function to stop both animators (head and shadow)
    void StopAnimators()
    {
        if (headAnimator != null)
        {
            headAnimator.speed = 0f; // Stop the head animation
            headAnimator.enabled = false; // Disable the head animator entirely
        }

        if (shadowAnimator != null)
        {
            shadowAnimator.speed = 0f; // Stop the shadow animation
            shadowAnimator.enabled = false; // Disable the shadow animator entirely
        }
    }

    // Function to resume the animators (head and shadow)
    void ResumeAnimators()
    {
        if (headAnimator != null)
        {
            headAnimator.enabled = true; // Enable the head animator again
            headAnimator.speed = 1f; // Resume the head animation
        }

        if (shadowAnimator != null)
        {
            shadowAnimator.enabled = true; // Enable the shadow animator again
            shadowAnimator.speed = 1f; // Resume the shadow animation
        }
    }

}
