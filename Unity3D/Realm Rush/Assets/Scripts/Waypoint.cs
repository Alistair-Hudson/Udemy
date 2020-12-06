using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] Tower towerPrefab;

    Vector3 gridPos;
    public bool hasBeenSearched = false;
    public Waypoint exploredFrom;
    public bool isPlaceable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetGridPos()
    {
        return new Vector3 (
                            Mathf.RoundToInt(transform.position.x / transform.lossyScale.x) * transform.lossyScale.x,
                            Mathf.RoundToInt(transform.position.y / transform.lossyScale.y) * transform.lossyScale.y,
                            Mathf.RoundToInt(transform.position.z / transform.lossyScale.z) * transform.lossyScale.z
                            );
    }

    public void SetColor(Color color)
    {
        MeshRenderer meshRenderer =  GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
