using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = new LayerMask();
    [SerializeField] RectTransform unitSelectionArea = null;

    Vector2 startPosition;

    Camera mainCamera;
    RTSPlayer player;

    public List<Unit> selectedUnits { get; } = new List<Unit>();

    private void Start()
    {
        mainCamera = Camera.main;
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawn;
        GameOverHandler.ClientOnGameOver += ClientHandleGameOver;
    }


    private void OnDestroy()
    {
        Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawn;
        GameOverHandler.ClientOnGameOver -= ClientHandleGameOver;
    }
    private void Update()
    {
        if(player == null)
        {
            player = NetworkClient.connection.identity.GetComponent<RTSPlayer>();

        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            UpdateSelectionArea();
        }
    }

    private void StartSelectionArea()
    {
        if (!Keyboard.current.leftShiftKey.isPressed)
        {
            foreach (Unit selectedUnit in selectedUnits)
            {
                selectedUnit.Deselect();
            }
            selectedUnits.Clear();
        }

        unitSelectionArea.gameObject.SetActive(true);
        startPosition = Mouse.current.position.ReadValue();

        UpdateSelectionArea();
    }

    private void UpdateSelectionArea()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        float areaWidth = mousePos.x - startPosition.x;
        float areaHeight = mousePos.y - startPosition.y;

        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        unitSelectionArea.anchoredPosition = startPosition + 
                                                new Vector2(areaWidth / 2, areaHeight / 2);
    }

    private void ClearSelectionArea()
    {
        unitSelectionArea.gameObject.SetActive(false);
        if(unitSelectionArea.sizeDelta.magnitude == 0)
        {

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                return;
            }
            if(!hit.collider.TryGetComponent<Unit>(out Unit unit))
            {
                return;
            }
            if (!unit.hasAuthority)
            {
                return;
            }

            selectedUnits.Add(unit);

            foreach (Unit selectedUnit in selectedUnits)
            {
                selectedUnit.Select();
            }

            return;
        }

        Vector2 min = unitSelectionArea.anchoredPosition - (unitSelectionArea.sizeDelta / 2);
        Vector2 max = unitSelectionArea.anchoredPosition + (unitSelectionArea.sizeDelta / 2);

        foreach(Unit unit in player.GetMyUnits())
        {
            if (selectedUnits.Contains(unit))
            {
                continue;
            }

            Vector3 screenPos = mainCamera.WorldToScreenPoint(unit.transform.position);

            if(screenPos.x > min.x && screenPos.x < max.x &&
                screenPos.y > min.y && screenPos.y < max.y)
            {
                selectedUnits.Add(unit);
                unit.Select();
            }
        }
    }

    private void AuthorityHandleUnitDespawn(Unit unit)
    {
        selectedUnits.Remove(unit);
    }

    void ClientHandleGameOver(string winnerName)
    {
        enabled = false;
    }
}

