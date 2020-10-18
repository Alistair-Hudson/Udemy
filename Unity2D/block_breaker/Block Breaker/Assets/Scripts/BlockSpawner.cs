using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] Block[] blocks;
    [SerializeField] float maxPlayAreaWidth = 16f;
    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 10f;
    [SerializeField] float blockVelocity = 5f;
    [SerializeField] float difficultyFactor = 1000f;

    GameStatus gameStatus;

    IEnumerator Start()
    {
        Debug.Log(Random.Range(5, 2));
        gameStatus = FindObjectOfType<GameStatus>();

        float xPos = transform.position.x + Random.Range(1f, maxPlayAreaWidth - 1f);
        float yPos = transform.position.y;
        Vector2 spawnPos = new Vector2(xPos, yPos);
        Block firstBlock = Instantiate(blocks[0], spawnPos, Quaternion.identity);
        firstBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -blockVelocity);
        firstBlock.transform.parent = transform;

        while (true)
        {
            
            yield return new WaitForSeconds(Random.Range(minSpawnTime, MaxSpwanTimeCal()));
            xPos = transform.position.x + Random.Range(1f, maxPlayAreaWidth-1f);
            yPos = transform.position.y;
            spawnPos = new Vector2(xPos, yPos);
            Block newBlock = Instantiate(blocks[Random.Range(0, blocks.Length)], spawnPos, Quaternion.identity);
            if ("Breakable" == newBlock.tag)
            {
                newBlock.GetComponent<SpriteRenderer>().color = new Color32((byte)Mathf.RoundToInt(Random.Range(0f, 256f)),
                                                                            (byte)Mathf.RoundToInt(Random.Range(0f, 256f)),
                                                                            (byte)Mathf.RoundToInt(Random.Range(0f, 256f)),
                                                                            255);
            }
            newBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -(blockVelocity + (gameStatus.GetScore()/difficultyFactor)));
            newBlock.transform.parent = transform;
        }
    }

    private float MaxSpwanTimeCal()
    {
        float newMax = maxSpawnTime - (gameStatus.GetScore() / difficultyFactor);
        if (minSpawnTime >= newMax)
        {
            return minSpawnTime;
        }
        return newMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
