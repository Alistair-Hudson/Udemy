using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;

    Dictionary<Vector3, Waypoint> grid = new Dictionary<Vector3, Waypoint>();
    Vector2Int[] directions = {
                                Vector2Int.up,
                                Vector2Int.right,
                                Vector2Int.down,
                                Vector2Int.left
                                };
    Queue<Waypoint> que = new Queue<Waypoint>();
    List<Waypoint> path = new List<Waypoint>();

    private void Pathfind()
    {
        que.Enqueue(startWaypoint);
        startWaypoint.hasBeenSearched = true;

        while (0 < que.Count)
        {
            var searchFrom = que.Dequeue();
            if (endWaypoint == searchFrom)
            {
                BuildPath(searchFrom);
                break;
            }
            ExploreNeighbours(searchFrom);
        }
        if (0 >= que.Count && 0 >= path.Count)
        {
            Debug.LogWarning("No path found");
        }
        
    }

    public List<Waypoint> GetPath()
    {
        if (0 >= path.Count)
        {
            LoadBlocks();
            Pathfind();
            ColorStartEnd();
        }
        return path;
    }

    private void BuildPath(Waypoint searchFrom)
    {
        while (searchFrom)
        {
            searchFrom = AppendToPath(searchFrom);
        }
        path.Reverse();
    }

    private Waypoint AppendToPath(Waypoint searchFrom)
    {
        path.Add(searchFrom);
        searchFrom.SetColor(Color.blue);
        searchFrom.isPlaceable = false;
        return searchFrom.exploredFrom;
    }

    private void ExploreNeighbours(Waypoint searchFrom)
    {
        foreach (Vector2Int direction in directions)
        {
            Vector3 explore = new Vector3((searchFrom.transform.position.x + direction.x),
                                           0,
                                           (searchFrom.transform.position.z + direction.y));
 
            if (grid.ContainsKey(explore) && !grid[explore].hasBeenSearched)
            {
                que.Enqueue(grid[explore]);
                grid[explore].hasBeenSearched = true;
                grid[explore].exploredFrom = searchFrom;
            }

        }
    }

    private void ColorStartEnd()
    {
        startWaypoint.SetColor(Color.red);
        endWaypoint.SetColor(Color.green);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            //check overlapping
            if (!grid.ContainsKey(waypoint.GetGridPos()))
            {
                //add to dictionary
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
            else
            {
                Debug.LogWarning("Overlapping block at " + waypoint);
            }
        }
    }

}
