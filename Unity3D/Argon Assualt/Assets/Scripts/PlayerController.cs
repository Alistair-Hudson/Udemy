using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
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

    [SerializeField] GameObject[] guns;
    float xThrow;
    float yThrow;
    bool isControlEnabled = true;

    //Constants
    string HORIZONTAL_AXIS = "Horizontal";
    string VERTICAL_AXIS = "Vertical";
    string SHOOT = "Fire1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessShooting();
        }

    }

    private void ProcessShooting()
    {
        if (CrossPlatformInputManager.GetButton(SHOOT))
        {
            SetGunState(true);
        }
        else
        {
            SetGunState(false);
        }
    }

    private void SetGunState(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
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

    void ControlFreeze()//called by SendMessage
    {
        isControlEnabled = false;
    }
}
