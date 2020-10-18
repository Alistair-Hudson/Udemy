using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] int resourceCost = 0;

    EnemySpawner myLaneSpawner;
    Animator animator;

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private bool IsAttackerInLane()
    {
        if (0 >= myLaneSpawner.transform.childCount)
        {
            return false;
        }
        return true;
    }

    private void SetLaneSpawner()
    {
        EnemySpawner[] attackerSpawners = FindObjectsOfType<EnemySpawner>();

        foreach(EnemySpawner spawner in attackerSpawners)
        {
            bool IsCloseEnough = Mathf.Abs(spawner.transform.position.y - (transform.position.y)) <= Mathf.Epsilon;
            if (IsCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    public void Attack()
    {
        float luanchX = transform.position.x + 0.5f;
        float luanchY = transform.position.y + 0.5f;
        Vector2 luanchPos = new Vector2(luanchX, luanchY);
        var luanchedProjectile = Instantiate(projectile, luanchPos, Quaternion.identity);
    }

    public void AddResources(int amount)
    {
        FindObjectOfType<ResourceDisplay>().AddResources(amount);
    }

    public int GetResourceCost()
    {
        return resourceCost;
    }
}
