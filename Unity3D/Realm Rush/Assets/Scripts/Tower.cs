using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Parameters
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange = 2f;

    //States
    Transform targetEnemy;

    // Update is called once per frame
    void Update()
    {
        if(!targetEnemy)
        {
            SetTarget();
            Shoot(false);
            return;
        }
        objectToPan.LookAt(targetEnemy);
        FireAtEnemy();
    }

    private void SetTarget()
    {
        var seenEnemies = FindObjectsOfType<Damage>();
        if (seenEnemies.Length <= 0)
        {
            return;
        }

        Transform closestEnemy = seenEnemies[0].transform;
        foreach (Damage testEnemy in seenEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        return Vector3.Distance(closestEnemy.position, gameObject.transform.position) <= Vector3.Distance(testEnemy.position, gameObject.transform.position) ? closestEnemy : testEnemy;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, gameObject.transform.position);
        if (Mathf.Abs(distanceToEnemy) <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            targetEnemy = null;
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = GetComponentInChildren<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
}
