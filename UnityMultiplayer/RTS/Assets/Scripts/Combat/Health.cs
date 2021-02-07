using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] int maxHealth = 100;

    [SyncVar(hook = nameof(HandleHealthUpdated))]
    int currentHealth;

    public event Action ServerOnDie;
    public event Action<int, int> ClientOnHealthUpdated;

    #region Server

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
        Base.ServerOnPlayerDie += ServerHandlePlayerDie;
    }

    public override void OnStopServer()
    {
        Base.ServerOnPlayerDie -= ServerHandlePlayerDie;
    }

    [Server]
    public void DamageReceived(int damage)
    {
        if(currentHealth <= 0)
        {
            return;
        }
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            ServerOnDie?.Invoke();
        }
    }

    void ServerHandlePlayerDie(int playerID)
    {
        if(playerID != connectionToClient.connectionId)
        {
            return;
        }

        DamageReceived(currentHealth);
    }

    #endregion

    #region Client

    void HandleHealthUpdated(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdated?.Invoke(newHealth, maxHealth);
    }

    #endregion
}
