using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartHealth : MonoBehaviour
{
    void Start()
    {
        Health.persistentHealth = -1f; // Reset health at the start of a new run
    }
}
