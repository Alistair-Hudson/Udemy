using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    void Update()
    {
        SnapToGrid();
        UpdateLable();

    }

    private void SnapToGrid()
    { 
        Waypoint waypoint = GetComponent<Waypoint>();
        transform.position = new Vector3(
                                         waypoint.GetGridPos().x, 
                                         waypoint.GetGridPos().y, 
                                         waypoint.GetGridPos().z
                                         );
    }

    private void UpdateLable()
    {
        Waypoint waypoint = GetComponent<Waypoint>();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string coordinateLabel = waypoint.GetGridPos().x / transform.lossyScale.x + "," + waypoint.GetGridPos().z / transform.lossyScale.z;
        textMesh.text = coordinateLabel;
        gameObject.name = coordinateLabel;
    }
}
