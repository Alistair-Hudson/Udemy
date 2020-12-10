using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetActiveWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWeapon();
        if (previousWeapon != currentWeapon)
        {
            SetActiveWeapon();
        }
    }

    private void ProcessScrollWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 )
        {
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                ++currentWeapon;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount -1;
            }
            else
            {
                --currentWeapon;
            }
        }

    }

    private void ProcessKeyInput()
    {
        
    }

    private void SetActiveWeapon()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            ++weaponIndex;
        }
    }

}
