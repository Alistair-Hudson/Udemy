﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 500f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (0 >= health)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }
}