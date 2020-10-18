using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 5f;
    [SerializeField] Attacker[] enemyTypes;

    bool spawn = true;
    LevelController levelController;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(maxSpawnTime / PlayerPrefsController.GetDifficulty(), maxSpawnTime));
            float xPos = transform.position.x;
            float yPos = transform.position.y + 1f;
            Vector2 spawnPos = new Vector2(xPos, yPos);
            Attacker newAttacker = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPos, Quaternion.identity);
            newAttacker.transform.parent = transform;
        }
    }
    
    public void StopSpawning()
    {
        spawn = false;
    }

}
