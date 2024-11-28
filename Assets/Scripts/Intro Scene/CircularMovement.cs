using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private Transform rotateAround; // Add the type 'Transform' here

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(rotateAround.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
