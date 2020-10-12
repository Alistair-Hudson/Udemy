using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //Paramaters
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig _waveConfig)
    {
        waveConfig = _waveConfig;
    }

    private void Move()
    {
        if (waypoints.Count - 1 >= waypointIndex)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position,
                                                     targetPos,
                                                     movementThisFrame);

            if (transform.position == targetPos)
            {
                ++waypointIndex;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
