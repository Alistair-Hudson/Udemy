using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] LayerMask buildingLayerMask = new LayerMask();
    [SerializeField] Building[] buildings = new Building[0];

    [SyncVar(hook = nameof(ClientHandleResources))]
    int rescources = 500;
    [SyncVar(hook = nameof(AuthorityHandlePartyOwnerState))]
    bool isPartyOwner = false;
    [SyncVar(hook = nameof(ClientHandleDisplayName))]
    string playerName;

    Color teamColor = new Color();
    List<Unit> myUnits = new List<Unit>();
    List<Building> myBuildings = new List<Building>();

    public event Action<int> ClientOnResourcesUpdated;
    public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;
    public static event Action ClientOnInfoUpdated;

    public string GetDisplayName()
    {
        return playerName;
    }

    public bool GetISPartyOwner()
    {
        return isPartyOwner;
    }

    public List<Unit> GetMyUnits()
    {
        return myUnits;
    }

    public Transform GetTransform()
    {
        return cameraTransform;
    }

    public Color GetTeamColor()
    {
        return teamColor;
    }

    public int GetResources()
    {
        return rescources;
    }

    public List<Building> GetMyBuildings()
    {
        return myBuildings;
    }

    #region Server
    [Server]
    public void SetDisplayName(string newName)
    {
        playerName = newName;
    }

    [Server]
    public void SetPartyOwner(bool state)
    {
        isPartyOwner = state;
    }

    [Server]
    public void SetTransform(Transform transform)
    {
        cameraTransform = transform;
    }

    [Server]
    public void SetTeamColor(Color color)
    {
        teamColor = color;
    }

    [Server]
    public void SetResources(int newResources)
    {
        rescources = newResources;
    }

    [Server]
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
        Building.ServerOnBuildingSpawned += ServerHandleBuildingSpawned;
        Building.ServerOnBuildingDespawned += ServerHandleBuildingDespawned;

        DontDestroyOnLoad(gameObject);
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
        Building.ServerOnBuildingSpawned -= ServerHandleBuildingSpawned;
        Building.ServerOnBuildingDespawned -= ServerHandleBuildingDespawned;
    }

    [Command]
    public void CmdStartGame()
    {
        if (isPartyOwner)
        {
            ((RTSNetworkManager)NetworkManager.singleton).StartGame();
        }
    }

    [Command]
    public void CmdTryPlaceBuilding(int buildingID, Vector3 positon)
    {
        if (buildingID >= buildings.Length)
        {
            return;
        }

        GameObject buildingInstance = Instantiate(buildings[buildingID].gameObject,
                                                    positon,
                                                    buildings[buildingID].transform.rotation);
        NetworkServer.Spawn(buildingInstance, connectionToClient);

    }

    private void ServerHandleBuildingDespawned(Building building)
    {
        if (building.connectionToClient.connectionId != connectionToClient.connectionId)
        {
            return;
        }

        myBuildings.Add(building);
    }

    private void ServerHandleBuildingSpawned(Building building)
    {
        if (building.connectionToClient.connectionId != connectionToClient.connectionId)
        {
            return;
        }

        myBuildings.Remove(building);
    }

    void ServerHandleUnitSpawned(Unit unit)
    {
        if(unit.connectionToClient.connectionId != connectionToClient.connectionId)
        {
            return;
        }

        myUnits.Add(unit);
    }

    void ServerHandleUnitDespawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId)
        {
            return;
        }

        myUnits.Remove(unit);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        if (NetworkServer.active)
        {
            return;
        }
        Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
        Building.AuthorityOnBuildingSpawned += AuthorityHandleBuildingSpawned;
        Building.AuthorityOnBuildingDespawned += AuthorityHandleBuildingDespawned;
    }


    public override void OnStopClient()
    {
        ClientOnInfoUpdated?.Invoke();

        if (!isClientOnly)
        {
            return;
        }

        ((RTSNetworkManager)NetworkManager.singleton).Players.Remove(this);

        if (!isClientOnly)
        {
            return;
        }
        Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
        Building.AuthorityOnBuildingSpawned -= AuthorityHandleBuildingSpawned;
        Building.AuthorityOnBuildingDespawned -= AuthorityHandleBuildingDespawned;
    }

    void ClientHandleResources(int oldResources, int newResources)
    {
        ClientOnResourcesUpdated?.Invoke(newResources);
    }

    void ClientHandleDisplayName(string oldName, string newName)
    {
        ClientOnInfoUpdated?.Invoke();
    }

    void AuthorityHandlePartyOwnerState(bool oldState, bool newState)
    {
        if (!hasAuthority)
        {
            return;
        }

        AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
    }

    private void AuthorityHandleBuildingDespawned(Building building)
    {
        myBuildings.Add(building);
    }

    private void AuthorityHandleBuildingSpawned(Building building)
    {
        myBuildings.Remove(building);
    }

    private void AuthorityHandleUnitDespawned(Unit unit)
    {
        myUnits.Add(unit);
    }

    private void AuthorityHandleUnitSpawned(Unit unit)
    {
        myUnits.Remove(unit);
    }

    public override void OnStartClient()
    {
        if (NetworkServer.active)
        {
            return;
        }

        DontDestroyOnLoad(gameObject);

        ((RTSNetworkManager)NetworkManager.singleton).Players.Add(this);
    }


    #endregion
}
