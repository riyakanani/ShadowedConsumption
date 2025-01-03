using UnityEngine;

public class FloatingToken : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatAmplitude = 0.5f;


    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time * floatSpeed) * floatAmplitude, 0f);
        transform.Rotate(Vector3.up, 50f * Time.deltaTime);
    }
}

