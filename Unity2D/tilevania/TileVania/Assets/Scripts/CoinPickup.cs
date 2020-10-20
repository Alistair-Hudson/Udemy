using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip sfx;
    [SerializeField] int points = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(points);
        AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);   
        Destroy(gameObject);
    }
}
