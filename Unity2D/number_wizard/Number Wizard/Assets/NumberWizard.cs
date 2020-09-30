using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberWizard : MonoBehaviour
{

    int max = 1000;
    int min = 1;
    int guess = 500;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Welcome to Number Wizard");
        Debug.Log("Pick a number bewteen " + min + "and " + max);
        Debug.Log("Tell me if your number is higher or lower than " + guess);
        Debug.Log("Up Arrow Key = Higher, Down Arrow Key = Lower, Enter/Return = Correct");
        ++max;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            min = guess;
            CalculateGuess();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            max = guess;
            CalculateGuess();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("^_^");
            Debug.Log("Pick another numbner");
            max = 1000;
            min = 1;
            guess = 500;
            Debug.Log("Tell me if your number is higher or lower than " + guess);
        }
    }

    void CalculateGuess()
    {
        guess = (max + min) / 2;
        Debug.Log("Higer or Lower than " + guess);
    }
}

