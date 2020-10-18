using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] int resources = 250;
    Text resourceText;

    // Start is called before the first frame update
    void Start()
    {
        resourceText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        resourceText.text = resources.ToString();
    }

    public void AddResources(int amount)
    {
        resources += amount;
        UpdateDisplay();
    }

    public void SpendResources(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            UpdateDisplay();
        }
    }

    public bool HaveEnough(int amount)
    {
        return resources >= amount;
    }
}
