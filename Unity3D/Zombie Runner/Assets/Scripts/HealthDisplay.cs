using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    PlayerHealth health;
    Text healthDisplay;

    // Start is called before the first frame update
    void Start()
    {
        health =  FindObjectOfType<PlayerHealth>();
        healthDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = health.GetHealth().ToString();
    }
}
