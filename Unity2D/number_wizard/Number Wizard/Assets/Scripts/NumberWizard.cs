using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour
{

    [SerializeField] int max;
    [SerializeField] int min;
    int guess;
    [SerializeField] Text guessText;

    // Start is called before the first frame update
    void Start()
    {
        CalculateGuess();
    }

    public void OnPressHigher()
    {
        min = guess + 1;
        CalculateGuess();
    }

    public void OnPressLower()
    {
        max = guess - 1;
        CalculateGuess();
    }

    void CalculateGuess()
    {
        guess = (int)Random.Range(min, max + 1);
        guessText.text = guess.ToString();
    }

}

