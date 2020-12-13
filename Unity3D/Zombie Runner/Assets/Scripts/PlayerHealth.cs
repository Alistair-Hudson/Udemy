using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 500f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(ShowHit());
        if (0 >= health)
        {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }

    public float GetHealth()
    {
        return health;
    }

    IEnumerator ShowHit()
    {
        DisplayDamage displayDamage = FindObjectOfType<DisplayDamage>();
        displayDamage.ShowSplatter(true);
        yield return new WaitForSeconds(1f);
        displayDamage.ShowSplatter(false);


    }
}
