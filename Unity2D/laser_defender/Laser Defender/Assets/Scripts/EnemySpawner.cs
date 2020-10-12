using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Paramaters
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawAllWaves());
        }while (looping);
    }

    IEnumerator SpawAllWaves()
    {
        for (int waveCount = 0; waveCount < waveConfigs.Count ; ++waveCount)
        {
            StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveCount]));

            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[waveCount])); 
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumEnemy(); ++enemyCount)
        {
            var newEnemy = Instantiate( waveConfig.GetEnemyPrefab(),
                                        waveConfig.GetWaypoints()[0].transform.position,
                                        Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnTime());
        }
    }
}
