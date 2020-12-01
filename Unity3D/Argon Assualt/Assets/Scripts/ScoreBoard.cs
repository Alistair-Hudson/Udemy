using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    Text scoreBoard;
    float timeUntilPoint;
    [SerializeField] float timePerPoint = 1f;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilPoint = timePerPoint;

        scoreBoard = GetComponent<Text>();
        scoreBoard.text = score.ToString();
    }

    private void Update()
    {
        timeUntilPoint -= Time.deltaTime;
        if (0 >= timeUntilPoint)
        {
            AddToScore(1);
            timeUntilPoint = timePerPoint;
        }

        
    }

    public void AddToScore(int points)
    {
        score += points;
        scoreBoard.text = score.ToString();
    }
}
