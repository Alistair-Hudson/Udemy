using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master volume";
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    const string DIFFICULTY_KEY = "difficulty";
    const float MIN_DIFFICULTY = 1;
    const float MAX_DIFFICULTY = 50;
    

    public static void SetMasterVolume(float volume)
    {
        if (MIN_VOLUME <= volume && MAX_VOLUME >= volume)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetDifficulty(float difficulty)
    {
        if (MIN_DIFFICULTY <= difficulty && MAX_DIFFICULTY >= difficulty)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
}
