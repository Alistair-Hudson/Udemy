using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int maxHits = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        --maxHits;
        if (0 >= maxHits)
        {
            Destroy(gameObject);
        }
    }
}
