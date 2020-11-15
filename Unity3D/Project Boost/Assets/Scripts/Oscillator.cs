using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;

    Vector3 startPos;
    float movementFactor;
    const float singleCycle = Mathf.PI * 2;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Epsilon >= period)
        {
            return;
        }

        float cycles = Time.time / period;

        movementFactor = Mathf.Sin(singleCycle * cycles) /2 +0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
