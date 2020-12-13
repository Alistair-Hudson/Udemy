using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] ParticleSystem muzzelFlash;
    [SerializeField] GameObject impactVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] float shotDelay = 1f;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
        ammoSlot.SetActiveAmmo(ammoType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (0 < ammoSlot.GetAmmoAmount(ammoType))
        {
            ammoSlot.ReduceAmmo(ammoType);
            PlayMuzzleFlash();
            ProcessBullet();
        }

        yield return new WaitForSeconds(shotDelay);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzelFlash.Play();
    }

    private void ProcessBullet()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, range))
        {
            var hitVFX = Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitVFX, 0.1f);
            try
            {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                target.TakeDamage(damage);
            }
            catch
            {
                //do nothing
            }
        }
    }
}
