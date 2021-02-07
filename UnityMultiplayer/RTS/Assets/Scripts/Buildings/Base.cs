using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : NetworkBehaviour
{
    [SerializeField] Health health = null;

    public static event Action<int> ServerOnPlayerDie;
    public static event Action<Base> ServerOnBaseSpawned;
    public static event Action<Base> ServerOnBaseDespawned;

    #region Server

    public override void OnStartServer()
    {
        health.ServerOnDie += ServerHandleDie;

        ServerOnBaseSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnBaseDespawned?.Invoke(this);

        health.ServerOnDie += ServerHandleDie;

    }

    [Server]
    void ServerHandleDie()
    {
        ServerOnPlayerDie?.Invoke(connectionToClient.connectionId);

        NetworkServer.Destroy(gameObject);
    }

    #endregion

    #region Client

    #endregion
}
