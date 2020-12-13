using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{

    Ammo ammo;
    Text ammoDisplay;

    void Start()
    {
        ammo = FindObjectOfType<Ammo>();
        ammoDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = ammo.GetAmmoAmount(ammo.GetActiveAmmo()).ToString();
    }
}
