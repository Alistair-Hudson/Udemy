using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    //Paramters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int basePointsPerBlock = 83;
    [SerializeField] Text scoreText;
    [SerializeField] bool isAutoPlay;

    // State Variables
    [SerializeField] int gameScore = 0;
    bool hasStarted = false;

    // Create Singleton of Game Status to retain score between levels
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = gameScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText.text = gameScore.ToString();

    }

    public void AddPoints(int points = 50)// Base Points per block
    {
        gameScore += points;
    }

    public int GetScore()
    {
        return gameScore;
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlay()
    {
        return isAutoPlay;
    }

    public bool GetHasStarted()
    {
        return hasStarted;
    }

    public void SetHasStarted(bool state)
    {
        hasStarted = state;
    }
}
