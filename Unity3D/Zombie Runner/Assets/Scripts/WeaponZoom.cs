using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    Camera fpCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    bool isZoomedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        fpCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (isZoomedIn)
            {
                fpCamera.fieldOfView = zoomedOutFOV;
                isZoomedIn = false;
            }
            else
            {
                fpCamera.fieldOfView = zoomedInFOV;
                isZoomedIn = true;
            }
        }
    }

}
