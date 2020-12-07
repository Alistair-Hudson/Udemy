using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 2;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towerHolder = new Queue<Tower>();

    public void AddTower(Waypoint location)
    {
        if (towerLimit > towerHolder.Count)
        {
            Tower tower = Instantiate(towerPrefab, location.transform.position, Quaternion.identity);
            towerHolder.Enqueue(tower);
            location.isPlaceable = false;
            tower.location = location;
            tower.transform.parent = transform;
        }
        else
        {
            Tower tower = towerHolder.Dequeue();
            tower.location.isPlaceable = true;
            towerHolder.Enqueue(tower);
            tower.transform.position = location.transform.position;
            location.isPlaceable = false;
            tower.location = location;
        }
    }
}
