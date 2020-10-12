using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] bool doesMusicChange = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (!doesMusicChange)
        {
            SetUpSingleton();
        }
    }

    private void SetUpSingleton()
    {
        if (1 < FindObjectsOfType<MusicPlayer>().Length)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
