using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float gameOverDelay = 1f;
    int currentLevel = 0;

    public void LoadGameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    public void LoadGameScene()
    {
        ++currentLevel;
        SceneManager.LoadScene(currentLevel);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        currentLevel = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOver");
    }
}
