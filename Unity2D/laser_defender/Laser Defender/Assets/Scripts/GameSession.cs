using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;

    void Awake()
    {
        if (1 < FindObjectsOfType<GameSession>().Length)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
