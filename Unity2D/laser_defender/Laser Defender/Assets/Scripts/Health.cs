using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Player player;
    Text health;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        health = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = player.GetHealth().ToString();
    }
}
