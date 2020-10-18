using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float timeDelay = 3f;
    int currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (0 == currentScene)
        {
            StartCoroutine(LoadDelay());
        }
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(timeDelay);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(++currentScene);
    }

    public void LoadStartMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }
}
