using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    Text scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = GetComponent<Text>();
        scoreBoard.text = score.ToString();
    }

    public void AddToScore(int points)
    {
        score += points;
        scoreBoard.text = score.ToString();
    }
}
