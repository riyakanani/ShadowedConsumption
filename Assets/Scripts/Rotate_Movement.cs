using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Movement : MonoBehaviour
{
    //[SerializeField] private Vector3 rotation = new Vector3(0, 1, 0);
    [SerializeField] private float rotationSpeed = 45f; // Speed of rotation
    [SerializeField] private float verticalSpeed = 2f;  // Speed of up-and-down movement
    [SerializeField] private float verticalAmplitude = 1f; // Amplitude of the up-and-down movement

    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Rotate around the Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Move up and down using a sine wave
        float newY = initialPosition.y + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);

        //this.transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
