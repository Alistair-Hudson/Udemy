using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Level time in seconds")]
    [SerializeField] float levelTime = 10f;
    float elapasedTime = 0f;
    bool levelFinished = false;

    // Update is called once per frame
    void Update()
    {
        if (levelFinished)
        {
            return;
        }

        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;
        
        if (Time.timeSinceLevelLoad >= levelTime)
        {
            FindObjectOfType<LevelController>().SetTimerFinished();
            levelFinished = true;
            StopSpawners();
        }
    }

    private void StopSpawners()
    {
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach(EnemySpawner spawner in spawners)
        {
            spawner.StopSpawning();
        }
    }
}
