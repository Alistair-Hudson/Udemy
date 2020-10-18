using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    Defender defenders;

    private void OnMouseDown()
    {
        Atempt2Place(GetSquareClicked());
    }

    private Vector2 Snap2Grid (Vector2 rawWorldPos)
    {
        float snapX = Mathf.Floor(rawWorldPos.x);
        float snapY = Mathf.Floor(rawWorldPos.y);
        return new Vector2(snapX, snapY);
    }
    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return Snap2Grid(Camera.main.ScreenToWorldPoint(clickPos));
    }

    public void SetSelcetedDefender(Defender selectedDefender)
    {
        defenders = selectedDefender;
    }

    private void Atempt2Place(Vector2 gridPos)
    {
        var resourceDisplay = FindObjectOfType<ResourceDisplay>();
        int cost = defenders.GetResourceCost();
        if (resourceDisplay.HaveEnough(cost))
        {
            Instantiate(defenders, gridPos, Quaternion.identity);
            resourceDisplay.SpendResources(cost);
        }
    }
}
