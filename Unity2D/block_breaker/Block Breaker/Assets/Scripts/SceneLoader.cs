using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene + 1);
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameStatus>().ResetScore();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
