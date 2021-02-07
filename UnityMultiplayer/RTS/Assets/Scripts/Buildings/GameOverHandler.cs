using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : NetworkBehaviour
{

    List<Base> bases = new List<Base>();

    public static event Action ServerOnGameOver;
    public static event Action<String> ClientOnGameOver;


    #region Server

    public override void OnStartServer()
    {
        Base.ServerOnBaseSpawned += ServerHandleBaseSpawned;
        Base.ServerOnBaseDespawned += ServerHandleBaseDespawned;
    }

    public override void OnStopServer()
    {
        Base.ServerOnBaseSpawned -= ServerHandleBaseSpawned;
        Base.ServerOnBaseDespawned -= ServerHandleBaseDespawned;
    }

    [Server]
    void ServerHandleBaseSpawned(Base mybase)
    {
        bases.Add(mybase);
    }

    [Server]
    void ServerHandleBaseDespawned(Base mybase)
    {
        bases.Remove(mybase);
        if (bases.Count != 1)
        {
            return;
        }

        int playerID = bases[0].connectionToClient.connectionId;

        RpcGameOver($"Player {playerID}");
        ServerOnGameOver?.Invoke();
    }

    #endregion

    #region Client
    [ClientRpc]
    void RpcGameOver(string winner)
    {
        ClientOnGameOver?.Invoke(winner);
    }
    #endregion
}
