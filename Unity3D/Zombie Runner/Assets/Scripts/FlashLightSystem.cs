using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 40f;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    public void RetoreLight(float angle, float intensity)
    {
        myLight.spotAngle = angle;
        myLight.intensity += intensity;
    }

    private void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    private void DecreaseLightIntensity()
    {
        myLight.intensity -= Time.deltaTime * lightDecay;
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minAngle)
        {
            return;
        }
        myLight.spotAngle -= Time.deltaTime * angleDecay;
    }
}
