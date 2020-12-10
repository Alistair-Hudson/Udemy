using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    Camera fpCamera;
    RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] float zoomedInSensitivity = 0.5f;

    bool isZoomedIn = false;

    // Start is called before the first frame update
    void Start()
    {
        fpCamera = GetComponentInParent<Camera>();
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (isZoomedIn)
            {
                ChangeZoomControlls(zoomedOutFOV, zoomedOutSensitivity);
            }
            else
            {
                ChangeZoomControlls(zoomedInFOV, zoomedInSensitivity);
            }
        }
    }

    private void ChangeZoomControlls(float fov, float senisitivity)
    {
        fpCamera.fieldOfView = fov;
        fpsController.mouseLook.XSensitivity = senisitivity;
        fpsController.mouseLook.YSensitivity = senisitivity;
        isZoomedIn ^= true;
    }

    private void OnDisable()
    {
        fpCamera.fieldOfView = zoomedOutFOV;
        fpsController.mouseLook.XSensitivity = zoomedOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomedOutSensitivity;
        isZoomedIn = false;
    }
}
