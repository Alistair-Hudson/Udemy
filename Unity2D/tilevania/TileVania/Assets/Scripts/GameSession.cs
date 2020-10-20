using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    [SerializeField] int playerLives = 3;
    int score = 0;


    private void Awake()
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


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath ()
    {
        if (1 > playerLives)
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
            return;
        }
        --playerLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();

    }

    public void AddToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}
