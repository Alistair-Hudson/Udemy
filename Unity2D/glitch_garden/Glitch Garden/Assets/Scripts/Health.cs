using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int reward = 10;

    public void DealDamage(int damage)
    {
        health -= damage;
        if (0 >= health)
        {
            FindObjectOfType<ResourceDisplay>().AddResources(reward);
            Destroy(gameObject);
        }
    }
}
