using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")]
    [SerializeField] float speed = 5f;
    [Tooltip("In m")]
    [SerializeField] float xRange = 5f;
    [Tooltip("In m")]
    [SerializeField] float yRange = 5f;

    [SerializeField] float pitchFactor = -5f;
    [SerializeField] float yawFactor = 3f;

    [SerializeField] float pitchControl = -10f;
    [SerializeField] float rollControl = -30f;

    float xThrow;
    float yThrow;

    //Constants
    string HORIZONTAL_AXIS = "Horizontal";
    string VERTICAL_AXIS = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * pitchFactor + yThrow * pitchControl;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xThrow * rollControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis(HORIZONTAL_AXIS);
        yThrow = CrossPlatformInputManager.GetAxis(VERTICAL_AXIS);

        float xOffset = xThrow * speed * Time.deltaTime;
        float yOffset = yThrow * speed * Time.deltaTime;

        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange),
                                                Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange),
                                                transform.localPosition.z);
    }
}
