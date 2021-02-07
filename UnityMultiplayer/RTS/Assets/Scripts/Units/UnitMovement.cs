using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Targeter targeter;
    [SerializeField] float chaseRange = 0f;

    float error = 1f;

    #region Server

    public override void OnStartServer()
    {
        GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    public override void OnStopServer()
    {
        GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
    }

    [ServerCallback]
    private void Update()
    {
        Targetable target = targeter.GetTarget();
        if(target != null)
        {
            if ((target.transform.position - transform.position).sqrMagnitude > chaseRange*chaseRange)
            {
                agent.SetDestination(target.transform.position);
            }
            else if (agent.hasPath)
            {
                agent.ResetPath();
            }
            return;
        }

        if(!agent.hasPath)
        {
            return;
        }
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }
        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        targeter.ClearTarget();
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, error, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(position);
    }

    [Server]
    void ServerHandleGameOver()
    {
        agent.ResetPath();
    }

    #endregion

    #region Client

    #endregion
}
