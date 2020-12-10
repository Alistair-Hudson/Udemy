using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmmount = 5;

    private void OnTriggerEnter(Collider other)
    {
        Ammo ammo = other.GetComponent<Ammo>();
        if (ammo)
        {
            ammo.AddAmmo(ammoType, ammoAmmount);
            Destroy(gameObject);
        }
    }
}
