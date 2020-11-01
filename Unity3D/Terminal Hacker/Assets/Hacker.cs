using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    [SerializeField] int maxNumTrys = 5;
    [SerializeField] float maxTimeSec = 60f;
    [SerializeField] float timePenalty = 5f;

    enum Screen
    {
        Mainmenu = 0,
        Password,
        Win
    };

    Screen currentScreen = Screen.Mainmenu;
    string level;
    string passwordToCrack;
    int trysLeft;
    float timeLeft;

    string[] kgbPassword = { "soviet", "stalin" };
    string[] ciaPassword = { "'merica", "trump", "washington" };
    string[] mossadPassword = { "david", "moshe", "avraham", "sinai" };


    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        
    }

    void ShowMainMenu()
    {
        level = " ";
        currentScreen = Screen.Mainmenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What Would you like to hack into?");
        Terminal.WriteLine("Press 1 for the KGB");
        Terminal.WriteLine("    Unlimited trys, no time limit");
        Terminal.WriteLine("Press 2 for the CIA");
        Terminal.WriteLine("    Limited number of trys, no time limit");
        Terminal.WriteLine("Press 3 for Mossad");
        Terminal.WriteLine("    Limited time, failed trys cost time");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Enter your selection:");
        trysLeft = maxNumTrys;
    }

    private void OnUserInput(string input)
    {
        if ("menu" == input)
        {
            ShowMainMenu();
        }
        else if ("quit" == input)
        {
            Application.Quit();
        }
        else if (Screen.Mainmenu == currentScreen)
        {
            RunMainMenu(input);
        }
        else if (Screen.Password == currentScreen)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        switch (input)
        {
            case "1":
                level = "KGB";
                passwordToCrack = kgbPassword[Random.Range(0, kgbPassword.Length)];
                StartGame();
                break;
            case "2":
                level = "CIA";
                passwordToCrack = ciaPassword[Random.Range(0, ciaPassword.Length)];
                StartGame();
                break;
            case "3":
                level = "Mossad";
                passwordToCrack = mossadPassword[Random.Range(0, mossadPassword.Length)];
                timeLeft = maxTimeSec;
                StartGame();
                break;
            default:
                Terminal.WriteLine("Sorry I ddn't quite get that...");
                break;
        }
    }

    void CheckPassword(string input)
    {
        switch (level)
        {
            case "KGB":
                PasswordCheck(input);
                break;
            case "CIA":
                --trysLeft;
                PasswordCheck(input);
                if (0 >= trysLeft)
                {
                    Lose();
                }
                break;
            case "Mossad":
                PasswordCheck(input);
                if (0 >= (timeLeft -= timePenalty))
                {
                    Lose();
                }
                break;
        }
    }

    void PasswordCheck(string userInput)
    {
        if (passwordToCrack == userInput)
        {
            currentScreen = Screen.Win;
            WinScreen();
        }
        else
        {
            StartGame();
        }
    }

    void WinScreen()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Congrates");
    }

    void Lose()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        Terminal.WriteLine("The " + level + " have disovered your location");
        Terminal.WriteLine("GAME OVER DUDE");
    }

    void StartGame()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hacking the " + level);
        PrintRemianing();
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Password hint " + passwordToCrack.Anagram());
        Terminal.WriteLine("Type in Password");

    }

    void PrintRemianing()
    {
        switch(level)
        {
            case "CIA":
                Terminal.WriteLine("Atempts left " + trysLeft);
                break;
            case "Mossad":
                Terminal.WriteLine("Time left " + timeLeft);
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (0 >= timeLeft && "Mossad" == level)
        {
            Lose();
        }
    }
}
