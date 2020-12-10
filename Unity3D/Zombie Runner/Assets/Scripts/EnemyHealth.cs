using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    //create public method for reducing hit points
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnHit");
        hitPoints -= damage;
        if (0 >= hitPoints)
        {
            GetComponent<Animator>().SetTrigger("TriggerDeath");
            Destroy(gameObject);
        }
    }
}
