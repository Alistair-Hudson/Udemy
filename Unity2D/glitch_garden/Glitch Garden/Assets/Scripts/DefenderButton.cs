using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefenderButton : MonoBehaviour
{
    [SerializeField] byte greyOutR = 41;
    [SerializeField] byte greyOutG = 41;
    [SerializeField] byte greyOutB = 41;
    [SerializeField] byte greyOutShade = 255;
    [SerializeField] Defender defenderPrefab;

    private void Start()
    {
        Text costText = GetComponentInChildren<Text>();
        if (costText)
        {
            costText.text = defenderPrefab.GetResourceCost().ToString();
        }
    }

    public void OnMouseDown()
    {
        var buttons = FindObjectsOfType<DefenderButton>();
        foreach(DefenderButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(greyOutR, greyOutG, greyOutB, greyOutShade);
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        FindObjectOfType<DefenderSpawner>().SetSelcetedDefender(defenderPrefab);
    }
}
