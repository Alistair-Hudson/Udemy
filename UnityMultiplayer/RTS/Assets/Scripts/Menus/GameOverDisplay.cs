using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] GameObject gameOverDisplayParent = null;
    [SerializeField] TMP_Text winnerNameText = null;

    // Start is called before the first frame update
    void Start()
    {
        GameOverHandler.ClientOnGameOver += ClientHandleGameOver;
    }

    private void OnDestroy()
    {
        GameOverHandler.ClientOnGameOver -= ClientHandleGameOver;
    }

    public void LeaveGame()
    {
        if(NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
    }

    void ClientHandleGameOver(string winner)
    {
        winnerNameText.text = $"{winner} is Victorious";
        gameOverDisplayParent.SetActive(true);
    }
}
