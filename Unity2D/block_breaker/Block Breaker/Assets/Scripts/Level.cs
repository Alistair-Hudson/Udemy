using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Paramters
    [SerializeField] int breakablBlocks; // Serialized for debugging

    //Cached Data
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        ++breakablBlocks;
    }

    public void DecreaseBlocks()
    {
        --breakablBlocks;
        
        if (0 >= breakablBlocks)
        {
            // sceneLoader.LoadNextScene();
        }
    }
}
