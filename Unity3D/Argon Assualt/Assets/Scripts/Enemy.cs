using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int pointsValue = 50;
    [SerializeField] int maxNumHits = 1;
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform fxHolder;


    // Start is called before the first frame update
    void Start()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        --maxNumHits;
        if (0 >= maxNumHits)
        {
            FindObjectOfType<ScoreBoard>().AddToScore(pointsValue);
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = fxHolder;
            Destroy(gameObject);
        }
    }
}
