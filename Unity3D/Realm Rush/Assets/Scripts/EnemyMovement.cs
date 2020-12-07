using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveDelay = 1f;
    [SerializeField] int damage = 5;

    List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        path = FindObjectOfType<Pathfinder>().GetPath();
        StartCoroutine(FollowWaypoints());
    }

    IEnumerator FollowWaypoints()
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(moveDelay);
        }
        FindObjectOfType<BaseHealth>().DamageBase(damage);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
