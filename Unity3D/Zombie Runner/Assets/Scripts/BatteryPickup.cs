using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float angleRestore = 70f;
    [SerializeField] float intensityRestore = 10f;

    private void OnTriggerEnter(Collider other)
    {
        print("Collison with battery");
        FlashLightSystem flashLight = other.GetComponentInChildren<FlashLightSystem>();
        if (flashLight)
        {

            flashLight.RetoreLight(angleRestore, intensityRestore);
            Destroy(gameObject);
        }
    }
}
