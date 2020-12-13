using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDamage : MonoBehaviour
{
    Image splatterImage;
    // Start is called before the first frame update
    void Start()
    {
        splatterImage = GetComponentInChildren<Image>();
        ShowSplatter(false);
    }

    public void ShowSplatter(bool state)
    {
        splatterImage.enabled = state;
    }

}
