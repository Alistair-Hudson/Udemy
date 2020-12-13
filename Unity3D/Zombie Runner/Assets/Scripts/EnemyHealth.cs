using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    float bodyRemovalDelay = 5f;
    bool isDead = false;

    //create public method for reducing hit points
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnHit");
        hitPoints -= damage;
        if (0 >= hitPoints && !isDead)
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("TriggerDeath");
            GetComponent<NavMeshAgent>().speed = 0;
            GetComponent<EnemyAI>().enabled = false;
            Destroy(gameObject, bodyRemovalDelay);
        }
    }
}
