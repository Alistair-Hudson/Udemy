using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Paramters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockBreakVFX;
    [SerializeField] float vfxTime = 2f;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    //Cached data
    Level level;
    GameStatus gStatus;

    // State variables
    [SerializeField] int timesHit; // Searialized for debugging
    int spriteIndex = 0;

    private void Start()
    {
        maxHits = hitSprites.Length;
        level = FindObjectOfType<Level>();
        if ("Breakable" == tag)
        {
            level.CountBreakableBlocks();
        }
        gStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        if ("Breakable" == tag)
        {
            --maxHits;
            if (0 >= maxHits)
            {
                BreakVFX();
                gStatus.AddPoints();
                level.DecreaseBlocks();
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
