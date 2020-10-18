using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneMangager

public class Block : MonoBehaviour
{
    // Paramters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockBreakVFX;
    [SerializeField] float vfxTime = 2f;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] int points = 50;
    int maxHits;

    //Cached data
    GameStatus gStatus;

    // State variables
    [SerializeField] int timesHit; // Searialized for debugging
    int spriteIndex = 0;

    private void Start()
    {
        maxHits = hitSprites.Length;
        gStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Paddle>() || !gStatus.GetHasStarted())
        {
            SceneManager.LoadScene("GameOverScene");
        }
        
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        if ("Breakable" == tag)
        {
            --maxHits;
            if (0 >= maxHits)
            {
                BreakVFX();
                gStatus.AddPoints(points);
                Destroy(gameObject);
            }
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[++spriteIndex];
    }

    private void BreakVFX()
    {
        GameObject sparkles = Instantiate(blockBreakVFX, transform.position, transform.rotation);
        Destroy(sparkles, vfxTime);
    }
}
