using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        Debug.Log("I connected to a server");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        conn.identity.GetComponent<MyNetworkPlayer>().SetDisplayName($"Player {numPlayers}");
        conn.identity.GetComponent<MyNetworkPlayer>().SetPlayerColor(new Color(Random.Range(0f, 1f),
                                                                               Random.Range(0f, 1f),
                                                                               Random.Range(0f, 1f)));

    }
}
