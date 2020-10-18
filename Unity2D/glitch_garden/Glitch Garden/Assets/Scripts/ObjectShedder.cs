using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShedder : MonoBehaviour
{
    HealthDisplay healthDisplay;
    LevelController levelController;

    public void Start()
    {
        healthDisplay = FindObjectOfType<HealthDisplay>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Attacker>())
        {
            healthDisplay.DamageHealth();
        }
        Destroy(collision.gameObject);
        
    }
}
