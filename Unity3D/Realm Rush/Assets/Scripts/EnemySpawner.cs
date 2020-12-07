using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] AudioClip spawnSFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            var newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            GetComponent<AudioSource>().PlayOneShot(spawnSFX);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
