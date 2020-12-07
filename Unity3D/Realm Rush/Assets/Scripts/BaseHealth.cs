using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip damagedSFX;

    private void Start()
    {
        healthText.text = health.ToString();
    }

    public void DamageBase(int damage)
    {
        health -= damage;
        healthText.text = health.ToString();
        GetComponent<AudioSource>().PlayOneShot(damagedSFX);
        if (0 >= health)
        {
            print("GAME OVER");
        }
    }
}
