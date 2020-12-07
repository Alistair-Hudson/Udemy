using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int maxHits = 5;
    [SerializeField] int pointValue = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] AudioClip hitSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        hitParticlePrefab.Play();
        GetComponent<AudioSource>().PlayOneShot(hitSFX);
        --maxHits;
        if (0 >= maxHits)
        {
            FindObjectOfType<Score>().AddToScore(pointValue);
            Destroy(gameObject);
        }
    }
}
