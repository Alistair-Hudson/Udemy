using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviour
{

    [SerializeField] GameObject lobbyUI;
    [SerializeField] Button startButton;
    [SerializeField] TMP_Text[] playerNameTexts = new TMP_Text[4];

    private void Start()
    {
        RTSNetworkManager.ClientOnConnected += HandleClientConnected;
        RTSPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        RTSPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
    }

    private void OnDestroy()
    {
        RTSNetworkManager.ClientOnConnected -= HandleClientConnected;
        RTSPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        RTSPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
    }

    void HandleClientConnected()
    {
        lobbyUI.SetActive(true);
    }

    void AuthorityHandlePartyOwnerStateUpdated(bool state)
    {
        startButton.gameObject.SetActive(state);
    }

    void ClientHandleInfoUpdated()
    {
        List<RTSPlayer> players = ((RTSNetworkManager)NetworkManager.singleton).Players;

        for (int i = 0; i < players.Count; ++i)
        {
            playerNameTexts[i].text = players[i].GetDisplayName();
        }

        for (int i = players.Count; i < playerNameTexts.Length; ++i)
        {
            playerNameTexts[i].text = "Waiting for player...";
        }

        startButton.interactable = players.Count >= 2;
    }

    public void StartGame()
    {
        NetworkClient.connection.identity.GetComponent<RTSPlayer>().CmdStartGame();
    }

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
