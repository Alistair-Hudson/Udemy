using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fpCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzelFlash;
    [SerializeField] GameObject impactVFX;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        PlayMuzzleFlash();
        ProcessBullet();

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
