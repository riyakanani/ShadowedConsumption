//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Rotate_Movement : MonoBehaviour
//{
//    //[SerializeField] private Vector3 rotation = new Vector3(0, 1, 0);
//    [SerializeField] private float rotationSpeed = 45f; // Speed of rotation
//    [SerializeField] private float verticalSpeed = 2f;  // Speed of up-and-down movement
//    [SerializeField] private float verticalAmplitude = 1f; // Amplitude of the up-and-down movement

//    private Vector3 initialPosition;

//    void Start()
//    {
//        // Save the initial position of the object
//        initialPosition = transform.position;
//    }

//    void Update()
//    {
//        // Rotate around the Y-axis
//        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

//        // Move up and down using a sine wave
//        float newY = initialPosition.y + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;
//        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

//        //this.transform.Rotate(rotation * speed * Time.deltaTime);
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotate_Movement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f; // Speed of rotation
    [SerializeField] private float verticalSpeed = 2f;  // Speed of up-and-down movement
    [SerializeField] private float verticalAmplitude = 1f; // Amplitude of the up-and-down movement
    [SerializeField] private float moveAwaySpeed = 5f;   // Very fast speed at which the token moves away from the player initially
    [SerializeField] private float movementDelay = 0.5f; // Delay before the token starts moving away
    [SerializeField] private float collectDistance = 1.5f; // Distance at which the token can be collected

    [SerializeField] private GameObject player; // Reference to the player

    private Vector3 initialPosition; // Store the initial position
    private bool startMoving = false; // Flag to start moving
    private Vector3 currentPosition; // Current position of the token

    void Start()
    {
        // Ensure player is set before start (automatic if you don't assign manually)
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        // Store the initial position of the object
        initialPosition = transform.position;
        currentPosition = initialPosition; // Start at the initial position

        // Start movement after a delay
        StartCoroutine(StartMovementCoroutine());
    }

    void Update()
    {
        // Rotate the token around the Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Move up and down using a sine wave
        float newY = initialPosition.y + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

        // Move away fast at the beginning
        if (startMoving)
        {
            // Initially zoom away very fast
            currentPosition.x += moveAwaySpeed * Time.deltaTime; // Fast zoom away
            transform.position = currentPosition;
        }

        // Check for token collection only when the player is within collectDistance
        CheckTokenCollection();
    }

    // Coroutine to start moving after a delay
    private IEnumerator StartMovementCoroutine()
    {
        yield return new WaitForSeconds(movementDelay);
        startMoving = true; // Begin moving after the delay
    }

    // Checks if the player is within collection distance and triggers collection
    private void CheckTokenCollection()
    {
        // Get the player's position and distance to the token
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= collectDistance)
        {
            // Trigger token collection only if the player is close enough
            CollectToken();
        }
    }

    // Logic to handle token collection (e.g., disable or destroy it)
    private void CollectToken()
    {
        Debug.Log("Token collected!");
        gameObject.SetActive(false); // Example action: disable the token after collection
    }
}

