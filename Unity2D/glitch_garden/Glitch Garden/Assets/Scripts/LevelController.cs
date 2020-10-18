using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabel;
    [SerializeField] float waitToLoad = 4f;

    int numAttackers = 0;
    bool hasTimerFinished = false;

    private void Start()
    {
        winLabel.SetActive(false);
        loseLabel.SetActive(false);
    }

    public void AddAttacker()
    {
        ++numAttackers;
    }

    public void SubAttacker()
    {
        --numAttackers;
        if (0 >= numAttackers && hasTimerFinished)
        {
            StartCoroutine(HandleWin());
        }
    }

    public void SetTimerFinished()
    {
        hasTimerFinished = true;
    }

    public void ResetTimerFinished()
    {
        hasTimerFinished = false;    
    }

    IEnumerator HandleWin()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        FindObjectOfType<LevelLoader>().LoadNextScene();
    }

    public void LoadLose()
    {
        loseLabel.SetActive(true);
        Time.timeScale = 0;
    }
    
}
